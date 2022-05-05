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
        [HttpGet]
        public async Task<IActionResult> GetALlAsync()
        {
            try
            {
                return Ok(await _unitOfWork.RoleOfferRepository.GetAllAsync());
            }
            catch (Exception)
            {
                return Ok("Something bad happened");
            }
        }
    }
}
