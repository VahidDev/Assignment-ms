using DomainModels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Constants;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
using Repository.Utilities.GenericRepositoryUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Implementation
{
    public class TemplateRepository:GenericRepository<Template>,ITemplateRepository
    {
        public TemplateRepository(AppDbContext context, ILogger logger ) 
            : base(context, logger){}
        public override bool Delete(Template template)
        {
            foreach (var filter in template.Filters)
            {
                filter.IsDeleted = true;
            }
            template.IsDeleted = true;
            dbSet.Update(template);
            return true;
        }

        public async Task<Template> GetTemplatesWithFiltersAsNoTrackingAsync
            (Expression<Func<Template,bool>> expression)
        {
            return await dbSet.Include(t => t.Filters.Where(f => !f.IsDeleted))
                .AsNoTracking().FirstOrDefaultAsync(expression);
        }
        public async override Task<IEnumerable<Template>> GetAllAsync
            (IEnumerable<string> includingItems = null)
        {
            _querable = _querable.IncludeItemsIfExist(includingItems);
            return await _querable.Where(t => !t.IsDeleted 
            && !t.Name.Contains(TemplateDifferentiatorConstants.ReportTemplate))
                .Include(t=>t.Filters.Where(f=>!f.IsDeleted)).ToListAsync();
        }
    }
}
