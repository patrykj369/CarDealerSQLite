using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    /// <summary>
    /// Publiczna klasa Customer
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Publiczna wlasciwosc Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc PostNumber
        /// </summary>
        public string PostNumberr { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc PhoneNumber
        /// </summary>
        public string PhoneNumber { get; set; }

    }
}
