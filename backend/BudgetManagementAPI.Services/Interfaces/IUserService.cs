using BudgetManagementApi.Dtos.Models.User;

namespace BudgetManagementAPI.Services.Interfaces;

public interface IUserService
{
    Task<UserDetailsDto> UpdateDetails(UserDetailsUpdateDto userDetailsDto, Guid userId);
    Task<UserDetailsDto> GetUserDetailsAsync(Guid userId);
    Task<bool> UserExist(string email);  
}

