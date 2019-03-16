using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using MediatR;

namespace CQRSExample.Application.Genres.Commands.CreateGenre
{
    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, KeyValuePairResource>
    {
        private readonly IGenreRepository GenreRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateGenreCommandHandler(IGenreRepository GenreRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.GenreRepository = GenreRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<KeyValuePairResource> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Description = request.Description
            };

            await this.GenreRepository.AddAsync(genre);
            await this.unitOfWork.SaveAsync(cancellationToken);

            return mapper.Map<KeyValuePairResource>(genre);
        }
    }
}