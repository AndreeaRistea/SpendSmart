using BudgetManagementAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.AspNetCore.Mvc;
using BudgetManagementAPI.Helpers;
using System.Net.Mail;
using System.Net;
using BudgetManagementApi.Dtos.Models.Auth;
using BudgetManagementApi.Dtos.Models.User;

namespace BudgetManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly CurrentUser _currentUser;
    private readonly IUserService _userService;
    public UserController (IAuthService authService, IUserService userService, CurrentUser currentUser)
    {
        _authService = authService;
        _userService = userService; 
        _currentUser = currentUser;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(Status200OK, Type = typeof(AuthResponseDto))]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequest)
    {

        var authResult = await _authService.LoginAsync(loginRequest);

        if (authResult == null) return BadRequest("User not found or invalid password.");

        SetRefreshToken(authResult.RefreshToken);
            
        return Ok(authResult.AuthResponse);
    }

    [AllowAnonymous]
    [HttpPost("[action]")]
    [ProducesResponseType(Status200OK, Type = typeof(AuthResponseDto))]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto registerRequest)
    {
        var authResult = await _authService.RegisterAsync(registerRequest);

        if (authResult == null) return BadRequest("User already exists.");

        SetRefreshToken(authResult.RefreshToken);

        return Created("", authResult.AuthResponse);
    }
    private void SetRefreshToken(RefreshTokenModel refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = refreshToken.ExpiredTime
        };

        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }


    [HttpPost("updateDetails/{userId}")]
    [Authorize]
    public async Task<IActionResult> UpdateDetails([FromBody] UserDetailsUpdateDto detailsDto)
    {
        var userId = detailsDto.UserId;
        var userDetails = await _userService.UpdateDetails(detailsDto, userId);
        return Ok(userDetails);
    }

    [HttpGet("details")]
    [Authorize]
    public async Task<IActionResult> GetUserDetails()
    {
        var user = _currentUser.Id;
        var userDetails = await _userService.GetUserDetailsAsync(user);
        return Ok(userDetails);
    }

    [HttpPost("send-reset-code")]
    [AllowAnonymous] 
    public async Task<IActionResult> SendResetCode([FromBody] ResetCodeDto resetCode)
    {
        var user = await _authService.GenerateOneTimeCodeAsync(resetCode.Email);

        if (user == null)
        {
            return NotFound("User doesn t exist");
        }
        var client = new SmtpClient("mail.smtp2go.com", 587)
        {
            EnableSsl = false,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("student.ucv.ro", "QPrgPrZnBEgKFuen")
        };


        await client.SendMailAsync(
            new MailMessage(
                from: "ristea.andreea.k5r@student.ucv.ro",
                to: user.Email,
                "Code for reset password",
                $"""
                Hello {user.Name},

                Use this code to reset your password in Spend Smart App: {user.CodeResetPassword}.

                Do not share it with anyone. It expires in 5 minutes.

                """
                ));
       
        return Ok(true);
    }

    [HttpPost("confirm-reset-code")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmResetCode([FromBody] CodeLoginRequestDto loginRequest)
    {
        var authResult = await _authService.LoginAsync(loginRequest);
        if (authResult == null) return BadRequest("User not found or invalid code.");

        SetRefreshToken(authResult.RefreshToken);

        return Ok(authResult.AuthResponse);
    }

    [HttpPut("password")]
    [AllowAnonymous]
    public async Task<IActionResult> ChangePassword([FromBody]ResetPasswordDto resetPassword)
    {
        var changed = await _authService.ChangePasswordAsync(resetPassword.Email, resetPassword.Password);

        if (!changed) return NotFound("User not found.");

        return Ok(resetPassword);
    }

}

