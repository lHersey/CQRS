using CQRSExample.Domain.Interfaces;

namespace CQRSExample.Domain.Resources
{
    public class MovieQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }

        public int? Id { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public int? NumberInStock { get; set; }
        public decimal? DailyRentalFee { get; set; }
    }
}