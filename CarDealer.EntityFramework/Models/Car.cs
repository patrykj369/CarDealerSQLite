using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    /// <summary>
    /// Publiczna klasa Car
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Publiczna wlasciwosc Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc BrandID
        /// </summary>
        public int BrandID { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Brand
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc ModelID
        /// </summary>
        public int ModelID { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Model
        /// </summary>
        public Model Model { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc BookingUserID
        /// </summary>
        public int BookingUserID { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Bookinguser
        /// </summary>
        public Customer BookingUser { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc ProductionYear
        /// </summary>
        public string ProductionYear { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Course
        /// </summary>
        public string Course { get; set; } //przebieg

        /// <summary>
        /// Publiczna wlasciwosc Capacity
        /// </summary>
        public string Capacity { get; set; } //pojemnosc

        /// <summary>
        /// Publiczna wlasciwosc RegistrationNumber
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Price
        /// </summary>
        public string Price { get; set; }

    }
}
