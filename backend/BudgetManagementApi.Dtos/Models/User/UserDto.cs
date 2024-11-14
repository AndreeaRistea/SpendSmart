using AutoMapper;

namespace BudgetManagementApi.Dtos.Models.User;

public class UserDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public static UserDto MapFromUser (BudgetManagementAPI.Entities.Entities.User user, IMapper mapper)
    {
        var dto = mapper.Map<UserDto>(user);
        return dto;
    }
}

