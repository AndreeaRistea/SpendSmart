using BudgetManagementAPI.Context;
using BudgetManagementAPI.Interfaces;
using BudgetManagementAPI.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BudgetManagementApi.Dtos.Models.Auth;
using BudgetManagementAPI.Entities.Entities;
using static BudgetManagementAPI.Utils.PasswordUtils;

namespace BudgetManagementAPI.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;


    public AuthService(UnitOfWork unitOfWork,IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<AuthResultModel?> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(
            user => user.Email.Equals(loginRequest.Email)); 

        

        if (user == null || !VerifyPasswordHash(loginRequest.Password, user.PasswordHash)) return null;

        var jwt = CreateJwt(user);
        await _unitOfWork.SaveChangesAsync();

        var mappedUser = AuthResponseDto.MapFromUser(user, _mapper);
        mappedUser.Token = jwt.Token;
        mappedUser.RefreshToken = jwt.RefreshToken.Token;

        return new AuthResultModel
        { AuthResponse = mappedUser,
         RefreshToken = jwt.RefreshToken
        };
    }

    public async Task<AuthResultModel?> LoginAsync(CodeLoginRequestDto codeLoginRequest)
    {
        var user = await _unitOfWork.Users
            .FirstOrDefaultAsync(user => user.Email.Equals(codeLoginRequest.Email)
            && user.CodeResetPassword != null && user.CodeResetPassword.Equals(codeLoginRequest.CodeResetPassword));

        if (user == null) return null;

        var oneTimeCodeExpires = user.TimeCodeExpires;

        user.CodeResetPassword = null;
        user.TimeCodeExpires = null;

        if (oneTimeCodeExpires < DateTime.UtcNow) return null;

        var jwt = CreateJwt(user);
        await _unitOfWork.SaveChangesAsync();

        var mappedUser = AuthResponseDto.MapFromUser(user, _mapper);
        mappedUser.Token = jwt.Token;
        mappedUser.RefreshToken = jwt.RefreshToken.Token;

        return new AuthResultModel
        {
            AuthResponse = mappedUser,
            RefreshToken = jwt.RefreshToken
        };
    }

    async Task<AuthResultModel?> IAuthService.RegisterAsync(RegisterRequestDto registerRequest)
    {
        var userExists = await _unitOfWork.Users.AnyAsync(
            user => user.Email.Equals(registerRequest.Email)
            );

        if (userExists) return null;


        CreatePasswordHash(registerRequest.Password, out var passwordHash);

        var user = new User
        {
            Email = registerRequest.Email,
            Name = registerRequest.Name,
            PasswordHash = passwordHash,
            Income = 0,
        };


        var entityEntry = await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var jwt = CreateJwt(user);

        var mappedUser = AuthResponseDto.MapFromUser(user, _mapper);
        mappedUser.Id = entityEntry.Entity.Id;
        mappedUser.Token = jwt.Token;
        mappedUser.RefreshToken = jwt.RefreshToken.Token;

        return new AuthResultModel
        {
            AuthResponse = mappedUser,
            RefreshToken = jwt.RefreshToken
        };
    }

    private JwtModel CreateJwt(User user)
    {
        var token = GenerateToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken.Token;
        user.TokenCreated = refreshToken.CreatedTime;
        user.TokenExpires = refreshToken.ExpiredTime;

        return new JwtModel
        {
            Token = token,
            RefreshToken = refreshToken
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(Constants.Token.UserIdClaim, user.Id.ToString()),
            new(nameof(ClaimTypes.Name), user.Name),
            new(nameof(ClaimTypes.Email), user.Email)
        };

        //var key = new SymmetricSecurityKey(
        //    Encoding.UTF8.GetBytes(_configuration[Constants.Token.JwtKey] ?? throw new InvalidOperationException()));
        string jwtKey = _configuration[Constants.Token.JwtKey];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT key configuration is missing or invalid.");
        }
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.Add(Constants.Token.TokenLife),
            signingCredentials: signingCredentials,
            issuer: _configuration[Constants.Token.JwtIssuer],
            audience: _configuration[Constants.Token.JwtAudience]
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static RefreshTokenModel GenerateRefreshToken()
    {
        return new RefreshTokenModel
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiredTime = DateTime.UtcNow.Add(Constants.Token.RefreshTokenLife),
            CreatedTime = DateTime.UtcNow
        };
    }

    public async Task<SendResetCodeResultDto?> GenerateOneTimeCodeAsync(string email)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

        if (user == null) return null;

        var generator = new Random();
        var code = generator
            .Next(0, (int)Math.Pow(10, Constants.Token.OtpDigits))
            .ToString(new string('0', Constants.Token.OtpDigits));

        user.CodeResetPassword = code;
        user.TimeCodeExpires = DateTime.UtcNow.Add(Constants.Token.OtpLife);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SendResetCodeResultDto>(user);
    }

    public async Task<bool> ChangePasswordAsync(string email, string password)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

        if (user == null) return false;
       
        CreatePasswordHash(password, out var passwordHash);

        user.PasswordHash = passwordHash;
        
        await _unitOfWork.SaveChangesAsync();

        return true;
    }


}

