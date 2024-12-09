using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP7.WebAPI.Models;
using SEP7.Database.Data;


namespace SEP7.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HQ_UsageController : ControllerBase
    {
        private readonly ApplicationDB _context;
        public HQ_UsageController(ApplicationDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HQ_Usage>>> GetHQ_Usages()
        {
            var hqUsages = await _context.HQ_Usages.ToListAsync();

            if (hqUsages == null || hqUsages.Count == 0)
            {
                return NotFound("No HQ Usage data found.");
            }

            return Ok(hqUsages);
        }

    }
}
