using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementApi.Dtos.Models.Budget;

public class BudgetDto
{
    public required Guid Id {  get; set; }
    public required Category Category { get; set; }  
    public required float Percent {  get; set; }
    public required float Value {  get; set; }
    public required float TotalPercentageSpent {  get; set; }
    public required float TotalValueSpent {  get; set; }
}

