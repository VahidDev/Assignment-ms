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
        private readonly AppDbContext _context;

        public FunctionalRequirementRepository(AppDbContext context, ILogger ilogger)
            :base(context, ilogger) 
        {
            _context = context;
        }

        public async Task<ICollection<FunctionalRequirement>> GetAllAsNoTrackingIncludingItemsAsync
            (Expression<Func<FunctionalRequirement, bool>> expression)
        {
            ICollection<FunctionalRequirement> functionalRequirements =
                await dbSet
                .Include(fr => fr.Requirements.Where(r => !r.IsDeleted))
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
            
            int[] roleOfferIds = functionalRequirements
                .Select(r=>r.RoleOfferId)
                .ToArray();

            ICollection<RoleOffer> roleOffers =
               await _context.RoleOffers
               .Where(r=>roleOfferIds.Contains(r.RoleOfferId))
               .AsNoTracking()
               .ToListAsync();

            foreach (FunctionalRequirement functionalRequirement in functionalRequirements)
            {
                functionalRequirement.RoleOffer = roleOffers
                    .FirstOrDefault(r=> r.RoleOfferId == functionalRequirement.RoleOfferId);
            }

            return functionalRequirements;
        }

        public async Task<FunctionalRequirement> GetByIdAsNoTrackingIncludingItemsAsync
            (Expression<Func<FunctionalRequirement, bool>> expression)
        {
            FunctionalRequirement functionalRequirement =
                await dbSet
                 .AsNoTracking()
                 .Include(fr => fr.Requirements.Where(r => !r.IsDeleted))
                 .FirstOrDefaultAsync(expression);

            RoleOffer roleOffer =
               await _context.RoleOffers
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.RoleOfferId == functionalRequirement.RoleOfferId);

            functionalRequirement.RoleOffer = roleOffer;

            return functionalRequirement;
        }

        public async Task<FunctionalRequirement> GetByRoleOfferIdAsNoTrackingAsync(int roleOfferId)
        {
            RoleOffer roleOffer = await _context.RoleOffers
               .AsNoTracking()
               .FirstOrDefaultAsync(r => r.Id == roleOfferId);

            if(roleOffer == null)
            {
                return null;
            }

            FunctionalRequirement functionalRequirement 
                = await dbSet
                 .AsNoTracking()
                 .Include(fr => fr.Requirements.Where(r => !r.IsDeleted))
                 .FirstOrDefaultAsync(r => !r.IsDeleted
                 && r.RoleOfferId == roleOffer.RoleOfferId);

            if (functionalRequirement != null)
            {
                functionalRequirement.RoleOffer = roleOffer;

            }

            return functionalRequirement;
        }
    }
}
