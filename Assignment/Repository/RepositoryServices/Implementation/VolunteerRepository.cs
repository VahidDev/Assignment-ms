using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    internal class VolunteerRepository:GenericRepository<Volunteer>,IVolunteerRepository
    {
        public VolunteerRepository(AppDbContext context,ILogger logger) : base(context, logger) { }
    }
}
