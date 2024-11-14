using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementApi.Dtos.Models.Budget;

public class BudgetDisplayDto
{
    public required Category Category { get; set; }
    public required float Value { get; set; }
}

