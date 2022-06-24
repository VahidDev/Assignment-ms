using DomainModels.Models.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Implementation
{
    internal class HistoryRepository 
        : GenericRepository<History>
        , IHistoryRepository
    {
        public HistoryRepository(AppDbContext context, ILogger logger) 
            : base( context,logger) { }
        public bool Add(History entity)
        {
            dbSet.Add(entity);
            return true;
        }
    }
}
