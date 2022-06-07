using Assignment.Constants.RoutingConstants;
using Assignment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route($"{RoutingConstants.Routing}/[controller]")]
    [ApiController]
    public class FunctionalAreaTypesController : ControllerBase
    {
        private readonly IFunctionalAreaTypeServices _functionalAreaTypeServices;

        public FunctionalAreaTypesController(IFunctionalAreaTypeServices functionalAreaTypeServices)
        {
            _functionalAreaTypeServices = functionalAreaTypeServices;
        }

        [HttpGet("nested")]
        public async Task<IActionResult> GetAllIncludingItemsAsync()
        {
            return await _functionalAreaTypeServices.GetAllIncludingItemsAsync();
        }
    }
}
