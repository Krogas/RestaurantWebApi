using Microsoft.EntityFrameworkCore;

namespace RestaurantWebApi.Entities
{
    public class RestaurantContext : DbContext
    {
        private string _connectionString = "Secret";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(d => d.Name).IsRequired().HasMaxLength(25);
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.Property(d => d.Name).IsRequired();
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(d => d.City).IsRequired().HasMaxLength(50);
                entity.Property(d => d.Street).IsRequired().HasMaxLength(50);
            });
        }
    }
}
