using System.Collections.Generic;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Interfaces;
using MediatR;

namespace CQRSExample.Application.Genres.Queries
{
    public class GetGenreListQuery : IRequest<IEnumerable<KeyValuePairResource>>{ }
}