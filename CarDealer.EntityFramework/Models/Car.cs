﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    public class Car
    {
        public int Id { get; set; }

        public int BrandID { get; set; }

        public virtual Brand Brand { get; set; }

        public int ModelID { get; set; }

        public virtual Model Model { get; set; }

        public int BookingUserID { get; set; }

        public virtual Customer BookingUser { get; set; }
  
        public DateTime ProductionYear { get; set; }

        public int Course { get; set; } //przebieg

        public double Capacity { get; set; } //pojemnosc

        public string RegistrationNumber { get; set; }

        public double Price { get; set; }

        public bool Booking { get; set; } //czy zarezerwowany

        public byte[] Image { get; set; }

    }
}
