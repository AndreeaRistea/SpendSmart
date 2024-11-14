using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.Budget;

public class BudgetDisplayDto
{
    public required Category Category { get; set; }
    public required float Value { get; set; }
}

