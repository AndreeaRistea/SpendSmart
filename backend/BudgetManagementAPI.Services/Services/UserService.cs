using AutoMapper;
using BudgetManagementApi.Dtos.Models.User;
using BudgetManagementAPI.Entities.Context;
using BudgetManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Services.Services;

public class UserService : IUserService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UserService(UnitOfWork uoW, IMapper mapper)
    {
        _unitOfWork = uoW;
        _mapper = mapper;
    }

    public async Task<UserDetailsDto> UpdateDetails (UserDetailsUpdateDto userDetailsDto, Guid userId)
    {
        var user = await _unitOfWork.Users.FindAsync(userDetailsDto.UserId);
        if (user == null) 
        { 
            throw new ArgumentNullException("User not found"); 
        }
        user.Income = userDetailsDto.Income;
        user.Profession = userDetailsDto.Profession;
        user.LevelFinancialEducation = userDetailsDto.Level;
        user.Currency = userDetailsDto.Currency;
        await _unitOfWork.SaveChangesAsync();
        var mappedUser = _mapper.Map<UserDetailsDto>(user);
        return new UserDetailsDto
        {
            UserId = mappedUser.UserId,
            Email = mappedUser.Email,
            Income = mappedUser.Income,
            Name = mappedUser.Name,
            Profession = mappedUser.Profession,
            LevelFinancialEducation = mappedUser.LevelFinancialEducation,
            Currency = mappedUser.Currency,
        };
    } 

    public async Task<UserDetailsDto> GetUserDetailsAsync (Guid userId)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var mappedUser = _mapper.Map<UserDetailsDto>(user);
        return new UserDetailsDto
        {
            UserId = userId,
            Email = mappedUser.Email,
            Income = mappedUser.Income,
            Name = mappedUser.Name,
            Profession = mappedUser.Profession,
            LevelFinancialEducation = mappedUser.LevelFinancialEducation,
            Currency = mappedUser.Currency,
        };
    }

    public Task<bool> UserExist (string email)
    {
        return _unitOfWork.Users.AnyAsync(user => user.Email.Equals(email));
    }
}

