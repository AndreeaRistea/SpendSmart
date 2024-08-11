namespace BudgetManagementAPI.Models.Auth
{
    public sealed class RefreshTokenResponseDto
    {
        public required string Token { get; set; }
        public required string RefreshToken {  get; set; }
    }
}
