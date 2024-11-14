using BudgetManagementAPI.Helpers;
using BudgetManagementAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetManagementApi.Dtos.Models.Budget;
using BudgetManagementAPI.Entities.Enums;

namespace BudgetManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private readonly CurrentUser _currentUser;
    public BudgetController(CurrentUser currentUser, IBudgetService budgetCategoryService)
    {
        _currentUser = currentUser;
        _budgetService = budgetCategoryService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetBudgets()
    {
        var userId = _currentUser.Id;

        var budgets = await _budgetService.GetAllAsync(userId);

        return Ok(budgets);
    }

    [HttpGet("allExisting")]
    [Authorize]
    public async Task<IActionResult> GetExistingBudgets()
    {
        var userId = _currentUser.Id;

        var budgets = await _budgetService.GetAllExistingAsync(userId);

        return Ok(budgets);
    }

    [HttpGet("{category}")]
    [Authorize]
    public async Task<IActionResult> GetBudgetByCategory ([FromRoute] Category category)
    {
        var userId = _currentUser.Id;

        var bugdet = await _budgetService.GetBudgetByCategory(category, userId);

        if (bugdet == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(bugdet);  
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateBudget ([FromBody] BudgetDto budgetDto)
    {
        var userId = _currentUser.Id;

        var budget = await _budgetService.CreateBudgetAsync(userId, budgetDto);

        return Ok(budget);
    }

    [HttpPost("updateBudget/{budgetId}")]
    [Authorize]
    public async Task<IActionResult> UpdateBudget([FromBody] BudgetUpdateDto budgetUpdateDto, Guid budgetId)
    {
        var userId = _currentUser.Id;

        var budget = await _budgetService.UpdateBudgetAync( userId, budgetUpdateDto);

        return Ok(budget);
    }

    [HttpDelete("delete/{budgetId}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid budgetId)
    {
        var userId = _currentUser.Id;

        var budget = await _budgetService.DeleteBudgetAsync(budgetId, userId);

        return Ok(budget);

    }

}

