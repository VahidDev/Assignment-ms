using DomainModels.Dtos;

namespace Assignment.Utilities.ServicesUtilities.AssignmentUtilities.Abstraction
{
    public interface IAssignmentUtilities
    {
        Task<bool> AddDeletingAssignmentIdToListAsync
            (ICollection<int> deletedAssignmentIds, VolunteerDto volunteerDto);
    }
}
