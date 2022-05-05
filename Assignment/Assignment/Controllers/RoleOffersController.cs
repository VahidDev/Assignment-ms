using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleOffersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleOffersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllRoleOffers")]
        public async Task<IActionResult> GetALlRoleOffersAsync()
        {
            try
            {
                return Ok(await _unitOfWork.RoleOfferRepository.GetAllAsync());
            }
            catch (Exception)
            {
                return Ok("Something went wront");
            }
        }
        [HttpGet("GetRoleOffer")]
        public async Task<IActionResult> GetRoleOfferAsync([FromRoute]int id)
        {
            try
            {
                return Ok(await _unitOfWork.RoleOfferRepository.FindByIdAsync(id));
            }
            catch (Exception)
            {
                return Ok("Something went wrong");
            }
        }
    }
}
