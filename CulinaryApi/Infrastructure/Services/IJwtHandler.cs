using CulinaryApi.Core.Entieties;

namespace CulinaryApi.Infrastructure.Services
{
    public interface IJwtHandler
    {
        string CreateToken(User user);
    }
}
