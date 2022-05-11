using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    internal class RoleOfferRepository : GenericRepository<RoleOffer>,IRoleOfferRepository
    {
        public RoleOfferRepository(AppDbContext context, ILogger logger) 
            : base(context, logger){}
    }
}
