using System.Threading.Tasks;

namespace Repository.RepositoryServices.Abstraction
{
    public interface IUnitOfWork
    {
        public IRoleOfferRepository RoleOfferRepository {get;}
        public IVolunteerRepository VolunteerRepository {get;}
        public ITemplateRepository TemplateRepository { get;}
        public IJobTitleRepository JobTitleRepository { get;}
        public IFunctionalAreaRepository FunctionalAreaRepository { get;}
        public IExcelEntityRepository ExcelEntityRepository { get;}
        public IVenueRepository VenueRepository { get;}
        public IFilterRepository FilterRepository { get;}
        Task CompleteAsync();
        void Dispose();
    }
}
