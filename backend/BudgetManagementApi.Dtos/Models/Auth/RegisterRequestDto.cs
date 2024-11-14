namespace BudgetManagementApi.Dtos.Models.Auth;
public class RegisterRequestDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

