using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.Persistance.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        private readonly VidlyContext context;

        public RentalRepository(VidlyContext context) : base(context) => this.context = context;
    }
}