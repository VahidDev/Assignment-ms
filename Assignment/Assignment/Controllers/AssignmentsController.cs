using Assignment.Attributes;
using Assignment.Constants.RoutingConstants;
using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    //[DynamicRoute($"[controller]")]
    [Route($"{RoutingConstants.Routing}/[controller]")]
    [ApiController]
    public class AssignmentsController:ControllerBase
    {
        private readonly IAssignmentServices _assignmentServices;
        
        public AssignmentsController(IAssignmentServices assignmentServices)
        {
            _assignmentServices= assignmentServices;
        }

        [HttpPost("AssignOrWaitList")]
        public async Task<IActionResult> AssignOrWaitListAsync
            ([FromBody]ICollection<AssignOrWaitlistVolunteerDto>volunteers, 
            [FromQuery] string email)
        {
            return await _assignmentServices.AssignOrWaitlistAsync(volunteers,email);
        }

        [HttpPost("ChangeToAnyStatus")]
        public async Task<IActionResult> ChangeToAnyStatusAsync
            ([FromBody] ICollection<VolunteerChangeToAnyStatusDto> volunteers, 
            [FromQuery] string email)
        {
            return await _assignmentServices.ChangeToAnyStatusAsync(volunteers,email);
        }
    }
}
