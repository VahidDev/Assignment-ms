using Assignment.Services.Abstraction;
using Assignment.Utilities.ResponseUtilities;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
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
            ([FromBody]ICollection<AssignOrWaitlistVolunteerDto>volunteers)
        {
            return ResponseGenerator
                .GetResponse(await _assignmentServices.AssignOrWaitlistAsync(volunteers));
        }
        [HttpPost("ChangeToAnyStatus")]
        public async Task<IActionResult> ChangeToAnyStatusAsync
            ([FromBody] ICollection<VolunteerChangeToAnyStatusDto> volunteers)
        {
            return ResponseGenerator
                .GetResponse(await _assignmentServices.ChangeToAnyStatusAsync(volunteers));
        }
    }
}
