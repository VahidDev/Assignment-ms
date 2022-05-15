using Assignment.Services.Abstraction;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateServices _templateServices;
        private readonly IUnitOfWork _unitOfWork;

        public TemplatesController(ITemplateServices templateServices
            ,IUnitOfWork unitOfWork)
        {
            _templateServices= templateServices;
            _unitOfWork= unitOfWork;    
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync
            ([FromBody] IReadOnlyCollection<CreateTemplateDto>templates)
        {
            if (templates.Count == 0) return Ok(false);
            return Ok(await _templateServices.CreateAsync(templates));
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateTemplateDto templateDto)
        {
            if (!await _unitOfWork.TemplateRepository
                .AnyAsync(t=>t.Id==templateDto.Id)) return NotFound();
            return Ok(await _templateServices.UpdateAsync(templateDto));
        }
    }
}
