using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP7.Database.Data;

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


 [HttpGet("{materialId}")]
        public async Task<IActionResult> GetMaterialsTotal(int materialId)
        {
            var materialsTotal = await _context.MaterialsTotals
                .Include(mt => mt.Product)  
                .FirstOrDefaultAsync(mt => mt.MaterialId == materialId);

            if (materialsTotal == null)
            {
                return NotFound("MaterialsTotal not found.");
            }

            var result = new
            {
                materialsTotal.MaterialId,
                materialsTotal.MaterialName,
                materialsTotal.ADP_Fossil_MJ,
                materialsTotal.ADP_Minerals_Metals,
                materialsTotal.AP_Mol_H_Eq,
                materialsTotal.E_Fi_CTUe,
                materialsTotal.E_Fm_CTUe,
                materialsTotal.E_Fo_CTUe,
                materialsTotal.EP_Terrestrial_Mol_N_Eq,
                materialsTotal.EP_Freshwater_kg_P,
                materialsTotal.EP_Marine_kg_N,
                materialsTotal.ETP_FW_CTUe,
                materialsTotal.GWP_Biogenic_kg_CO2,
                materialsTotal.GWP_Fossil_kg_CO2,
                materialsTotal.GWP_LULUC_kg_CO2,
                materialsTotal.GWP_Total_kg_CO2,
                materialsTotal.HT_CI_CTUh,
                materialsTotal.HT_CM_CTUh,
                materialsTotal.HT_CO_CTUh,
                materialsTotal.HT_NCI_CTUh,
                materialsTotal.HT_NCM_CTUh,
                materialsTotal.HT_NCO_CTUh,
                materialsTotal.HTTP_C_CTUh,
                materialsTotal.HTTP_NC_CTUh,
                materialsTotal.IRP_kBq_U235,
                materialsTotal.LU_Pt,
                materialsTotal.ODP_kg_CFC11,
                materialsTotal.PM_Disease_Inc,
                materialsTotal.POCP_kg_NMVOC,
                materialsTotal.WDP_m3_Depriv,
                Product = new
                {
                    materialsTotal.Product.ProductID,
                    materialsTotal.Product.ProductName
                }
            };

            return Ok(result);
        }


    }
}