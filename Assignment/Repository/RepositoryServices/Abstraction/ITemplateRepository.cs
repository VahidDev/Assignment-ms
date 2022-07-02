using DomainModels.Models.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface ITemplateRepository:IGenericRepository<Template>
    {
        Task<Template> GetTemplatesWithFiltersAsNoTrackingAsync
            (Expression<Func<Template,bool>>expression);
        Task<Template> GetTemplatesWithFiltersAsync
            (Expression<Func<Template, bool>> expression);
    }
}
