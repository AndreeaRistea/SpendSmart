using BudgetManagementApi.Dtos.Models.Transaction;
using BudgetManagementAPI.Entities.Enums;
using BudgetManagementAPI.Helpers;
using BudgetManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagementAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly CurrentUser _currentUser;
    public TransactionController(CurrentUser currentUser, ITransactionService transactionService)
    {
        _currentUser = currentUser;
        _transactionService = transactionService;
    }

    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionDto transactionDto)
    {
        var userId = _currentUser.Id;

        var transaction = await _transactionService.CreateTransactionAsync(userId, transactionDto);

        return Ok(transaction);
        IFormFile a;
    }

    [HttpGet("{category}")]
    [Authorize]
    public async Task<IActionResult> GetTransactionsByCategory([FromRoute] Category category)
    {
        var userId = _currentUser.Id;

        var transactions = await _transactionService.GetTransactionsByCategoryAsync(userId,category);

        if (transactions == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(transactions);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllTransactions()
    {
        var userId = _currentUser.Id;
        var transactions = await _transactionService.GetAllTransaction(userId);

        if (transactions == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(transactions);
        }
    }

    [HttpPost("update/{transactionId}")]
    [Authorize]
    public async Task<IActionResult> UpdateTransaction([FromBody] TransactionUpdateDto transactionUpdateDto,
                                                        Guid transactionId)
    {
        var userId = _currentUser.Id;
        var transaction = await _transactionService.UpdateTransactionAsync(userId , transactionUpdateDto);
        return Ok(transaction);
    }

    [HttpDelete("delete/{transactionId}")]
    [Authorize]
    public async Task<IActionResult> DeleteTransaction([FromRoute] Guid transactionId)
    {
        var userId = _currentUser.Id;
        var transaction = await _transactionService.DeleteTransactionAsync(userId, transactionId);
        return Ok(transaction);
    }
}

