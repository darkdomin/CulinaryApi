using Microsoft.AspNetCore.Authorization;

namespace CulinaryApi.Infrastructure.Authorization
{
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get;}

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
    }
}
