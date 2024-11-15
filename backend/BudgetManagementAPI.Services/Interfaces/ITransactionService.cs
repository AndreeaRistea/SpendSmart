using BudgetManagementApi.Dtos.Models.Transaction;
using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementAPI.Services.Interfaces;

public interface ITransactionService
{
    Task<TransactionDto> CreateTransactionAsync(Guid userId, TransactionDto transaction);
    Task<List<TransactionDto>> GetTransactionsByCategoryAsync(Guid userId, Category category);
    Task<List<TransactionDto>> GetAllTransaction(Guid userId);
    Task<TransactionDto> UpdateTransactionAsync(Guid userId, TransactionUpdateDto transactionUpdate);
    Task<TransactionDto?> DeleteTransactionAsync(Guid userId, Guid transactionId);
}

