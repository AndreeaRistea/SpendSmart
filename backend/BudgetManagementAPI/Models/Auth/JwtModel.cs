namespace BudgetManagementAPI.Models.Auth
{
    public class JwtModel
    {
        public required string Token {  get; set; }
        public required RefreshTokenModel RefreshToken { get; set; }   

    }
}
