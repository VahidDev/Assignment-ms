using DomainModels.Models.Entities;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IHistoryRepository 
        : IGenericRepository<History>
    {
        bool Add(History history);
    }
}
