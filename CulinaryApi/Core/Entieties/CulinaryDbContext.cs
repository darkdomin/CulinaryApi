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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
