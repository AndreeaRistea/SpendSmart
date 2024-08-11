using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.Budget;

public class BudgetDto
{
    public Guid Id {  get; set; }
    public Category Category { get; set; }  
    public float Percent {  get; set; }
    public float Value {  get; set; }
    public float TotalPercentageSpent {  get; set; }
    public float TotalValueSpent {  get; set; }
    //public float RestSum {  get; set; }
}

