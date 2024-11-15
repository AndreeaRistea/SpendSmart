using BudgetManagementApi.Dtos.Models.Budget;
using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementAPI.Services.Interfaces;

public interface IBudgetService
{
    Task<List<BudgetDto>> GetAllExistingAsync(Guid UserId);
    Task<List<BudgetDto>> GetAllAsync(Guid UserId);
    Task<BudgetDisplayDto> GetBudgetByCategory(Category category, Guid UserId);
    Task<BudgetDto> CreateBudgetAsync(Guid userId, BudgetDto budget);
    Task<BudgetDto> UpdateBudgetAync(Guid UserId, BudgetUpdateDto budgetUpdateDto);
    Task<BudgetDto> DeleteBudgetAsync(Guid budgetId, Guid UserId);
}
