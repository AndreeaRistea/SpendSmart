using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementApi.Dtos.Models.User;

public class UserDetailsUpdateDto
{
    public required Guid UserId { get; set; }
    public required double Income { get; set; }
    public required Profession Profession { get; set; }
    public required Level Level { get; set; }
}

