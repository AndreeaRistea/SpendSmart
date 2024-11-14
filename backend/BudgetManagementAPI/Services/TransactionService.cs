using AutoMapper;
using BudgetManagementAPI.Context;
using BudgetManagementAPI.Entities;
using BudgetManagementAPI.Enums;
using BudgetManagementAPI.Interfaces;
using BudgetManagementAPI.Models.Budget;
using BudgetManagementAPI.Models.Transaction;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;

namespace BudgetManagementAPI.Services;

public class TransactionService : ITransactionService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILessonService _lessonService;

    public TransactionService(UnitOfWork unitOfWork, IMapper mapper, ILessonService lessonService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _lessonService = lessonService;
    }

    public async Task<TransactionDto> CreateTransactionAsync (Guid userId, TransactionDto transaction)
    {
        var budget = await _unitOfWork.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Category == transaction.Category);
        if (budget == null)
        {
            throw new HttpRequestException("No budget allocated for this category", null, HttpStatusCode.BadRequest);
        }

        var newTransaction = new Transaction
        {
            TransactionId = transaction.TransactionId,
            UserId = userId,
            BudgetId = budget.Id,
            Category = transaction.Category,
            Amount = transaction.Amount,
            Descripiton = transaction.Description!,
            TransactionProcessingTime = transaction.TransactionProcessingTime,
        };
        _unitOfWork.Transactions.Add(newTransaction);
        await CalculateAmountSpent(budget.Id);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TransactionDto>(newTransaction);
    }
    public async Task<TransactionDto?> DeleteTransactionAsync (Guid userId, Guid transactionId)
    {
         var transaction = await _unitOfWork.Transactions.FirstOrDefaultAsync(t => 
                        t.UserId == userId && 
                        t.TransactionId == transactionId);
        if (transaction == null)
        {
            throw new ArgumentNullException("Transaction not found.");
        }
        var budget = await _unitOfWork.Budgets.FirstOrDefaultAsync(b => b.Id == transaction.BudgetId);
        if (budget == null)
        {
            throw new ArgumentNullException("Budget not found.");
        }
        _unitOfWork.Transactions.Remove(transaction);
        await _unitOfWork.SaveChangesAsync();

        await CalculateAmountSpent(budget.Id);

        return _mapper.Map<TransactionDto>(transaction);
    }

    public async Task<TransactionDto> UpdateTransactionAsync (Guid userId, TransactionUpdateDto transactionUpdate)
    {
        var transaction = await _unitOfWork.Transactions.FirstOrDefaultAsync(t =>
                       t.UserId == userId &&
                       t.TransactionId == transactionUpdate.TransactionId);
        if (transaction == null)
        {
            throw new ArgumentNullException("Transaction not found.");
        }
        var budget = await _unitOfWork.Budgets.FirstOrDefaultAsync(b => b.Id == transaction.BudgetId);
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == transaction.UserId);
        if (budget != null && user != null)
        {
            transaction.Amount = transactionUpdate.Amount;

            await _unitOfWork.SaveChangesAsync();
            await CalculateAmountSpent(budget.Id);
        }

        return _mapper.Map<TransactionDto>(transaction);
    }
    public async Task<List<TransactionDto>> GetTransactionsByCategoryAsync(Guid userId, Category category)
    {
        var transactions = await _unitOfWork.Transactions
            .Where(t => t.UserId == userId && t.Category == category)
            .ToListAsync();
       
        var transactionDtos = transactions.Select(transaction => new TransactionDto
        {
            TransactionId = transaction.TransactionId,
            Category = category,
            Amount = transaction.Amount,
            Description = transaction.Descripiton,
            TransactionProcessingTime = transaction.TransactionProcessingTime
        }).ToList();
        return transactionDtos;
    }

    public async Task<List<TransactionDto>> GetAllTransaction(Guid userId)
    {
        var transactions = await _unitOfWork.Transactions
            .Where(t => t.UserId == userId)
             .OrderByDescending(t => t.TransactionProcessingTime)
            .ToListAsync();

        var transactionDtos = transactions.Select(transaction => new TransactionDto
        {
            TransactionId = transaction.TransactionId,
            Category = transaction.Category,
            Amount = transaction.Amount,
            Description = transaction.Descripiton,
            TransactionProcessingTime = transaction.TransactionProcessingTime
        }).ToList();
        return transactionDtos;
    }

    public async Task CalculateAmountSpent(Guid budgetId)
    {
        var budget = await _unitOfWork.Budgets.Include(u => u.User)
            .Include(t => t.Transactions)
            .FirstOrDefaultAsync(b => b.Id == budgetId);
        if (budget == null)
        {
            throw new ArgumentNullException("Budget not found");
        }
        var totalSpent = budget.Transactions?.Sum(x => x.Amount) ?? 0;
        
        var income = budget.User.Income;
        var budgetValue = (budget.Percent / 100) * income;
        float totalSpentPercent = (float)((totalSpent * budget.Percent) / budgetValue);
        budget.TotalPercentageSpent = totalSpentPercent;
        await _unitOfWork.SaveChangesAsync();
        var client = new SmtpClient("mail.smtp2go.com", 587)
        {
            EnableSsl = false,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("student.ucv.ro", "QPrgPrZnBEgKFuen")
        };
        if (budget.TotalPercentageSpent > budget.Percent)
        {
            await client.SendMailAsync(
            new MailMessage(
               from: "ristea.andreea.k5r@student.ucv.ro",
               to: budget.User.Email,
               "Excessive spending",
               $"""
                    Hello {budget.User.Name},

                    You have exceeded the budget limit allocated to the {budget.Category} category 
                    by {budget.TotalPercentageSpent - budget.Percent} %.

                    Check the "Lessons" section of the application and see what you can improve.

                    Good luck!

                    """
               ));
           await _lessonService.AddLessonsToUserAsync(budget.User.Id);
        }

    }
}

