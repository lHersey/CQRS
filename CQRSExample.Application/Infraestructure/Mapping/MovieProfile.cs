using AutoMapper;
using CQRSExample.Application.Movies.Commands.CreateMovie;
using CQRSExample.Application.Movies.Models;
using CQRSExample.Application.Movies.Queries.GetMovieList;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Resources;

namespace CQRSExample.Application.Infraestructure.Mapping
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<GetMovieListQuery, MovieQuery>();
            CreateMap<CreateMovieCommand, Movie>();
            CreateMap<Movie, MovieResource>();
        }
    }
}