﻿namespace BudgetManagementAPI.Models.Auth;

public class ResetPasswordDto
{
    public required string Email {  get; set; }
    public required string Password { get; set; }
}

