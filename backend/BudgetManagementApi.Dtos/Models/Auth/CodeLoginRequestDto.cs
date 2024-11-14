namespace BudgetManagementApi.Dtos.Models.Auth;

public sealed class CodeLoginRequestDto
{
    public required string Email { get; set; }
    public required string CodeResetPassword {  get; set; }
}

