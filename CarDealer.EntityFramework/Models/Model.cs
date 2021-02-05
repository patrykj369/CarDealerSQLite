﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int BrandID { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
