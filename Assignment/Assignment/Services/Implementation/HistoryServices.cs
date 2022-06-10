using Assignment.Services.Abstraction;
using DomainModels.Models.Entities;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Services.Implementation
{
    public class HistoryServices : IHistoryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public HistoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool WriteHistory(Volunteer volunteer, string? email)
        {
            _unitOfWork.HistoryRepository
                    .Add(new History
                    {
                        Status = volunteer.Status
                    ,
                        CreatedAt = DateTime.Now
                    ,
                        RoleOfferId = volunteer.RoleOfferId
                    ,
                        VolunteerId = volunteer.CandidateId
                    ,
                        Email = email
                    });
            return true;
        }
    }
}
