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
        public IFunctionalAreaTypeRepository FunctionalAreaTypeRepository { get;}
        public ILocationRepository LocationRepository { get;}
        public IFilterRepository FilterRepository { get;}
        public IFunctionalRequirementRepository FunctionalRequirementRepository { get;}
        public IRequirementRepository RequirementRepository { get;}
        public IReportRepository ReportRepository { get;}
        public IHistoryRepository HistoryRepository { get;}
        Task CompleteAsync();
        void Complete();
        void ClearChangs();
        void Dispose();
    }
}
