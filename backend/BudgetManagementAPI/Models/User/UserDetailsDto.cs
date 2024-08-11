using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.User;

public class UserDetailsDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }

    public double Income { get; set; }

    public Profession Profession {  get; set; }

    public Level LevelFinancialEducation { get; set; }
}

