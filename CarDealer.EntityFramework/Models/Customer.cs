using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.EntityFramework.Models
{
    class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string City { get; set; }

        public string PostNumberr { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }


    }
}
