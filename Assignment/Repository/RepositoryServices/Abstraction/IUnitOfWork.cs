using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IUnitOfWork
    {
        public IRoleOfferRepository RoleOfferRepository {get;}
        public IVolunteerRepository VolunteerRepository {get;}
        public ITemplateRepository TemplateRepository { get;}
        Task CompleteAsync();
        void Dispose();
    }
}
