using CulinaryApi.Core.Entieties;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryApi
{
    public class CulinarySeeder
    {
        private readonly CulinaryDbContext dbContext;

        public CulinarySeeder(CulinaryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task SeedAsync()
        {
            if (dbContext.Database.CanConnect())
            {
                var migrations = dbContext.Database.GetPendingMigrations();
                if (migrations != null && migrations.Any())
                {
                    dbContext.Database.Migrate();
                }

                if (!dbContext.Roles.Any())
                {
                    IEnumerable<Role> roles = GetRoles();
                    await dbContext.Roles.AddRangeAsync(roles);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.TImes.Any())
                {
                    var times = GetTimes();
                    await dbContext.TImes.AddRangeAsync(times);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Cuisines.Any())
                {
                    var cuisines = GetCuisines();
                    await dbContext.Cuisines.AddRangeAsync(cuisines);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Difficulties.Any())
                {
                    var level = GetLevel();
                    await dbContext.Difficulties.AddRangeAsync(level);
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Meals.Any())
                {
                    var meals = GetMeals();
                    await dbContext.Meals.AddRangeAsync(meals);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public static IEnumerable<Role> GetRoles()
        {
            var admin = new Role("Admin");
            var user = new Role("User");

            return new List<Role>()
            {
               admin, user
            };
        }

        public static IEnumerable<Time> GetTimes()
        {
            return new List<Time>()
            {
                new Time("do 30 min"),
                new Time("30-60 min"),
                new Time("60-90 min"),
                new Time("powyżej 90 min")
            };
        }

        public static IEnumerable<Cuisine> GetCuisines()
        {
            return new List<Cuisine>()
            {
                new Cuisine("amerykańska"),
                new Cuisine("azjatycka"),
                new Cuisine("czeska"),
                new Cuisine("francuska"),
                new Cuisine("grecka"),
                new Cuisine("hiszpańska"),
                new Cuisine("portugalska"),
                new Cuisine("polska"),
                new Cuisine("włoska"),
                new Cuisine("angielska"),
                new Cuisine("orientalna"),
                new Cuisine("alpejska"),
                new Cuisine("tajska"),
                new Cuisine("meksykańska"),
                new Cuisine("chińska")
            };
        }

        public static IEnumerable<Difficulty> GetLevel()
        {
            return new List<Difficulty>()
            {
                new Difficulty("łatwe"),
                new Difficulty("Srednie"),
                new Difficulty("trudne")
            };
        }

        public static IEnumerable<Meal> GetMeals()
        {
            return new List<Meal>()
            {
                new Meal("Danie główne"),
                new Meal("Zupy"),
                new Meal("Sałatki"),
                new Meal("Napoje"),
                new Meal("Przetwory"),
                new Meal("Sniadania"),
                new Meal("Fast Food"),
                new Meal("Przekąski"),
                new Meal("Desery"),
                new Meal("Ciasta"),
                new Meal("Ciastka")
            };
        }
    }
}
