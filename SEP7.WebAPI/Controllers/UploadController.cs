using Microsoft.AspNetCore.Mvc;

using SEP7.WebAPI.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using SEP7.Database.Data;

namespace SEP7.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ApplicationDB _context;
        private readonly ILogger<ImportController> _logger;

        public ImportController(ApplicationDB context, ILogger<ImportController> logger)
        {
            _context = context;
            _logger = logger;
        }

       [HttpPost("upload")]
public async Task<IActionResult> UploadCsv(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        return BadRequest("No file uploaded.");
    }

    try
    {
        // Parse the CSV file
        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var records = csv.GetRecords<dynamic>().ToList();

            foreach (var record in records)
            {
                string tableType = record.Table?.ToString(); // Table column is used to determine the type

                if (tableType == "Product")
                {
                    // If it's a Product row
                    var product = new Product
                    {
                        ProductID = record.ProductID?.ToString(),
                        ProductName = record.ProductName?.ToString()
                    };
                    _context.Products.Add(product);
                }
                else if (tableType == "MaterialsTotal")
                {
                    // If it's a MaterialsTotal row
                    // Parse MaterialId as an integer
                    int materialId;
                    if (!int.TryParse(record.MaterialId?.ToString(), out materialId))
                    {
                        _logger.LogWarning($"Skipping MaterialsTotal row with invalid MaterialId: {record.MaterialId}");
                        continue; // Skip invalid rows
                    }

                    var materialTotal = new MaterialsTotal
                    {
                        MaterialId = materialId,
                        MaterialName = record.MaterialName?.ToString(),
                        ADP_Fossil_MJ = ParseFloat(record.ADP_Fossil_MJ),
                        ADP_Minerals_Metals = ParseFloat(record.ADP_Minerals_Metals),
                        AP_Mol_H_Eq = ParseFloat(record.AP_Mol_H_Eq),
                        E_Fi_CTUe = ParseFloat(record.E_Fi_CTUe),
                        E_Fm_CTUe = ParseFloat(record.E_Fm_CTUe),
                        E_Fo_CTUe = ParseFloat(record.E_Fo_CTUe),
                        EP_Terrestrial_Mol_N_Eq = ParseFloat(record.EP_Terrestrial_Mol_N_Eq),
                        EP_Freshwater_kg_P = ParseFloat(record.EP_Freshwater_kg_P),
                        EP_Marine_kg_N = ParseFloat(record.EP_Marine_kg_N),
                        ETP_FW_CTUe = ParseFloat(record.ETP_FW_CTUe),
                        GWP_Biogenic_kg_CO2 = ParseFloat(record.GWP_Biogenic_kg_CO2),
                        GWP_Fossil_kg_CO2 = ParseFloat(record.GWP_Fossil_kg_CO2),
                        GWP_LULUC_kg_CO2 = ParseFloat(record.GWP_LULUC_kg_CO2),
                        GWP_Total_kg_CO2 = ParseFloat(record.GWP_Total_kg_CO2),
                        HT_CI_CTUh = ParseFloat(record.HT_CI_CTUh),
                        HT_CM_CTUh = ParseFloat(record.HT_CM_CTUh),
                        HT_CO_CTUh = ParseFloat(record.HT_CO_CTUh),
                        HT_NCI_CTUh = ParseFloat(record.HT_NCI_CTUh),
                        HT_NCM_CTUh = ParseFloat(record.HT_NCM_CTUh),
                        HT_NCO_CTUh = ParseFloat(record.HT_NCO_CTUh),
                        HTTP_C_CTUh = ParseFloat(record.HTTP_C_CTUh),
                        HTTP_NC_CTUh = ParseFloat(record.HTTP_NC_CTUh),
                        IRP_kBq_U235 = ParseFloat(record.IRP_kBq_U235),
                        LU_Pt = ParseFloat(record.LU_Pt),
                        ODP_kg_CFC11 = ParseFloat(record.ODP_kg_CFC11),
                        PM_Disease_Inc = ParseFloat(record.PM_Disease_Inc),
                        POCP_kg_NMVOC = ParseFloat(record.POCP_kg_NMVOC),
                        WDP_m3_Depriv = ParseFloat(record.WDP_m3_Depriv)
                    };
                    _context.MaterialsTotals.Add(materialTotal);
                }
                else if (tableType == "MaterialData")
                {
                    // If it's a MaterialData row
                    int materialId;
                    if (!int.TryParse(record.MaterialId?.ToString(), out materialId))
                    {
                        _logger.LogWarning($"Skipping MaterialData row with invalid MaterialId: {record.MaterialId}");
                        continue; // Skip invalid rows
                    }

                    var materialData = new MaterialData
                    {
                        ProductID = record.ProductID?.ToString(),
                        MaterialId = materialId,
                        MaterialType = record.MaterialType?.ToString(),
                        TotalWeight = ParseFloat(record.TotalWeight)
                    };
                    _context.MatData.Add(materialData);
                }
            }

            await _context.SaveChangesAsync();
        }

        return Ok("Data successfully imported.");
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error importing data: {ex.Message}");
        return StatusCode(500, "Internal server error");
    }
}

// Helper method to parse float and handle missing values
private float ParseFloat(object value)
{
    return float.TryParse(value?.ToString(), out var result) ? result : 0f; // Default to 0 if parsing fails
}

    }
}
