namespace BudgetManagementAPI.Models.Auth;

public sealed class SendResetCodeResultDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string CodeResetPassword { get; set; }
}

