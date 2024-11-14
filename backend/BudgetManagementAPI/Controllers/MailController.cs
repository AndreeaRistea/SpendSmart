using BudgetManagementAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BudgetManagementAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly IUserService _userService;

    public MailController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("is-valid")]
    [AllowAnonymous]
    public async Task<IActionResult> IsValidAsync([FromQuery] string email)
    {
        _ = new MailAddress(email);
        var isUser = await _userService.UserExist(email);
        return Ok(isUser);
    }
}

