using System.Security.Claims;

namespace CulinaryApi.Infrastructure.Services.Users
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}