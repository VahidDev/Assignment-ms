using Assignment.Services.Abstraction;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleOffersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRoleOfferServices _roleOfferServices;
        public RoleOffersController(IUnitOfWork unitOfWork,IMapper mapper
            ,IRoleOfferServices roleOfferServices)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
            _roleOfferServices = roleOfferServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetALlRoleOffersAsync()
        {
            return Ok(_mapper.Map<IList<RoleOfferDto>>(await _unitOfWork
                .RoleOfferRepository
                .GetAllAsync(new List<string> { nameof(Venue) ,nameof(JobTitle)
                ,nameof(Location),nameof(FunctionalArea)})));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleOfferAsync(int id)
        {
            RoleOffer roleOffer = await _unitOfWork.RoleOfferRepository
                .FirstOrDefaultAsync(r=>r.Id==id&&!r.IsDeleted
                ,new List<string> { nameof(Venue) ,nameof(JobTitle)
                ,nameof(Location),nameof(FunctionalArea)});
            if (roleOffer == null) return NotFound();
            return Ok(_mapper.Map<RoleOfferDto>(roleOffer));
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportRoleOffersAsync([FromForm]IFormFile file)
        {
            if(file==null)return NotFound();
            return Ok(await _roleOfferServices
                .ValidateExcelFileThenWriteToDbAsync(file));
        }
    }
}
