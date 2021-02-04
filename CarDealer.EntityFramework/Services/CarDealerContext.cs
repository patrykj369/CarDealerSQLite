using CarDealer.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Services
{
    public class CarDealerContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Brand> Brands {get;set;}

        public CarDealerContext(DbContextOptions<CarDealerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(GetCustomer());

            base.OnModelCreating(modelBuilder);
        }

        private Customer[] GetCustomer()
        {
            return new Customer[]
            {
                new Customer {Id = 1, Name = "Patryk", Surname = "Jablonski", Email ="patrykjablonski23@gmail.com", City="Mecina", PhoneNumber= 694926314, PostNumberr="34654" },
                new Customer {Id = 2, Name = "Marcin", Surname = "Najman", Email ="marcin.najman@gmail.com", City="Czestochowa", PhoneNumber= 666555444, PostNumberr="30333" }
            };
        }

    }
}
