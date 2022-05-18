using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
namespace Repository.RepositoryServices.Implementation
{
    internal class FunctionalAreaRepository
        :GenericRepository<FunctionalArea>,IFunctionalAreaRepository
    {
        public FunctionalAreaRepository
            (AppDbContext context, ILogger logger) : base(context, logger) { }
    }
}
