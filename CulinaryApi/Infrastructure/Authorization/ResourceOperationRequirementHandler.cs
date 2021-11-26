using CulinaryApi.Core.Entieties;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Recipe>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Recipe recipe)
        {
            var userId = context.User.FindFirst(u => u.Type == ClaimTypes.NameIdentifier).Value;

            if (recipe.CreateById == int.Parse(userId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
