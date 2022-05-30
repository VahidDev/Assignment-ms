using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IAssignmentServices
    {
        Task<ObjectResult> AssignOrWaitlistAsync
            (ICollection<AssignOrWaitlistVolunteerDto> volunteers); 
        Task<ObjectResult> ChangeToAnyStatusAsync
            (ICollection<VolunteerChangeToAnyStatusDto> volunteers); 
    }
}
