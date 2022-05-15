using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
namespace Repository.RepositoryServices.Implementation
{
    public class TemplateRepository:GenericRepository<Template>,ITemplateRepository
    {
        public TemplateRepository(AppDbContext context, ILogger logger) 
            : base(context, logger) { }
    }
}
