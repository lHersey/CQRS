using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Models;
using CQRSExample.Application.Movies.Models;
using CQRSExample.Domain.Interfaces;
using CQRSExample.Domain.Resources;
using MediatR;

namespace CQRSExample.Application.Movies.Queries.GetMovieList
{
    public class GetMovieListQueryHandler : IRequestHandler<GetMovieListQuery, QueryResultResource<MovieResource>>
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;

        public GetMovieListQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
        }

        public async Task<QueryResultResource<MovieResource>> Handle(GetMovieListQuery request, CancellationToken cancellationToken)
        {
            var movieQuery = mapper.Map<MovieQuery>(request);
            var queryResult = await movieRepository.GetPagedMovieList(movieQuery, cancellationToken);
            return mapper.Map<QueryResultResource<MovieResource>>(queryResult);
        }
    }
}