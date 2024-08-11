using AutoMapper;

namespace BudgetManagementAPI.Models.User;

public class UserDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public static UserDto MapFromUser (Entities.User user, IMapper mapper)
    {
        var dto = mapper.Map<UserDto>(user);
        return dto;
    }
}

