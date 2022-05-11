using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentServices _assignmentServices;
        public AssignmentsController(IMapper mapper,IAssignmentServices assignmentServices)
        {
            _mapper = mapper;
            _assignmentServices= assignmentServices;
        }
        [HttpPost("AssignOrWaitList")]
        public async Task<IActionResult> AssignOrWaitListAsync
            ([FromBody]ICollection<VolunteerDto>volunteers)
        {
            return Ok(volunteers.Count == 0 ? false
                : await _assignmentServices.AssignOrWaitlistAsync(volunteers));
        }
        [HttpPost("ChangeToAnyStatus")]
        public async Task<IActionResult> ChangeToAnyStatusAsync
            ([FromBody] ICollection<VolunteerDto> volunteers)
        {

            return Ok(volunteers.Count == 0 ? false
                : await _assignmentServices.ChangeToAnyStatusAsync(volunteers));
        }
    }
}
