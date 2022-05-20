using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
namespace Repository.RepositoryServices.Implementation
{
    internal class LocationRepository:GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository
          (AppDbContext context, ILogger logger) : base(context, logger) { }
    }
}
