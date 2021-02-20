using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    /// <summary>
    /// Publiczna klasa Brand
    /// </summary>
    public class Brand
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
        /// Publiczna wlasciwosc Models
        /// </summary>
        public ICollection<Model> Models { get; set; }

    }
}
