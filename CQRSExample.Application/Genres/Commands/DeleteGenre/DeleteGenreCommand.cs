using System.Collections.Generic;
using CQRSExample.Application.Models;
using MediatR;

namespace CQRSExample.Application.Genres.Commands.DeleteGenre
{
    public class DeleteGenreCommand : IRequest<KeyValuePairResource>
    {
        public int Id { get; set; }
    }
}