using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Brand> Brand { get; set; }
    }
}
