using CarDealer.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Services
{
    /// <summary>
    /// Publiczna klasa CarDealerContext
    /// </summary>
    public class CarDealerContext : DbContext
    {
        /// <summary>
        /// Publiczna konstruktor klasy CarDealerContext
        /// </summary>
        public CarDealerContext(DbContextOptions<CarDealerContext> options) : base(options)
        {
            
            Database.EnsureCreated();
           
        }

        /// <summary>
        /// Publiczna wlasciwosc Cars
        /// </summary>
        public DbSet<Car> Cars { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Customers
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Model
        /// </summary>
        public DbSet<Model> Models { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Brands
        /// </summary>
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(GetCustomer());
            //modelBuilder.Entity<Brand>().HasKey(b => b.Id);
            //modelBuilder.Entity<Model>().HasKey(b => b.Id);
            modelBuilder.Entity<Brand>().HasMany(m => m.Models).WithOne(b => b.Brand);
            modelBuilder.Entity<Model>().HasOne(b => b.Brand).WithMany(m => m.Models);
            modelBuilder.Entity<Car>().HasKey(abc => new { abc.Id});
            modelBuilder.Entity<Brand>().HasData(GetBrand());
            base.OnModelCreating(modelBuilder);

        }

        private Customer[] GetCustomer()
        {
            return new Customer[]
            {
                new Customer {Id = 1, Name = "Patryk", Surname = "Jablonski", Email ="patrykjablonski23@gmail.com", City="Mecina", PhoneNumber= "694926314", PostNumberr="34-654" },
                new Customer {Id = 2, Name = "Marcin", Surname = "Najman", Email ="marcin.najman@gmail.com", City="Czestochowa", PhoneNumber= "666555444", PostNumberr="30-333" },
                new Customer {Id = 3, Name = "Piotr", Surname = "Nowak", Email ="piotr.nowak@gmail.com", City="Warszawa", PhoneNumber= "111222333", PostNumberr="30-111" },
                new Customer {Id = 4, Name = "Paweł", Surname = "Kowal", Email ="paweł.kowal@op.pl", City="Kraków", PhoneNumber= "333222111", PostNumberr="32-121" },
                new Customer {Id = 5, Name = "Kamil", Surname = "Drąg", Email ="kamild@wp.pl", City="Nowy Sącz", PhoneNumber= "123456789", PostNumberr="35-121" },
                new Customer {Id = 6, Name = "Damian", Surname = "Smoleń", Email ="damianek73@vp.pl", City="Kazimierz Dolny", PhoneNumber= "112112112", PostNumberr="38-543" },
                new Customer {Id = 7, Name = "Mateusz", Surname = "Jeż", Email ="jezyk@gmail.com", City="Frombork", PhoneNumber= "998998998", PostNumberr="76-533" },
                new Customer {Id = 8, Name = "Dawid", Surname = "Lachor", Email ="dawid_lachor@gmail.com", City="Rzeszów", PhoneNumber= "183327869", PostNumberr="54-533" },
                new Customer {Id = 9, Name = "Piotr", Surname = "Zimoch", Email ="zimochp@gmail.com", City="Tarnawa", PhoneNumber= "778556434", PostNumberr="45-537" },
                new Customer {Id = 10, Name = "Marek", Surname = "Suski", Email ="suski70m@gmail.com", City="Warszawa", PhoneNumber= "700000000", PostNumberr="70-000" },
            };
        }
        
        private Brand[] GetBrand()
        {
            return new Brand[]
            {
                new Brand{Id=1, Name="Acura"},
                new Brand{Id=2, Name="Alfa Romeo"},
                new Brand{Id=3, Name="Aston Martin"},
                new Brand{Id=4, Name="Audi"},
                new Brand{Id=5, Name="Austin"},
                new Brand{Id=6, Name="Avia"},
                new Brand{Id=7, Name="Bentley"},
                new Brand{Id=8,Name="BMW"},
                new Brand{Id=9,Name="Cadillac"},
                new Brand{Id=10,Name="Chevrolet"},
                new Brand{Id=11,Name="Chrysler"},
                new Brand{Id=12,Name="Citroen"},
                new Brand{Id=13,Name="Dacia"},
                new Brand{Id=14,Name="Dodge"},
                new Brand{Id=15,Name="Ferrari"},
                new Brand{Id=16,Name="Fiat"},
                new Brand{Id=17,Name="Ford"},
                new Brand{Id=18,Name="FSO"},
                new Brand{Id=19,Name="GMC"},
                new Brand{Id=20,Name="Honda"},
                new Brand{Id=21,Name="Hyundai"},
                new Brand{Id=22,Name="Infiniti"},
                new Brand{Id=23,Name="Isuzu"},
                new Brand{Id=24,Name="Iveco"},
                new Brand{Id=25,Name="Jaguar"},
                new Brand{Id=26,Name="Jeep"},
                new Brand{Id=27,Name="Kia"},
                new Brand{Id=28,Name="Lada"},
                new Brand{Id=29,Name="Lamborghini"},
                new Brand{Id=30,Name="Lancia"},
                new Brand{Id=31,Name="Land Rover"},
                new Brand{Id=32,Name="Lexus"},
                new Brand{Id=33,Name="Lincoln"},
                new Brand{Id=34,Name="Lotus"},
                new Brand{Id=35,Name="Maserati"},
                new Brand{Id=36,Name="Mazda"},
                new Brand{Id=37,Name="Mercedes-Benz"},
                new Brand{Id=38,Name="Mini"},
                new Brand{Id=39,Name="Mitsubishi"},
                new Brand{Id=40,Name="Nissan"},
                new Brand{Id=41,Name="Opel"},
                new Brand{Id=42,Name="Peugeot"},
                new Brand{Id=43,Name="Pontiac"},
                new Brand{Id=44,Name="Porsche"},
                new Brand{Id=45,Name="Renault"},
                new Brand{Id=46,Name="Rolls-Royce"},
                new Brand{Id=47,Name="Rover"},
                new Brand{Id=48,Name="Saab"},
                new Brand{Id=49,Name="Saturn"},
                new Brand{Id=50,Name="Scion"},
                new Brand{Id=51,Name="Seat"},
                new Brand{Id=52,Name="Skoda"},
                new Brand{Id=53,Name="Smart"},
                new Brand{Id=54,Name="Subaru"},
                new Brand{Id=55,Name="Suzuki"},
                new Brand{Id=56,Name="Tata"},
                new Brand{Id=57,Name="Toyota"},
                new Brand{Id=58,Name="Trabant"},
                new Brand{Id=59,Name="Uaz"},
                new Brand{Id=60,Name="Voklswagen"},
                new Brand{Id=61,Name="Volvo"},
                new Brand{Id=62,Name="Wartburg"}
            };
        }

    }
}
