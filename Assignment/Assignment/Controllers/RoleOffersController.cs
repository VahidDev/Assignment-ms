using Assignment.Services.Abstraction;
using Assignment.Utilities.ResponseUtilities;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleOffersController : ControllerBase
    {
        private readonly IRoleOfferServices _roleOfferServices;
        public RoleOffersController(IRoleOfferServices roleOfferServices)
        {
            _roleOfferServices = roleOfferServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetALlRoleOffersAsync()
        {
            return ResponseGenerator
                .GetResponse(await _roleOfferServices.GetAllRoleOffersAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleOfferAsync(int id)
        {
            return ResponseGenerator
                .GetResponse(await _roleOfferServices.GetRoleOfferAsync(id));
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportRoleOffersAsync([FromForm]IFormFile file)
        {
            return ResponseGenerator
                .GetResponse(await _roleOfferServices.ValidateExcelFileThenWriteToDbAsync(file));
        }
        [HttpPost("importDetals")]
        public async Task<IActionResult> ImportRoleOfferDetailsAsync([FromForm] IFormFile file)
        {
            return ResponseGenerator
                .GetResponse(await _roleOfferServices.ImportRoleOfferDetailsAsync(file));
        }
    }
}
