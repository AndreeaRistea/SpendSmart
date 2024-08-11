using AutoMapper;
using BudgetManagementAPI.Context;
using BudgetManagementAPI.Entities;
using BudgetManagementAPI.Models.Budget;

namespace BudgetManagementAPI.Services;

public class LevelLessonService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LevelLessonService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

}

