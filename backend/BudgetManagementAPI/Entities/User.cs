using BudgetManagementAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManagementAPI.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(100)]
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public required double Income { get; set; }
        

        [Column(TypeName = "text")]
        public Profession Profession { get; set; }

        [Column(TypeName = "text")]
        public Level LevelFinancialEducation { get; set; }

        public string? RefreshToken {  get; set; }

        public DateTime? TokenCreated { get; set; }

        public DateTime? TokenExpires {  get; set; }

        [StringLength(4)]
        public string? CodeResetPassword { get; set; }

        public DateTime? TimeCodeExpires {  get; set; }

        public List<Transaction> Transactions { get; set; }

        public List<Budget> UserBudgetCategories { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
