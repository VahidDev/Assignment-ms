using Assignment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IDashboardServices _dashboardServices;

        public DashboardsController(IDashboardServices dashboardServices)
        {
            _dashboardServices = dashboardServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInfoAsync()
        {
            return await _dashboardServices.GetAllInfoAsync(); 
        }

        [HttpGet("getroleoffers")]
        public async Task<IActionResult> GetAllInfoAsync([FromBody] int[] roleOfferIds)
        {
            return await _dashboardServices.GetRoleOffersAsync(roleOfferIds); 
        }
    }
}
