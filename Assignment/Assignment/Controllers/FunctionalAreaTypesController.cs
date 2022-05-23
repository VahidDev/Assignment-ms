using Assignment.Services.Abstraction;
using Assignment.Utilities.ResponseUtilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
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
            return ResponseGenerator
                .GetResponse(await _functionalAreaTypeServices.GetAllIncludingItemsAsync());
        }
    }
}
