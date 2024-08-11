namespace BudgetManagementAPI.Models.Transaction;

public class TransactionUpdateDto
{
    public Guid TransactionId { get; set; }
    public double Amount { get; set; }
}

