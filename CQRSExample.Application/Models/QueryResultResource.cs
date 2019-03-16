using System.Collections.Generic;

namespace CQRSExample.Application.Models
{
    public class QueryResultResource<TEntity>
    {
        public int TotalItems { get; set; }
        public IEnumerable<TEntity> Items { get; set; }
    }
}