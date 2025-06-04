using Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnderhoudsbuddyWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly OnderhoudsbuddyDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TestController(OnderhoudsbuddyDbContext DbContext, IWebHostEnvironment env)
        {
            _context = DbContext;
            _env = env;
        }

        [HttpPost("cleanupdotnet")]
        public async Task<IActionResult> Cleanup()
        {
            if (_env.EnvironmentName == "Test")
            {
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE `UserCars`");
                await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE `Cars`");

                await _context.SaveChangesAsync();

                return Ok();
            }

            return Forbid();
        }
    }
}
