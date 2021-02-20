using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    /// <summary>
    /// Publiczna klasa Model
    /// </summary>
    public class Model
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
        /// Publiczna wlasciwosc Brand
        /// </summary>
        public Brand Brand { get; set; }
    }
}
