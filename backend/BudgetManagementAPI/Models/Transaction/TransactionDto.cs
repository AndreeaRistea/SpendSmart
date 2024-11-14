using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.Transaction;

public class TransactionDto
{
    public required Guid TransactionId { get; set; }
    public required Category Category { get; set; }
    public required double Amount { get; set; }
    public string? Description { get; set; }
    public DateTime? TransactionProcessingTime { get; set; }
}

