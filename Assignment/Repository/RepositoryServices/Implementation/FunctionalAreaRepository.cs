using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Implementation
{
    internal class FunctionalAreaRepository
        :GenericRepository<FunctionalArea>,IFunctionalAreaRepository
    {
        public FunctionalAreaRepository
            (AppDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<ICollection<FunctionalArea>> GetAllWithJTsAsNoTrackingAsync
         (Expression<Func<FunctionalArea, bool>> expression)
        {
            return await dbSet
                .Include(r => r.JobTitles.Where(r => !r.IsDeleted))
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
