using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CQRSExample.Domain.Entities
{
    public class Movie
    {
        public Movie()
        {
            this.Rentals = new Collection<Rental>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberInStock { get; set; }
        public decimal DailyRentalRate { get; set; }
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}