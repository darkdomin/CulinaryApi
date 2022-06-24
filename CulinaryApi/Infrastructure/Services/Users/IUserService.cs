using CulinaryApi.Infrastructure.DTO.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services.Users
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto dto);
        Task<UserDto> LoginAsync(LoginDto dto);
        Task RemoveAsync(int id);
        Task<IEnumerable<UserDto>> Get();
        Task<UserDto> Get(int id);
        Task UpdateAsync(UserUpdateDto dto, int id);
        Task ChangeRole(UserUpdateRole dto);
    }
}
