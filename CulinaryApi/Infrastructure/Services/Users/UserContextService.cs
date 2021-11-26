using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CulinaryApi.Infrastructure.Services.Users
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;
        public int? GetUserId => User is null ? null : (int?)int.Parse(User.FindFirst(u => u
                                                                           .Type == ClaimTypes.NameIdentifier)
                                                                          .Value
                                                                      );
    }
}
