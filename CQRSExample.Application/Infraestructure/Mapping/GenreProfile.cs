using System.Collections.Generic;
using AutoMapper;
using CQRSExample.Application.Genres.Commands.CreateGenre;
using CQRSExample.Application.Genres.Commands.UpdateGenre;
using CQRSExample.Application.Genres.Queries;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Resources;

namespace CQRSExample.Application.Infraestructure.Mapping
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<Genre, KeyValuePairResource>();

            CreateMap<CreateGenreCommand, Genre>();
            CreateMap<UpdateGenreCommand, Genre>();
        }
    }
}