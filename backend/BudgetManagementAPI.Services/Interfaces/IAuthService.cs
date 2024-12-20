﻿using BudgetManagementApi.Dtos.Models.Auth;

namespace BudgetManagementAPI.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResultModel?> RegisterAsync(RegisterRequestDto registerRequest);

    Task<AuthResultModel?> LoginAsync(LoginRequestDto loginRequest);

    Task<AuthResultModel?> LoginAsync(CodeLoginRequestDto codeLoginRequest);
    Task<SendResetCodeResultDto?> GenerateOneTimeCodeAsync(string email);

    Task<bool> ChangePasswordAsync(string email, string password);
}

