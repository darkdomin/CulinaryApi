using CulinaryApi.Core.Entieties;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly CulinaryDbContext dbContext;

        public DataInitializer(CulinaryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task SeedAsync()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Roles.Any())
                {
                    IEnumerable<Role> roles = GetRoles();
                    await dbContext.Roles.AddRangeAsync(roles);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private static IEnumerable<Role> GetRoles()
        {
            var admin = new Role("Admin");
            var user = new Role("User");

            return new List<Role>()
            {
               admin, user
            };
        }
    }
}
