using Microsoft.EntityFrameworkCore;


namespace CulinaryApi.Core.Entieties
{
    public class CulinaryDbContext : DbContext
    {
        public CulinaryDbContext(DbContextOptions<CulinaryDbContext> options) : base(options)
        {

        }
        
        public DbSet<Recipe> Recipes { get; protected set; }
        public DbSet<Meal> Meals { get; protected set; }
        public DbSet<Difficulty> Difficulties { get; protected set; }
        public DbSet<Cuisine> Cuisines { get; protected set; }
        public DbSet<Time> TImes { get; protected set; }
        public DbSet<User> Users { get; protected set; }
        public DbSet<Role> Roles { get; protected set; }
       public DbSet<FavoriteRecipe> FavoriteRecipe { get; protected set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .Property(u => u.Email)
              .IsRequired();

            modelBuilder.Entity<User>()
              .Property(u => u.Email)
              .HasMaxLength(20);

            modelBuilder.Entity<Role>()
             .Property(u => u.Name)
             .IsRequired();

            modelBuilder.Entity<Recipe>()
                .Property(l => l.Name)
                .IsRequired();

            modelBuilder.Entity<Meal>()
                .Property(l => l.Name)
                .IsRequired();

            modelBuilder.Entity<Difficulty>()
                .Property(l => l.Name)
                .IsRequired();

            modelBuilder.Entity<Cuisine>()
                .Property(l => l.Name)
                .IsRequired();

            modelBuilder.Entity<Time>()
                .Property(l => l.Name)
                .IsRequired();
        }
    }
}
