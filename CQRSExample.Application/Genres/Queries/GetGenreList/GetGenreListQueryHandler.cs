using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Interfaces;
using CQRSExample.Domain.Resources;
using MediatR;

namespace CQRSExample.Application.Genres.Queries
{
    public class GetGenreListQueryHandler : IRequestHandler<GetGenreListQuery, IEnumerable<KeyValuePairResource>>
    {
        private readonly IGenreRepository genreRepository;
        private readonly IMapper mapper;

        public GetGenreListQueryHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            this.genreRepository = genreRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<KeyValuePairResource>> Handle(GetGenreListQuery request, CancellationToken cancellationToken)
        {
            var genres =  await this.genreRepository.FindAllAsync();   
            return mapper.Map<IEnumerable<KeyValuePairResource>>(genres);
        }
    }
}