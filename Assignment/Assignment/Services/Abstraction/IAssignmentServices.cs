using DomainModels.Dtos;

namespace Assignment.Services.Abstraction
{
    public interface IAssignmentServices
    {
        Task<bool> AssignOrWaitlistAsync(ICollection<VolunteerDto> volunteers); 
        Task<bool> ChangeToAnyStatusAsync(ICollection<VolunteerDto> volunteers); 
    }
}
