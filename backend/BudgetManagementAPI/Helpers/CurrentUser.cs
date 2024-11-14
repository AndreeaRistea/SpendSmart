using BudgetManagementAPI.Utils;
using System.Security.Claims;

namespace BudgetManagementAPI.Helpers;

public class CurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string? _userName;
    private string? _email;

    private Guid _userId;

    public CurrentUser (IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid Id
    {
        get
        {
            if (_userId != Guid.Empty)
            {
                return _userId;
            }

            var id = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(claim => claim.Type.Equals(Constants.Token.UserIdClaim))?.Value;

            _userId = new Guid(id ?? string.Empty);

            return _userId;
        }
    }

    public string? UserName
    {
        get
        {
            if (_userName != null) return _userName;

            return _userName = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(claim => claim.Type.Equals(nameof(ClaimTypes.Name)))?.Value;
        }
    }

    public string? Email
    {
        get
        {
            if(_email != null) return _email;

            return _email = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(claim => claim.Type.Equals(nameof(ClaimTypes.Email)))?.Value;
        }
    }
}

