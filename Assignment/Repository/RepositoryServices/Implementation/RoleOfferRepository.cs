using DomainModels.Dtos;
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
    internal class RoleOfferRepository
        : GenericRepository<RoleOffer>
        ,IRoleOfferRepository
    {
        public RoleOfferRepository(AppDbContext context, ILogger logger) 
            : base(context, logger){}

        public Task<RoleOffer> FirstOrDefaultIncludingItemsAsync
            (Expression<Func<RoleOffer, bool>> expression)
        {
            return dbSet.Include(r => r.FunctionalArea)
                .Include(f => f.JobTitle).Include(j => j.Location)
                .Include(r=>r.FunctionalAreaType)
                .Where(r => !r.FunctionalArea.IsDeleted)
                .Where(r => !r.FunctionalAreaType.IsDeleted)
                .Where(r => !r.JobTitle.IsDeleted)
                .Where(r => !r.Location.IsDeleted)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<ICollection<RoleOffer>> GetAllAsNoTrackingWithItemsAsync
            (Expression<Func<RoleOffer, bool>> expression)
        {
            return await dbSet
                .Include(r => r.FunctionalAreaType)
                .Include(f => f.FunctionalArea)
                .Include(j => j.JobTitle)
                .Include(r=>r.Location)
                .Where(r => !r.FunctionalAreaType.IsDeleted)
                .Where(r => !r.FunctionalArea.IsDeleted)
                .Where(r => !r.JobTitle.IsDeleted)
                .Where(r => !r.Location.IsDeleted)
                .Where(expression)
                .AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<RoleOffer>> GetAllIncludingItemsAsync()
        {
            return await dbSet
                .Include(r=>r.FunctionalAreaType)
                .Include(r => r.FunctionalArea)
                .Include(f => f.JobTitle)
                .Include(j => j.Location)
                .Include(r=>r.FunctionalRequirement)
                .ThenInclude(r=>r.Requirements.Where(r=>!r.IsDeleted))
                .Where(r => !r.FunctionalAreaType.IsDeleted)
                .Where(r => !r.FunctionalArea.IsDeleted)
                .Where(r => !r.JobTitle.IsDeleted)
                .Where(r => !r.Location.IsDeleted)
                .Where(r=>!r.IsDeleted)
                .ToListAsync();
        }

        public async Task<ICollection<RoleOffer>> 
            GetAllSpecificRoleOffersAsNoTrackingAsync
            (Expression<Func<RoleOffer, bool>> expression)
        {
            return await dbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<AssigneeDemandWaitlistCountDto>>
         GetAllAssigneeDemandWaitlistCountsAsync(Expression<Func<RoleOffer, bool>> expression)
        {
            return await dbSet
               .Select(r => new AssigneeDemandWaitlistCountDto
               {
                   AssigneeDemand = r.AssigneeDemand,
                   WaitlistDemand = (int)r.WaitlistDemand
               }).AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<RoleOffer>> GetAllSpecificRoleOffersAsync
            (Expression<Func<RoleOffer, bool>> expression)
        {
            return await dbSet.Where(expression).ToListAsync();
        }
    }
}
