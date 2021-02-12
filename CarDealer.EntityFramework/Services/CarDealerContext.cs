using CarDealer.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Services
{
    public class CarDealerContext : DbContext
    {
        
        public CarDealerContext(DbContextOptions<CarDealerContext> options) : base(options)
        {
            
            Database.EnsureCreated();
           
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(GetCustomer());
            modelBuilder.Entity<Brand>().HasMany(m => m.Models).WithOne(b => b.Brand);
            modelBuilder.Entity<Model>().HasOne(b => b.Brand).WithMany(m => m.Models);
            modelBuilder.Entity<Car>().HasKey(abc => new { abc.Id});

            base.OnModelCreating(modelBuilder);
        }

        private Customer[] GetCustomer()
        {
            return new Customer[]
            {
                new Customer {Id = 1, Name = "Patryk", Surname = "Jablonski", Email ="patrykjablonski23@gmail.com", City="Mecina", PhoneNumber= "694926314", PostNumberr="34-654" },
                new Customer {Id = 2, Name = "Marcin", Surname = "Najman", Email ="marcin.najman@gmail.com", City="Czestochowa", PhoneNumber= "666555444", PostNumberr="30-333" },
            };
        }

    }
}
