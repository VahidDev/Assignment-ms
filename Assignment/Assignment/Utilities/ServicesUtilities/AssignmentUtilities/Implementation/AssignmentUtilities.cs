using Assignment.Utilities.ServicesUtilities.AssignmentUtilities.Abstraction;
using DomainModels.Dtos;
using Repository.RepositoryServices.Abstraction;
using Entities = DomainModels.Models.Entities;
namespace Assignment.Utilities.ServicesUtilities.AssignmentUtilities.Implementation
{
    public class AssignmentUtilities:IAssignmentUtilities
    {
        private readonly IUnitOfWork _unitOfWork;
        public AssignmentUtilities(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddDeletingAssignmentIdToListAsync
            (ICollection<int> deletedAssignmentIds, VolunteerDto volunteerDto)
        {
            Entities.Assignment assignment = await _unitOfWork.AssignmentRepository
                .FirstOrDefaultAsync
                (a => a.VolunteerId == volunteerDto.Id && !a.IsDeleted);
            if (assignment == null) return false;
            deletedAssignmentIds.Add(assignment.Id);
            return true;
        }
    }
}
