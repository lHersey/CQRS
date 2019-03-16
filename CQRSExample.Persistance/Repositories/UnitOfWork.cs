using System.Threading;
using System.Threading.Tasks;
using CQRSExample.Domain.Interfaces;

namespace CQRSExample.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VidlyContext context;

        public UnitOfWork(VidlyContext context) => this.context = context;

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return this.context.SaveChangesAsync(cancellationToken);
        }
    }
}