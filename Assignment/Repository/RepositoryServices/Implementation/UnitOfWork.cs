﻿using Microsoft.Extensions.Logging;
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
        public IAssignmentRepository AssignmentRepository { get; private set; }
        public IVolunteerRepository VolunteerRepository { get; private set; }
        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            RoleOfferRepository = new RoleOfferRepository(context, _logger);
            AssignmentRepository = new AssignmentRepository(context, _logger);
            VolunteerRepository = new VolunteerRepository(context, _logger);
        }
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose() => _context.Dispose();
    }
}
