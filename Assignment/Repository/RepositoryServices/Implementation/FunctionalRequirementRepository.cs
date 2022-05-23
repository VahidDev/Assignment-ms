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
    internal class FunctionalRequirementRepository
        : GenericRepository<FunctionalRequirement>, IFunctionalRequirementRepository
    {
        public FunctionalRequirementRepository(AppDbContext context, ILogger ilogger)
            :base(context, ilogger) { }

        public async Task<ICollection<FunctionalRequirement>> GetAllAsNoTrackingIncludingItemsAsync
            (Expression<Func<FunctionalRequirement, bool>> expression)
        {
            return await dbSet
                .AsNoTracking()
                .Include(fr=>fr.Requirements.Where(r=>!r.IsDeleted))
                .Include(fr=>fr.RoleOffer)
                .ToListAsync();
        }

        public async Task<FunctionalRequirement> GetByIdAsNoTrackingIncludingItemsAsync
            (Expression<Func<FunctionalRequirement, bool>> expression)
        {
            return await dbSet
                 .AsNoTracking()
                 .Include(fr => fr.Requirements.Where(r => !r.IsDeleted))
                 .Include(fr => fr.RoleOffer)
                 .FirstOrDefaultAsync(expression);
        }
    }
}
