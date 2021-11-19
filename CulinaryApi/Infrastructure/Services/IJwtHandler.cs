using CulinaryApi.Infrastructure.DTO;

namespace CulinaryApi.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(int userId, string role);
    }
}
