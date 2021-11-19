using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using Microsoft.EntityFrameworkCore;
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
           return await Task.FromResult(dbContext
                                        .Users
                                        .Include(e => e.Role)
                                        .SingleOrDefault(x => x.Id == id));
        }

        public async Task<User> GetAsync(string email)
        { 
          return await Task.FromResult(dbContext
                                       .Users
                                       .Include(e => e.Role)
                                       .FirstOrDefault(x => x.Email.ToLower() == email.ToLower()));
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
