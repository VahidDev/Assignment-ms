using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;
using System.Threading.Tasks;

namespace Repository.RepositoryServices.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        public IRoleOfferRepository RoleOfferRepository { get; private set; }
        public IVolunteerRepository VolunteerRepository { get; private set; }
        public ITemplateRepository TemplateRepository { get; private set; }
        public IFilterRepository FilterRepository { get; private set; }
        public IFunctionalAreaTypeRepository FunctionalAreaTypeRepository { get; private set; }
        public IFunctionalAreaRepository FunctionalAreaRepository { get; private set; }
        public IJobTitleRepository JobTitleRepository { get; private set; }
        public ILocationRepository LocationRepository { get; private set; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            RoleOfferRepository = new RoleOfferRepository(context, _logger);
            VolunteerRepository = new VolunteerRepository(context, _logger);
            FilterRepository = new FilterRepository(context, _logger);
            TemplateRepository = new TemplateRepository(context, _logger);
            JobTitleRepository = new JobTitleReposiotry(context, _logger);
            LocationRepository = new LocationRepository(context, _logger);
            FunctionalAreaTypeRepository = new FunctionalAreaTypeRepository(context, _logger);
            FunctionalAreaRepository = new FunctionalAreaRepository(context, _logger);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose() => _context.Dispose();
    }
}
