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
    internal class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(AppDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<ICollection<Report>> GetAllAsNoTrackingIncludingItemsAsync
            (Expression<Func<Report, bool>> expression)
        {
            return await dbSet
                .Include(r=>r.VolunteerTemplate)
                .ThenInclude(r=>r.Filters.Where(r=>!r.IsDeleted))
                .Include(r=>r.RoleOfferTemplate)
                .ThenInclude(r=>r.Filters.Where(f=>!f.IsDeleted))
                .Where(expression)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Report> GetByIdIncludingItemsAsNoTrackingAsync(int id)
        {
            return dbSet
                .AsNoTracking()
                .Include(r => r.RoleOfferTemplate)
                .ThenInclude(r => r.Filters.Where(f => !f.IsDeleted))
                .Include(r => r.VolunteerTemplate)
                .ThenInclude(v => v.Filters.Where(f => !f.IsDeleted))
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }
        public override bool Delete(Report item)
        {
            item.IsDeleted = true;

            if (item.RoleOfferTemplate != null)
            {
                item.RoleOfferTemplate.IsDeleted = true;

                if (item.RoleOfferTemplate.Filters != null)
                {
                    foreach (Filter filter in item.RoleOfferTemplate.Filters)
                    {
                        filter.IsDeleted = true;
                    }
                }
            }
            if (item.VolunteerTemplate != null)
            {
                item.VolunteerTemplate.IsDeleted = true;

                if (item.VolunteerTemplate.Filters != null)
                {
                    foreach (Filter filter in item.VolunteerTemplate.Filters)
                    {
                        filter.IsDeleted = true;
                    }
                }
            }
            dbSet.Update(item);
            return true;
        }
    }
}
