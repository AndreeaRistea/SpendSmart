using BudgetManagementAPI.Enums;
using BudgetManagementAPI.Models.Transaction;

namespace BudgetManagementAPI.Interfaces;

public interface ITransactionService
{
    Task<TransactionDto> CreateTransactionAsync(Guid userId, TransactionDto transaction);
    Task<List<TransactionDto>> GetTransactionsByCategoryAsync(Guid userId, Category category);
    Task<List<TransactionDto>> GetAllTransaction(Guid userId);
    Task<TransactionDto> UpdateTransactionAsync(Guid userId, TransactionUpdateDto transactionUpdate);
    Task<TransactionDto?> DeleteTransactionAsync(Guid userId, Guid transactionId);
}

