using CQRSExample.Application.Models;

namespace CQRSExample.Application.Movies.Models
{
    public class MovieResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NumberInStock { get; set; }
        public decimal DailyRentalRate { get; set; }
        public KeyValuePairResource Genre { get; set; }
    }
}