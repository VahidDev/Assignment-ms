using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.DAL;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ValuesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("test");
        }
    }
}
