using CQRSExample.Application.Models;
using MediatR;

namespace CQRSExample.Application.Genres.Commands.CreateGenre
{
    public class CreateGenreCommand : IRequest<KeyValuePairResource>
    {
        public string Description { get; set; }
    }
}