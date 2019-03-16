using CQRSExample.Application.Models;
using MediatR;

namespace CQRSExample.Application.Genres.Queries.GetGenreDetails
{
    public class GetGenreDetailsQuery : IRequest<KeyValuePairResource>
    {
        public int GenreId { get; set; }
    }
}