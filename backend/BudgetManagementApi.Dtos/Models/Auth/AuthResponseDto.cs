using AutoMapper;
using BudgetManagementApi.Dtos.Models.User;

namespace BudgetManagementApi.Dtos.Models.Auth
{
    public sealed class AuthResponseDto :UserDto
    {
        public string? Token {  get; set; }
        public string? RefreshToken {  get; set; }

        public static new AuthResponseDto MapFromUser (BudgetManagementAPI.Entities.Entities.User user, IMapper mapper)
        {
            var dto = mapper.Map<AuthResponseDto>(user);
            return dto;
        }
    }
}
