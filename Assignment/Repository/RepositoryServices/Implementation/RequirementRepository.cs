using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    internal class RequirementRepository : GenericRepository<Requirement>, IRequirementRepository
    {
        public RequirementRepository(AppDbContext context, ILogger logger): base(context, logger) { }
    }
}
