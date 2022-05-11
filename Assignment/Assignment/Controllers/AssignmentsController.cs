using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAssignmentServices _assignmentServices;
        public AssignmentsController(IUnitOfWork unitOfWork, IMapper mapper
            ,IAssignmentServices assignmentServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _assignmentServices= assignmentServices;
        }
        [HttpPost("AssignOrWaitList")]
        public async Task<IActionResult> AssignOrWaitListAsync
            ([FromBody]ICollection<VolunteerDto>volunteers)
        {
            return Ok(await _assignmentServices.AssignOrWaitlistAsync(volunteers));
        }
        [HttpPost("ChangeToAnyStatus")]
        public async Task<IActionResult> ChangeToAnyStatusAsync
            ([FromBody] ICollection<VolunteerDto> volunteers)
        {
            return Ok(await _assignmentServices.ChangeToAnyStatusAsync(volunteers));
        }
    }
}
