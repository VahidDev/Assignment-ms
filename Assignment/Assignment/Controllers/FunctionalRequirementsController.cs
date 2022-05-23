using Assignment.Services.Abstraction;
using Assignment.Utilities.ResponseUtilities;
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
            return ResponseGenerator
                .GetResponse(await _functionalRequirementServices
                .GetByRoleOfferIdAsync(id));
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportFunctionalRequirementsAsync
            ([FromForm] IFormFile file)
        {
            return ResponseGenerator
                .GetResponse(await _functionalRequirementServices
                .ValidateExcelFileThenWriteToDbAsync(file));
        }

        [HttpGet]
        public async Task<IActionResult> GetFunctionalRequirementsAsync()
        {
            return ResponseGenerator
                .GetResponse(await _functionalRequirementServices
                .GetAllFunctionalRequirementsAsync());
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateFunctionalRequirementAsync
            (UpdateFunctionalRequirementConvertibleDto convertibleDto)
        {
            return ResponseGenerator
                .GetResponse(await _functionalRequirementServices
                .UpdateFunctionalRequirementAsync(convertibleDto));
        }
    }
}
