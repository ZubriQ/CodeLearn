using CodeLearn.Application.Common.Interfaces;
using System.Security.Claims;

namespace CodeLearn.Api.Services;

public class CurrentUser : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null;

            // For JWT-based auth, looking for a specific claim
            var jwtId = httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(jwtId)) return jwtId;

            // For Windows Authentication, fallback to the Name claim or directly use the Identity.Name
            return httpContext.User?.FindFirstValue(ClaimTypes.WindowsAccountName) ?? httpContext.User?.Identity?.Name;
        }
    }
}