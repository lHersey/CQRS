using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using CQRSExample.Domain.Resources;
using CQRSExample.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.Persistance.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        private readonly VidlyContext context;

        public MovieRepository(VidlyContext context) : base(context) => this.context = context;

        public Task<Movie> GetMovieWithGenre(int movieId, CancellationToken cancellationToken)
        {
            return context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(pr => pr.Id == movieId, cancellationToken);
        }

        public async Task<QueryResult<Movie>> GetPagedMovieList(MovieQuery movieQuery, CancellationToken cancellationToken)
        {

            var queryResult = new QueryResult<Movie>();
            var query = context.Movies.Include(mv => mv.Genre).AsQueryable();

            var columnsOrder = new Dictionary<string, Expression<Func<Movie, object>>>{
                ["id"] = x => x.Id,
                ["title"] = x => x.Title,
                ["genre"] = x => x.Genre.Description,
                ["numberInStock"] = x => x.NumberInStock,
                ["dailyRentalFee"] = x => x.DailyRentalRate
            };

            var columnsFilter = new Dictionary<bool, Expression<Func<Movie, bool>>>{
                [movieQuery.Id.HasValue] = x => x.Id == movieQuery.Id,
                [!String.IsNullOrWhiteSpace(movieQuery.Title)] = x => x.Title.Contains(movieQuery.Title),
                [movieQuery.GenreId.HasValue] = x => x.GenreId == movieQuery.GenreId,
                [movieQuery.NumberInStock.HasValue] = x => x.NumberInStock == movieQuery.NumberInStock,
            };

            query = query.ApplyFiltering(movieQuery, columnsFilter);
    

            queryResult.TotalItems = await query.CountAsync(cancellationToken);
            
            query = query.ApplyOrdering(movieQuery, columnsOrder);
            query = query.ApplyPaging(movieQuery);

            queryResult.Items = await query.ToListAsync(cancellationToken);

            return queryResult;
        }
    }
}