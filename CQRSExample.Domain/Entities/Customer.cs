using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CQRSExample.Domain.Entities
{
    public class Customer
    {
        public Customer()
        {
            this.Rentals = new Collection<Rental>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsGold { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}