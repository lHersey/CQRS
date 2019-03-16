using CQRSExample.Application.Models;
using CQRSExample.Application.Movies.Models;
using CQRSExample.Domain.Interfaces;
using MediatR;

namespace CQRSExample.Application.Movies.Queries.GetMovieList
{
    public class GetMovieListQuery : IRequest<QueryResultResource<MovieResource>>, IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }

        public int? Id { get; set; }
        public string Title { get; set; }
        public int? GenreId { get; set; }
        public int? NumberInStock { get; set; }

    }
}