using Microsoft.EntityFrameworkCore;

namespace CulinaryApi.Core.Entieties
{
    public class CulinaryDbContext : DbContext
    {
        private string _connectionString = "Data Source=DARK\\SQLEXPRESS;Database=CulinaryDb;Trusted_Connection=True";

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Time> TImes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .Property(u => u.Email)
              .IsRequired();

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
