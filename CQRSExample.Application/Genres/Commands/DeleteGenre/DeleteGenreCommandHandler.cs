using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Infraestructure.Exceptions;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using MediatR;

namespace CQRSExample.Application.Genres.Commands.DeleteGenre
{
    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, KeyValuePairResource>
    {
        private readonly IGenreRepository GenreRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DeleteGenreCommandHandler(IGenreRepository GenreRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.GenreRepository = GenreRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<KeyValuePairResource> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await this.GenreRepository.FindUniqueAsync(ct => ct.Id == request.Id, cancellationToken);

            if (genre is null)
            {
                throw new NotFoundException(nameof(genre), request.Id);
            }

            this.GenreRepository.Remove(genre);
            await this.unitOfWork.SaveAsync(cancellationToken);

            return mapper.Map<KeyValuePairResource>(genre);
        }
    }
}