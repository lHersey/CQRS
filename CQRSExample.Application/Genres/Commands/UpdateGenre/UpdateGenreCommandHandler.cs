using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Infraestructure.Exceptions;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using MediatR;

namespace CQRSExample.Application.Genres.Commands.UpdateGenre
{
    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, KeyValuePairResource>
    {
        private readonly IGenreRepository GenreRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateGenreCommandHandler(IGenreRepository GenreRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.GenreRepository = GenreRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<KeyValuePairResource> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await this.GenreRepository.FindUniqueAsync(ct => ct.Id == request.Id, cancellationToken);

            if (genre is null)
            {
                throw new NotFoundException(nameof(genre), request.Id);
            }

            mapper.Map<UpdateGenreCommand, Genre>(request, genre);
            await unitOfWork.SaveAsync(cancellationToken);

            genre = await this.GenreRepository.FindUniqueAsync(ct => ct.Id == request.Id, cancellationToken);
            return mapper.Map<KeyValuePairResource>(genre);
        }
    }
}