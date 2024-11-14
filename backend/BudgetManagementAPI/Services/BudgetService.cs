using AutoMapper;
using BudgetManagementAPI.Context;
using BudgetManagementAPI.Entities;
using BudgetManagementAPI.Enums;
using BudgetManagementAPI.Interfaces;
using BudgetManagementAPI.Models.Budget;
using BudgetManagementAPI.Models.Transaction;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Services;

public class BudgetService  : IBudgetService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public BudgetService (UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<BudgetDto>> GetAllExistingAsync (Guid UserId)
    {
        var budgets = await _unitOfWork.Budgets.Where(b => b.UserId == UserId)
           .ToListAsync();
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == UserId);

        if (user == null)
        {
            throw new ArgumentNullException($"User with ID {UserId} not found.");
        }

        var budgetDtos = budgets.Select(budget =>  new BudgetDto
        {
            Id = budget.Id,
            Category = budget.Category,
            Percent = budget.Percent,
            Value = (float)((budget.Percent / 100.0) * user.Income),
            TotalPercentageSpent = budget.TotalPercentageSpent,
            TotalValueSpent = (float)((budget.TotalPercentageSpent / 100.0) * user.Income),
        }).ToList();

        return budgetDtos;
    }
    public async Task <List<BudgetDto>> GetAllAsync (Guid UserId)
    {
        var budgets =  await _unitOfWork.Budgets.Where(b => b.UserId == UserId)
            .ToListAsync();
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u=>u.Id==UserId);

        if (user == null)
        {
            throw new ArgumentNullException($"User with ID {UserId} not found.");
        }

        var categories = Enum.GetValues<Category>();


        var budgetDtos = categories.Select(category => {
            var budget = budgets.Find(b => b.Category == category);

            if(budget == null)
            {
                return new BudgetDto
                {
                    Id = new Guid(),
                    Category = category,
                    Percent = 0,
                    Value = 0,
                    TotalPercentageSpent = 0,
                    TotalValueSpent = 0,
                };
            }

            return new BudgetDto
            {
                Id = budget.Id,
                Category = budget.Category,
                Percent = budget.Percent,
                Value = (float)((budget.Percent / 100.0) * user.Income),
                TotalPercentageSpent = budget.TotalPercentageSpent,
                TotalValueSpent = (float)((budget.TotalPercentageSpent / 100.0) * user.Income),
            };
        }).ToList();

        return budgetDtos;
    }

    public async Task <BudgetDisplayDto> GetBudgetByCategory (Category category, Guid UserId)
    {
        var budgetCategory = await _unitOfWork.Budgets
            .FirstOrDefaultAsync(b => b.UserId == UserId && b.Category == category);

        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == UserId);

        if (budgetCategory == null)
        {
            throw new Exception($"Budget category not found.");
        }

        var budgetDto = new BudgetDisplayDto
        {
            Category = budgetCategory.Category,
            Value = (float)((budgetCategory.Percent / 100.0) * user.Income),
        };

        return budgetDto;
    }

  
    public async Task <BudgetDto> CreateBudgetAsync (Guid userId, BudgetDto budget)
    {
        var existingBudget = await _unitOfWork.Budgets.FirstOrDefaultAsync(b => b.UserId == userId && b.Category == budget.Category);

        if (existingBudget != null)
        {
            var updateBudgetDto = new BudgetUpdateDto
            {
                Id = existingBudget.Id,
                Percent = budget.Percent,
            };
            return await UpdateBudgetAync(userId, updateBudgetDto);
        }
        else
        {
            var newBudget = new Budget
            {
                Id = new Guid(),
                UserId = userId,
                Category = budget.Category,
                Percent = budget.Percent,
                TotalPercentageSpent = 0,
            };

            _unitOfWork.Budgets.Add(newBudget);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BudgetDto>(newBudget);
        }
    }

    public async Task<BudgetDto> UpdateBudgetAync( Guid UserId, BudgetUpdateDto budgetUpdateDto)
    {
        var existingBudget = await _unitOfWork.Budgets.Where(b => b.UserId == UserId)
            .FirstOrDefaultAsync(b => b.Id.Equals(budgetUpdateDto.Id));

        if (existingBudget == null)
        {
            throw new ArgumentNullException($"Budget not found.");
        }
        existingBudget.Percent = budgetUpdateDto.Percent; 
        var newBudget = new Budget
        {
            Id = existingBudget.Id,
            Category = existingBudget.Category,
            Percent = existingBudget.Percent,
            TotalPercentageSpent = existingBudget.TotalPercentageSpent,
            UserId = UserId,
           
        };
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<BudgetDto>(newBudget);
    }

    public async Task<BudgetDto> DeleteBudgetAsync(Guid budgetId, Guid UserId)
    {
        var budgetToDelete = await _unitOfWork.Budgets
            .Where(u => u.UserId == UserId)
            .FirstOrDefaultAsync(b=>b.Id.Equals(budgetId));

        if (budgetToDelete == null) 
        {
            throw new ArgumentNullException("Budget not found.");
            
        }

        var transactions = await _unitOfWork.Transactions
             .Where(t => t.BudgetId == budgetId)
             .ToListAsync();

        if (transactions.Count > 0)
        {
            throw new NotSupportedException("Cannot delete budget with associated transactions.");
        }

        _unitOfWork.Budgets.Remove(budgetToDelete);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<BudgetDto>(budgetToDelete);

    }

}

