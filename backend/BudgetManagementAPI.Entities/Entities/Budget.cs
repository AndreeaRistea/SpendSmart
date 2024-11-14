using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementAPI.Entities.Entities;

public class Budget : BaseEntity
{
    [Key]
    public required Guid Id { get; set; }

    [ForeignKey(nameof(User))]
    public required Guid UserId { get; set; }

    public virtual User User { get; set; }

    [Column(TypeName = "text")]
    public Category Category { get; set; }

    public required float Percent {  get; set; }

    public required float TotalPercentageSpent {  get; set; }
    public virtual ICollection<Transaction>? Transactions { get; set; }
}

