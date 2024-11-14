using AutoMapper;
using BudgetManagementApi.Dtos.Models.Auth;
using BudgetManagementApi.Dtos.Models.Budget;
using BudgetManagementApi.Dtos.Models.Lesson;
using BudgetManagementApi.Dtos.Models.Transaction;
using BudgetManagementApi.Dtos.Models.User;
using BudgetManagementAPI.Entities.Entities;

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

