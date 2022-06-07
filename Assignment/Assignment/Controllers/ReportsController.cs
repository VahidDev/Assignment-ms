using Assignment.Constants.RoutingConstants;
using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route($"{RoutingConstants.Routing}/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportServices _reportServices;

        public ReportsController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReportAsync()
        {
            return await _reportServices.GetAllReportsAsync();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReportAsync([FromBody] CreateReportDto dto)
        {
            return await _reportServices.CreateReportAsync(dto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateeReportAsync([FromBody] UpdateReportDto dto)
        {
            return await _reportServices.UpdateReportAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
        {
            return await _reportServices.DeleteByIdAsync(id);
        }
    }
}
