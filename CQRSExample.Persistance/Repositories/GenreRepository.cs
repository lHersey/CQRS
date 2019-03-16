using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CQRSExample.Domain.Entities;
using CQRSExample.Domain.Interfaces;
using CQRSExample.Domain.Resources;
using CQRSExample.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CQRSExample.Persistance.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private readonly VidlyContext context;

        public GenreRepository(VidlyContext context) : base(context) => this.context = context;
    }
}