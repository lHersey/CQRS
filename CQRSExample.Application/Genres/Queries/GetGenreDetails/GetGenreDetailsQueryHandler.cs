using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Infraestructure.Exceptions;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using MediatR;

namespace CQRSExample.Application.Genres.Queries.GetGenreDetails
{
    public class GetGenreDetailsQueryHandler : IRequestHandler<GetGenreDetailsQuery, KeyValuePairResource>
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;

        public GetGenreDetailsQueryHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }
        
        public async Task<KeyValuePairResource> Handle(GetGenreDetailsQuery request, CancellationToken cancellationToken)
        {
            var genre = await this.genreRepository.FindUniqueAsync(ct => ct.Id == request.GenreId, cancellationToken);

            if (genre is null)
            {
                throw new NotFoundException(nameof(genre), request.GenreId);
            }

            return mapper.Map<KeyValuePairResource>(genre);
        }
    }
}