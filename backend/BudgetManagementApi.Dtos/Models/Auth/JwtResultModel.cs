namespace BudgetManagementApi.Dtos.Models.Auth
{
    public class JwtResultModel
    {
        public required string Token {  get; set; }
        public required string RefreshToken {  get; set; }
    }
}
