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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=CarDealer.db");

            optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
