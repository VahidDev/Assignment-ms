using Assignment.Constants.RoutingConstants;
using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route($"{RoutingConstants.Routing}/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateServices _templateServices;

        public TemplatesController(ITemplateServices templateServices)
        {
            _templateServices= templateServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return await _templateServices.GetAllTemplatesAsync();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync
            ([FromBody] CreateTemplateDto templates)
        {
            return await _templateServices.CreateAsync(templates);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateTemplateDto templateDto)
        {
            return await _templateServices.UpdateAsync(templateDto);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await _templateServices.DeleteAsync(id);
        }
    }
}
