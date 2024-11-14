using BudgetManagementAPI.Models.User;

namespace BudgetManagementAPI.Interfaces;

public interface IUserService
{
    Task<UserDetailsDto> UpdateDetails(UserDetailsUpdateDto userDetailsDto, Guid userId);
    Task<UserDetailsDto> GetUserDetailsAsync(Guid userId);
    Task<bool> UserExist(string email);  
}

