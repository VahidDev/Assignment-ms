using Assignment.Constants.RoutingConstants;
using Assignment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route($"{RoutingConstants.Routing}/[controller]")]
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
            return await _roleOfferServices.GetAllRoleOffersAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleOfferAsync(int id)
        {
            return await _roleOfferServices.GetRoleOfferAsync(id);
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportRoleOffersAsync([FromForm]IFormFile file)
        {
            return await _roleOfferServices.ValidateExcelFileThenWriteToDbAsync(file);
        }
        [HttpPost("importDetails")]
        public async Task<IActionResult> ImportRoleOfferDetailsAsync([FromForm] IFormFile file)
        {
            return await _roleOfferServices.ImportRoleOfferDetailsAsync(file);
        }
    }
}
