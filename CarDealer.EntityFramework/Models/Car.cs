using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    public class Car
    {
        public int Id { get; set; }

        public int BrandID { get; set; }

        public Brand Brand { get; set; }

        public int ModelID { get; set; }

        public Model Model { get; set; }

        public int BookingUserID { get; set; }

        public Customer BookingUser { get; set; }


        public string ProductionYear { get; set; }

        public string Course { get; set; } //przebieg

        public string Capacity { get; set; } //pojemnosc

        public string RegistrationNumber { get; set; }

        public string Price { get; set; }

    }
}
