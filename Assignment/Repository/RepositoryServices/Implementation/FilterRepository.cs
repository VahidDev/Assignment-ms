using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    internal class FilterRepository:GenericRepository<Filter>,IFilterRepository
    {
        public FilterRepository(AppDbContext context, ILogger logger)
            : base(context, logger) { }
    }
}
