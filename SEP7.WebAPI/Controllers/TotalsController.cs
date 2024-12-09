using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP7.Database.Data;
using SEP7.WebAPI.Models;

namespace SEP7.WebAPI.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalsController : ControllerBase
    {
        private readonly ApplicationDB _context;

        public TotalsController(ApplicationDB context)
        {
            _context = context;
        }

[HttpGet("GetTotals")]
public async Task<ActionResult<List<MaterialsTotal>>> GetMaterialsTotals()
{
    var materialsTotals = await _context.MaterialsTotals.Include(m => m.Product).ToListAsync();

    if (materialsTotals == null || !materialsTotals.Any())
    {
        return NotFound("No materials totals found.");
    }

    // Directly return the list without any wrapper
    return Ok(materialsTotals);
}
}

        }
    


