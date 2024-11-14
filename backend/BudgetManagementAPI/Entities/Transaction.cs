using BudgetManagementAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManagementAPI.Entities;

public class Transaction
{
    [Key]
    public Guid TransactionId { get; set; }

    [ForeignKey(nameof(User))]
    public required Guid UserId { get; set; }

    public virtual User User { get; set; }

    [ForeignKey(nameof(Budget))]
    public required Guid BudgetId { get; set; }

    public virtual Budget Budget { get; set; }

    [Column(TypeName = "text")]
    public Category Category { get; set; }

    public required double Amount { get; set; }

    [MaxLength(500)]
    public required string Descripiton {  get; set; }

    public DateTime? TransactionProcessingTime { get; set; }
}

