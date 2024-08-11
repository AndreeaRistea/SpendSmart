using AutoMapper;
using BudgetManagementAPI.Entities;
using BudgetManagementAPI.Models.Auth;
using BudgetManagementAPI.Models.Budget;
using BudgetManagementAPI.Models.Lesson;
using BudgetManagementAPI.Models.Transaction;
using BudgetManagementAPI.Models.User;

namespace BudgetManagementAPI.Mapping;

internal class MappingProfile : Profile
{
    public MappingProfile()    
    {
        CreateMap<User, AuthResponseDto>();
        CreateMap<User, RefreshTokenResponseDto>();
        CreateMap<User, UserDto>();
        CreateMap<User, UserDetailsDto>();
        CreateMap<Budget, BudgetDto>();
        CreateMap<Budget, BudgetDisplayDto>();   
        CreateMap<Budget, BudgetUpdateDto>();
        CreateMap<Transaction, TransactionDto>();
        CreateMap<Transaction, TransactionUpdateDto>();
        CreateMap<Lesson, LessonDto>();
        CreateMap<LevelLesson, LevelLessonDto>();
        CreateMap<User, SendResetCodeResultDto>();

    }
}

