using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Authorization
{
    public class LoginRequirementHandler : AuthorizationHandler<LoginRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoginRequirement requirement)
        {
            var email = context.User.FindFirst(x => x.Type == ClaimTypes.Name);

            if(email != null)
            {
                var login = email.Value;
                if (login != requirement.Email)
                {
                    context.Succeed(requirement);
                }
            }
              
            return Task.CompletedTask;

        }
    }
}
