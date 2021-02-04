using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    public class Car
    {
        public int Id { get; set; }

        public virtual ICollection<Brand> Brand { get; set; }

        public virtual ICollection<Model> Model { get; set; }

        public virtual ICollection<Customer> BookingUser { get; set; }

        public DateTime ProductionYear { get; set; }

        public int Course { get; set; } //przebieg

        public double Capacity { get; set; } //pojemnosc

        public string RegistrationNumber { get; set; }

        public double Price { get; set; }

        public bool Booking { get; set; } //czy zarezerwowany

        public byte[] Image { get; set; }

    }
}
