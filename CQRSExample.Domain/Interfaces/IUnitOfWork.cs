using System.Threading;
using System.Threading.Tasks;

namespace CQRSExample.Domain.Interfaces
{
    public interface IUnitOfWork
    {
         Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}