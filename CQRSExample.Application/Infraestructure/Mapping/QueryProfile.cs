using AutoMapper;
using CQRSExample.Application.Models;
using CQRSExample.Domain.Resources;

namespace CQRSExample.Application.Infraestructure.Mapping
{
    public class QueryProfile : Profile
    {
        public QueryProfile()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
        }
    }
}