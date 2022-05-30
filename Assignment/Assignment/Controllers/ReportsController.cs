using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> CreateReportAsync(CreateReportDto dto)
        {
            return await _reportServices.CreateReportAsync(dto);
        }
    }
}
