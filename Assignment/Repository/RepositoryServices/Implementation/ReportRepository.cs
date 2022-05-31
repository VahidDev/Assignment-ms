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
    }
}
