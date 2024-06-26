﻿using Microsoft.EntityFrameworkCore;

namespace Foodies.Models
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(@"Server=.;Database=Foodies;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Meal)
                .WithOne()
                .HasForeignKey<Reservation>(m => m.Meal_Id)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);

        }
    }
}
