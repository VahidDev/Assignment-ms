using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IUnitOfWork
    {
        IRoleOfferRepository RoleOfferRepository {get;}
        Task CompleteAsync();
        void Dispose();
    }
}
