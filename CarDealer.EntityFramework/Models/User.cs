using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    /// <summary>
    /// Publiczna klasa User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Publiczna wlasciwosc Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Publiczna wlasciwosc Mail
        /// </summary>
        public string Mail { get; set; }
    }
}
