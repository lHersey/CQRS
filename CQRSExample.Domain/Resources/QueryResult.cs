using System.Collections.Generic;

namespace CQRSExample.Domain.Resources
{
    public class QueryResult<TEntity>
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}