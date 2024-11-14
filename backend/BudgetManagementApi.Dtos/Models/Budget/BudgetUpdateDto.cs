namespace BudgetManagementApi.Dtos.Models.Budget;

public class BudgetUpdateDto
{
    public required Guid Id {  get; set; }
    public required float Percent {  get; set; }
}

