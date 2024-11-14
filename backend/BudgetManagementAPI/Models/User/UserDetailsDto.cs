using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.User;

public class UserDetailsDto
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required double Income { get; set; }
    public required Profession Profession {  get; set; }
    public required Level LevelFinancialEducation { get; set; }
}

