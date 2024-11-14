namespace BudgetManagementApi.Dtos.Models.Auth
{
    public class RefreshTokenModel
    {
        public required string Token { get; set; }
        public required DateTime CreatedTime { get; set; }
        public required DateTime ExpiredTime {  get; set; }
    }
}
