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
            modelBuilder.Entity<Brand>().HasData(GetBrand());
            modelBuilder.Entity<Model>().HasData(GetModel());
            base.OnModelCreating(modelBuilder);
        }

        private Customer[] GetCustomer()
        {
            return new Customer[]
            {
                new Customer {Id = 1, Name = "Patryk", Surname = "Jablonski", Email ="patrykjablonski23@gmail.com", City="Mecina", PhoneNumber= 694926314, PostNumberr="34-654" },
                new Customer {Id = 2, Name = "Marcin", Surname = "Najman", Email ="marcin.najman@gmail.com", City="Czestochowa", PhoneNumber= 666555444, PostNumberr="30-333" },
            };
        }

        private Brand[] GetBrand()
        {
            return new Brand[]
            {
                new Brand {Id = 1, Name = "Mercedes"},
                new Brand {Id = 2, Name = "Renault"},
            };
        }

        private Model[] GetModel()
        {
            return new Model[]
            {
                new Model {Id = 1, Name = "S-Class"},
                new Model {Id = 2, Name = "Megane"},
            };
        }

    }
}
