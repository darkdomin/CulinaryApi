using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CulinaryDbContext dbContext;

        public UserRepository(CulinaryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> GetAsync(int id)
        { 
           return await Task.FromResult(dbContext.Users.SingleOrDefault(x => x.Id == id));
        }

        public async Task<User> GetAsync(string email)
        { 
          return await Task.FromResult(dbContext.Users.SingleOrDefault(x => x.Email.ToLowerInvariant() ==               email.ToLowerInvariant()));
        }

        public async Task AddAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }


        public async Task UpdateAsync(User user)
        {
            await dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(User user)
        {
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
