using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    internal class JobTitleReposiotry:GenericRepository<JobTitle>,IJobTitleRepository
    {
        public JobTitleReposiotry(AppDbContext context,ILogger logger) : base(context, logger) { }
    }
}
