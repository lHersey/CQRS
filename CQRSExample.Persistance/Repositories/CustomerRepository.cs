using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.Persistance.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly VidlyContext context;

        public CustomerRepository(VidlyContext context) : base(context) => this.context = context;
    }
}