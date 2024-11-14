namespace BudgetManagementApi.Dtos.Models.Transaction;

public class TransactionUpdateDto
{
    public required Guid TransactionId { get; set; }
    public required double Amount { get; set; }
}

