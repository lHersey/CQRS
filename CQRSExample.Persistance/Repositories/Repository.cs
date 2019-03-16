using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CQRSExample.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet;
        public Repository(DbContext context) => this.dbSet = context.Set<TEntity>();

        public Task<List<TEntity>> FindAllAsync() => dbSet.ToListAsync();
        public Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken) => dbSet.ToListAsync(cancellationToken);
        
        public Task AddAsync(TEntity entity) => dbSet.AddAsync(entity);
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken) => dbSet.AddAsync(entity, cancellationToken);

        public Task AddRangeAsync(IEnumerable<TEntity> entities) => dbSet.AddRangeAsync(entities);
        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) => dbSet.AddRangeAsync(entities, cancellationToken);

        public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) => dbSet.AnyAsync(predicate);
        public Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => dbSet.AnyAsync(predicate, cancellationToken);

        public Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate) => dbSet.Where(predicate).ToListAsync();
        public Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => dbSet.Where(predicate).ToListAsync(cancellationToken);

        public Task<TEntity> FindUniqueAsync(Expression<Func<TEntity, bool>> predicate) => dbSet.FirstOrDefaultAsync(predicate);
        public Task<TEntity> FindUniqueAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        
        public void Remove(TEntity entity) => dbSet.Remove(entity);
        public void RemoveRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);
    }
}