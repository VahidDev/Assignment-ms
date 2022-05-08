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
        public RoleOffersController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
        }
        [HttpGet("GetAllRoleOffers")]
        public async Task<IActionResult> GetALlRoleOffersAsync()
        {
            return Ok(_mapper.Map<IList<RoleOfferDto>>(await _unitOfWork
                .RoleOfferRepository.GetAllAsync()));
        }
        [HttpGet("GetRoleOffer/{id}")]
        public async Task<IActionResult> GetRoleOfferAsync(int id)
        {
            RoleOffer roleOffer = await _unitOfWork.RoleOfferRepository.FindByIdAsync(id);
            if (roleOffer == null) return NotFound();
            return Ok(_mapper.Map<RoleOfferDto>(roleOffer));
        }
    }
}
