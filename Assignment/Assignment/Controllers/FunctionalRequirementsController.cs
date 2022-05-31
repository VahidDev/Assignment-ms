using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionalRequirementsController : ControllerBase
    {
        private readonly IFunctionalRequirementServices _functionalRequirementServices;

        public FunctionalRequirementsController
            (IFunctionalRequirementServices functionalRequirementServices)
        {
            _functionalRequirementServices = functionalRequirementServices;
        }

        [HttpGet("getByRoleOfferId/{id}")]
        public async Task<IActionResult> GetByRoleOfferIdAsync([FromRoute] int id)
        {
            return await _functionalRequirementServices
                .GetByRoleOfferIdAsync(id);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportFunctionalRequirementsAsync
            ([FromForm] IFormFile file)
        {
            return await _functionalRequirementServices
                .ValidateExcelFileThenWriteToDbAsync(file);
        }

        [HttpGet]
        public async Task<IActionResult> GetFunctionalRequirementsAsync()
        {
            return await _functionalRequirementServices
                .GetAllFunctionalRequirementsAsync();
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateFunctionalRequirementAsync
            ([FromBody] UpdateFunctionalRequirementConvertibleDto convertibleDto)
        {
            return await _functionalRequirementServices
                .UpdateOrAddFunctionalRequirementAsync(convertibleDto);
        }
    }
}
