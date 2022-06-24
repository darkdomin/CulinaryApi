using Microsoft.AspNetCore.Authorization;

namespace CulinaryApi.Infrastructure.Authorization
{
    public class LoginRequirement : IAuthorizationRequirement
    {
        public string Email { get;}
        public LoginRequirement(string email)
        {
            Email = email;
        }
    }
}
