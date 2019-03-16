using System.Threading.Tasks;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Resources;

namespace CQRSExample.Domain.Interfaces
{
    public interface IGenreRepository : IRepository<Genre> { }
}