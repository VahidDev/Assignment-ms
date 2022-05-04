using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Dispose();
    }
}
