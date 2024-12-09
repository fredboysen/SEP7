using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP7.Database.Data;
using SEP7.WebAPI.Models;

namespace SEP7.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDB _context;

        public ProductController(ApplicationDB context)
        {
            _context = context;
        }


    [HttpGet("product/{productId}")]
public async Task<IActionResult> GetMaterialsTotalByProduct(string productId)
{
    var materialsTotals = await _context.MaterialsTotals
        .Where(mt => mt.ProductID == productId)
        .Include(mt => mt.Product)
        .ToListAsync();

    if (!materialsTotals.Any())
    {
        return NotFound("No MaterialsTotal found for this product.");
    }

            var result = materialsTotals.Select(mt => new
            {
                mt.MaterialId,
                mt.MaterialName,
                mt.ADP_Fossil_MJ,
                mt.ADP_Minerals_Metals,
                mt.AP_Mol_H_Eq,
                mt.E_Fi_CTUe,
                mt.E_Fm_CTUe,
                mt.E_Fo_CTUe,
                mt.EP_Terrestrial_Mol_N_Eq,
                mt.EP_Freshwater_kg_P,
                mt.EP_Marine_kg_N,
                mt.ETP_FW_CTUe,
                mt.GWP_Biogenic_kg_CO2,
                mt.GWP_Fossil_kg_CO2,
                mt.GWP_LULUC_kg_CO2,
                mt.GWP_Total_kg_CO2,
                mt.HT_CI_CTUh,
                mt.HT_CM_CTUh,
                mt.HT_CO_CTUh,
                mt.HT_NCI_CTUh,
                mt.HT_NCM_CTUh,
                mt.HT_NCO_CTUh,
                mt.HTTP_C_CTUh,
                mt.HTTP_NC_CTUh,
                mt.IRP_kBq_U235,
                mt.LU_Pt,
                mt.ODP_kg_CFC11,
                mt.PM_Disease_Inc,
                mt.POCP_kg_NMVOC,
                mt.WDP_m3_Depriv,
                Product = new
                {
                    mt.Product.ProductID,
                    mt.Product.ProductName
                }
            }).ToList();

            return Ok(result);
        }


  [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                // Retrieve all products from the database, including navigation properties if needed
                var products = await _context.Products
                    .Include(p => p.MaterialData) // Eager load MaterialData
                    .Include(p => p.MaterialsTotal) // Eager load MaterialsTotal
                    .ToListAsync();

                // Return the list of products
                return Ok(products);
            }
            catch (Exception ex)
            {
                // Log error (optional)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}