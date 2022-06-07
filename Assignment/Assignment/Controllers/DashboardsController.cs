using Assignment.Constants.RoutingConstants;
using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [ApiController]
    [Route($"{RoutingConstants.Routing}/[controller]")]
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

        [HttpPost("getroleoffers")]
        public async Task<IActionResult> GetAllInfoAsync([FromBody] int[] roleOfferIds)
        {
            return await _dashboardServices.GetRoleOffersAsync(roleOfferIds); 
        }

        [HttpPost("getvolunteersinfo")]
        public async Task<IActionResult> GetVolunteersInfoAsync
            ([FromBody] RoleOfferVolunteerDto dto)
        {
            return await _dashboardServices.GetVolunteersInfoAsync(dto);
        }
    }
}
