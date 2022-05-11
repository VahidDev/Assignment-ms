using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    internal class AssignmentRepository:GenericRepository<Assignment>,IAssignmentRepository
    {
        public AssignmentRepository(AppDbContext context, ILogger logger)
            : base(context, logger) { }
    }
}
