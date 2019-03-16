using CQRSExample.Application.Movies.Models;
using MediatR;

namespace CQRSExample.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommand : IRequest<MovieResource>
    {
        public string Title { get; set; }
        public int NumberInStock { get; set; }
        public decimal DailyRentalRate { get; set; }
        public int GenreId { get; set; }

    }
}