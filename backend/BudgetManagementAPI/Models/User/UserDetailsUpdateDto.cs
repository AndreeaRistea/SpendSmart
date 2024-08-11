using BudgetManagementAPI.Enums;

namespace BudgetManagementAPI.Models.User;

public class UserDetailsUpdateDto
{
    public Guid UserId { get; set; }

    public double Income { get; set; }

    public Profession Profession { get; set; }

    public Level Level { get; set; }
}

