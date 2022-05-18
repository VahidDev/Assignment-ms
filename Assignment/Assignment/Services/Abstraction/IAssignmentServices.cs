using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Services.Abstraction
{
    public interface IAssignmentServices
    {
        Task<JsonResult> AssignOrWaitlistAsync
            (ICollection<AssignOrWaitlistVolunteerDto> volunteers); 
        Task<JsonResult> ChangeToAnyStatusAsync
            (ICollection<VolunteerChangeToAnyStatusDto> volunteers); 
    }
}
