﻿using Microsoft.EntityFrameworkCore;

namespace RestaurantWebApi.Entities
{
    public class RestaurantContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("connectionstring.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(d => d.EmailAddres).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(d => d.Name).IsRequired();
            });
        }
    }
}
