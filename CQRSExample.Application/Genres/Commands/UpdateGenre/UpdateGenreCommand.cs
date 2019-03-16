using CQRSExample.Application.Models;
using MediatR;

namespace CQRSExample.Application.Genres.Commands.UpdateGenre
{
    public class UpdateGenreCommand : IRequest<KeyValuePairResource>
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}