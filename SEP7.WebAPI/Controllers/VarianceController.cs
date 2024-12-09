using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP7.Database.Data;
using SEP7.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEP7.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialVarianceController : ControllerBase
    {
        private readonly ApplicationDB _context;

        public MaterialVarianceController(ApplicationDB context)
        {
            _context = context;
        }

        [HttpGet("variance")]
        public async Task<IActionResult> GetMaterialVariance([FromQuery] string productId = null)
        {
            // Define the fields we want to calculate min, max, and percentage variance for
            var fields = new string[]
            {
                "ADP_Fossil_MJ", "ADP_Minerals_Metals", "AP_Mol_H_Eq", "E_Fi_CTUe", "E_Fm_CTUe", 
                "E_Fo_CTUe", "EP_Terrestrial_Mol_N_Eq", "EP_Freshwater_kg_P", "EP_Marine_kg_N", 
                "ETP_FW_CTUe", "GWP_Biogenic_kg_CO2", "GWP_Fossil_kg_CO2", "GWP_LULUC_kg_CO2", 
                "GWP_Total_kg_CO2", "HT_CI_CTUh", "HT_CM_CTUh", "HT_CO_CTUh", "HT_NCI_CTUh", 
                "HT_NCM_CTUh", "HT_NCO_CTUh", "HTTP_C_CTUh", "HTTP_NC_CTUh", "IRP_kBq_U235", 
                "LU_Pt", "ODP_kg_CFC11", "PM_Disease_Inc", "POCP_kg_NMVOC", "WDP_m3_Depriv"
            };

            // Filter the query for MaterialsTotal based on the productId provided (or all if not provided)
            IQueryable<MaterialsTotal> query = _context.MaterialsTotals;

            if (!string.IsNullOrEmpty(productId))
            {
                // If a productId is provided, filter for ProductIDs that start with the input value
                query = query.Where(mt => mt.ProductID.StartsWith(productId));
            }

            var result = new List<MaterialVarianceResult>();

            // Loop through each field and calculate min, max, and percentage variance
            foreach (var field in fields)
            {
                // Dynamically generate min, max, and percentage variance for each field
                var minValue = await query.MinAsync(mt => EF.Property<float>(mt, field));
                var maxValue = await query.MaxAsync(mt => EF.Property<float>(mt, field));

                // Calculate percentage variance: (max - min) / min * 100
                var percentageVariance = minValue != 0 ? ((maxValue - minValue) / minValue) * 100 : 0;

                result.Add(new MaterialVarianceResult
                {
                    FieldName = field,
                    MinValue = minValue,
                    MaxValue = maxValue,
                    PercentageVariance = percentageVariance
                });
            }

            return Ok(result);
        }
    }
}
