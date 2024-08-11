namespace BudgetManagementAPI.Models.Auth
{
    public class JwtResultModel
    {
        public required string Token {  get; set; }
        public required string RefreshToken {  get; set; }
    }
}
