using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Resources;

namespace CQRSExample.Domain.Interfaces
{
    public interface IMovieRepository : IRepository<Movie> { 

        Task<QueryResult<Movie>> GetPagedMovieList(MovieQuery movieQuery, CancellationToken cancellationToken);

        Task<Movie> GetMovieWithGenre(int movieId, CancellationToken cancellationToken);


    }
}