using CulinaryApi.Infrastructure.DTO.Users;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Users
{
    public interface IUserService
    {
        //Task<AccountDto> GetAccoutAsync(int userId);
        Task RegisterAsync(RegisterUserDto dto);
        Task<string> LoginAsync(LoginDto dto);

    }
}
