using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
namespace Repository.RepositoryServices.Implementation
{
    internal class VenueRepository:GenericRepository<Venue>, IVenueRepository
    {
        public VenueRepository
          (AppDbContext context, ILogger logger) : base(context, logger) { }
    }
}
