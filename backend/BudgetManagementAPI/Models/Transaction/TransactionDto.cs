using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.Transaction;

public class TransactionDto
{
    public Guid TransactionId { get; set; }
    public Category Category { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
    public DateTime? TransactionProcessingTime { get; set; }
}

