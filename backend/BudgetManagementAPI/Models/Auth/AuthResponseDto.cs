using AutoMapper;
using BudgetManagementAPI.Models.User;

namespace BudgetManagementAPI.Models.Auth
{
    public sealed class AuthResponseDto :UserDto
    {
        public string? Token {  get; set; }
        public string? RefreshToken {  get; set; }

        public static new AuthResponseDto MapFromUser (Entities.User user, IMapper mapper)
        {
            var dto = mapper.Map<AuthResponseDto>(user);
            return dto;
        }
    }
}
