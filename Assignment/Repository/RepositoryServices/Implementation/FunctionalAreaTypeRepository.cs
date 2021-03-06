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
    internal class FunctionalAreaTypeRepository 
        : GenericRepository<FunctionalAreaType>,IFunctionalAreaTypeRepository
    {

        public FunctionalAreaTypeRepository
           (AppDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<ICollection<FunctionalAreaType>> GetAllWithItemsAsNoTrackingAsync
           (Expression<Func<FunctionalAreaType, bool>> expression)
        {
            return await dbSet
                .Include(r => r.FunctionalAreas.Where(r => !r.IsDeleted))
                .ThenInclude(f => f.JobTitles.Where(r => !r.IsDeleted))
                .ThenInclude(j => j.Locations.Where(r => !r.IsDeleted))
                .ThenInclude(r => r.RoleOffers.Where(r => !r.IsDeleted))
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<FunctionalAreaType>> GetAllWithFAsAsNoTrackingAsync
           (Expression<Func<FunctionalAreaType, bool>> expression)
        {
            return await dbSet
                .Include(r => r.FunctionalAreas.Where(r => !r.IsDeleted))
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
