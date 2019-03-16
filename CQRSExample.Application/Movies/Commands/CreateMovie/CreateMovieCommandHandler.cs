using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSExample.Application.Movies.Models;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using MediatR;

namespace CQRSExample.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieResource>
    {
        private readonly IMovieRepository movieRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.movieRepository = movieRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        
        public async Task<MovieResource> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = mapper.Map<Movie>(request);

            await movieRepository.AddAsync(movie, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);

            movie = await movieRepository.GetMovieWithGenre(movie.Id, cancellationToken);

            return mapper.Map<MovieResource>(movie);
        }
    }
}