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
    internal class JobTitleReposiotry:GenericRepository<JobTitle>,IJobTitleRepository
    {
        public JobTitleReposiotry(AppDbContext context,ILogger logger) : base(context, logger) { }

        public async Task<ICollection<JobTitle>> GetAllWithLocsAsNoTrackingAsync
         (Expression<Func<JobTitle, bool>> expression)
        {
            return await dbSet
                .Include(r => r.Locations.Where(r => !r.IsDeleted))
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
