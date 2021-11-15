using CulinaryApi.Core.Entieties;
using System.Threading.Tasks;

namespace CulinaryApi.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(int id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
