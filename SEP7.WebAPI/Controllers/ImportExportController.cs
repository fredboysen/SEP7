using Microsoft.AspNetCore.Mvc;
using SEP7.WebAPI.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

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
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IConfiguration _configuration;

      public ImportController(ApplicationDB context, ILogger<ImportController> logger, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment; 
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsvAndImage(IFormFile file, IFormFile imageFile)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                var webAppImagePath = _configuration["WebAppImagePath"];
                if (string.IsNullOrEmpty(webAppImagePath))
                {
                    return StatusCode(500, "WebApp image path is not configured.");
                }

                var imageUrl = string.Empty;
                if (imageFile != null && imageFile.Length > 0)
                {
                    imageUrl = await SaveImageToWebAppAsync(imageFile, webAppImagePath);
                }

                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var records = csv.GetRecords<dynamic>().ToList();

                    foreach (var record in records)
                    {
                        string tableType = record.Table?.ToString();

                        if (tableType == "Product")
                        {
                            var product = new Product
                            {
                                ProductID = record.ProductID?.ToString(),
                                ProductName = record.ProductName?.ToString(),
                                ImageUrl = imageUrl
                            };
                            _context.Products.Add(product);
                        }
                        else if (tableType == "MaterialsTotal")
                        {
                            int materialId;
                            if (!int.TryParse(record.MaterialId?.ToString(), out materialId))
                            {
                                _logger.LogWarning($"Skipping MaterialsTotal row with invalid MaterialId: {record.MaterialId}");
                                continue;
                            }

                            var existingMaterial = await _context.MaterialsTotals
                                .FirstOrDefaultAsync(m => m.MaterialId == materialId);

                            if (existingMaterial != null)
                            {
                                existingMaterial.MaterialName = record.MaterialName?.ToString();
                                existingMaterial.ADP_Fossil_MJ = ParseFloat(record.ADP_Fossil_MJ);
                                existingMaterial.ADP_Minerals_Metals = ParseFloat(record.ADP_Minerals_Metals);
                                existingMaterial.AP_Mol_H_Eq = ParseFloat(record.AP_Mol_H_Eq);
                                existingMaterial.E_Fi_CTUe = ParseFloat(record.E_Fi_CTUe);
                                existingMaterial.E_Fm_CTUe = ParseFloat(record.E_Fm_CTUe);
                                existingMaterial.E_Fo_CTUe = ParseFloat(record.E_Fo_CTUe);
                                existingMaterial.EP_Terrestrial_Mol_N_Eq = ParseFloat(record.EP_Terrestrial_Mol_N_Eq);
                                existingMaterial.EP_Freshwater_kg_P = ParseFloat(record.EP_Freshwater_kg_P);
                                existingMaterial.EP_Marine_kg_N = ParseFloat(record.EP_Marine_kg_N);
                                existingMaterial.ETP_FW_CTUe = ParseFloat(record.ETP_FW_CTUe);
                                existingMaterial.GWP_Biogenic_kg_CO2 = ParseFloat(record.GWP_Biogenic_kg_CO2);
                                existingMaterial.GWP_Fossil_kg_CO2 = ParseFloat(record.GWP_Fossil_kg_CO2);
                                existingMaterial.GWP_LULUC_kg_CO2 = ParseFloat(record.GWP_LULUC_kg_CO2);
                                existingMaterial.GWP_Total_kg_CO2 = ParseFloat(record.GWP_Total_kg_CO2);
                                existingMaterial.HT_CI_CTUh = ParseFloat(record.HT_CI_CTUh);
                                existingMaterial.HT_CM_CTUh = ParseFloat(record.HT_CM_CTUh);
                                existingMaterial.HT_CO_CTUh = ParseFloat(record.HT_CO_CTUh);
                                existingMaterial.HT_NCI_CTUh = ParseFloat(record.HT_NCI_CTUh);
                                existingMaterial.HT_NCM_CTUh = ParseFloat(record.HT_NCM_CTUh);
                                existingMaterial.HT_NCO_CTUh = ParseFloat(record.HT_NCO_CTUh);
                                existingMaterial.HTTP_C_CTUh = ParseFloat(record.HTTP_C_CTUh);
                                existingMaterial.HTTP_NC_CTUh = ParseFloat(record.HTTP_NC_CTUh);
                                existingMaterial.IRP_kBq_U235 = ParseFloat(record.IRP_kBq_U235);
                                existingMaterial.LU_Pt = ParseFloat(record.LU_Pt);
                                existingMaterial.ODP_kg_CFC11 = ParseFloat(record.ODP_kg_CFC11);
                                existingMaterial.PM_Disease_Inc = ParseFloat(record.PM_Disease_Inc);
                                existingMaterial.POCP_kg_NMVOC = ParseFloat(record.POCP_kg_NMVOC);
                                existingMaterial.WDP_m3_Depriv = ParseFloat(record.WDP_m3_Depriv);
                            }
                            else
                            {
                                var materialTotal = new MaterialsTotal
                                {
                                    MaterialId = materialId,
                                    MaterialName = record.MaterialName?.ToString(),
                                    ProductID = record.ProductID?.ToString(),
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
                        }
                        else if (tableType == "MaterialData")
                        {
                            int materialId;
                            if (!int.TryParse(record.MaterialId?.ToString(), out materialId))
                            {
                                _logger.LogWarning($"Skipping MaterialData row with invalid MaterialId: {record.MaterialId}");
                                continue;
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

       private async Task<string> SaveImageToWebAppAsync(IFormFile imageFile, string webAppImagePath)
{
    try
    {
        var uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "../SEP7.WebApp/wwwroot");

        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var originalFileName = Path.GetFileName(imageFile.FileName);
        var fullFilePath = Path.Combine(uploadPath, originalFileName);

        using (var stream = new FileStream(fullFilePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return originalFileName;
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error uploading image: {ex.Message}");
        return string.Empty;
    }
}


      private float ParseFloat(object value)
{
    if (float.TryParse(value?.ToString(), out var result))
    {
        return (float)Math.Round(result, 4); // Round to 4 decimal places
    }
    return 0f;
}

[HttpGet("export-totals-and-variance")]
public async Task<IActionResult> ExportTotalsAndVarianceToCsv([FromQuery] string productGroup)
{
    if (string.IsNullOrEmpty(productGroup))
    {
        return BadRequest("Product group must be provided.");
    }

    // Step 1: Get Material Totals filtered by the product group (ProductID prefix)
    var materialTotalsQuery = _context.MaterialsTotals.AsQueryable();

    // Filter by the product group (ProductID starts with the provided product group prefix)
    materialTotalsQuery = materialTotalsQuery.Where(mt => mt.ProductID.StartsWith(productGroup));

    // Get the list of materials for the given product group
    var materialTotals = await materialTotalsQuery.ToListAsync();

    // Step 2: Define fields
    var fields = new string[]
    {
        "ADP_Fossil_MJ", "ADP_Minerals_Metals", "AP_Mol_H_Eq", "E_Fi_CTUe", "E_Fm_CTUe", 
        "E_Fo_CTUe", "EP_Terrestrial_Mol_N_Eq", "EP_Freshwater_kg_P", "EP_Marine_kg_N", 
        "ETP_FW_CTUe", "GWP_Biogenic_kg_CO2", "GWP_Fossil_kg_CO2", "GWP_LULUC_kg_CO2", 
        "GWP_Total_kg_CO2", "HT_CI_CTUh", "HT_CM_CTUh", "HT_CO_CTUh", "HT_NCI_CTUh", 
        "HT_NCM_CTUh", "HT_NCO_CTUh", "HTTP_C_CTUh", "HTTP_NC_CTUh", "IRP_kBq_U235", 
        "LU_Pt", "ODP_kg_CFC11", "PM_Disease_Inc", "POCP_kg_NMVOC", "WDP_m3_Depriv"
    };

    var varianceResults = new List<MaterialVarianceResult>();

    // Step 3: Calculate variance for each field
   foreach (var field in fields)
{
    // Get the min and max values for each field for the filtered data
    var minValue = materialTotals.Min(mt => GetFieldValue(mt, field));
    var maxValue = materialTotals.Max(mt => GetFieldValue(mt, field));

    var percentageVariance = minValue != 0 ? ((maxValue - minValue) / minValue) * 100 : 0;

    varianceResults.Add(new MaterialVarianceResult
    {
        FieldName = field,
        MinValue = minValue,
        MaxValue = maxValue,
        PercentageVariance = percentageVariance
    });
}
    // Step 4: Generate CSV data
    var csvData = new List<string>();

    // Header row for Material Totals and Variance data
    csvData.Add("ProductID," + string.Join(",", fields) + ",Variance_Field,Min_Value,Max_Value,Percentage_Variance");

    // Combine data for each row
    foreach (var material in materialTotals)
    {
        var materialValues = fields.Select(field => GetFieldValue(material, field).ToString()).ToArray();

        foreach (var variance in varianceResults)
        {
            // Combine material and variance data in a single row
            var row = $"{material.ProductID},{string.Join(",", materialValues)},{variance.FieldName},{variance.MinValue},{variance.MaxValue},{variance.PercentageVariance}";
            csvData.Add(row);
        }
    }

    var csvContent = string.Join("\n", csvData);

    // Step 5: Return CSV content as a file (using FileContentResult)
    var fileContent = new System.Text.UTF8Encoding().GetBytes(csvContent);
    return new FileContentResult(fileContent, "text/csv")
    {
        FileDownloadName = "MaterialData.csv"
    };
}

// Helper method to retrieve the field value dynamically
private double GetFieldValue(MaterialsTotal mt, string field)
{
    return field switch
    {
        "ADP_Fossil_MJ" => mt.ADP_Fossil_MJ,
        "ADP_Minerals_Metals" => mt.ADP_Minerals_Metals,
        "AP_Mol_H_Eq" => mt.AP_Mol_H_Eq,
        "E_Fi_CTUe" => mt.E_Fi_CTUe,
        "E_Fm_CTUe" => mt.E_Fm_CTUe,
        "E_Fo_CTUe" => mt.E_Fo_CTUe,
        "EP_Terrestrial_Mol_N_Eq" => mt.EP_Terrestrial_Mol_N_Eq,
        "EP_Freshwater_kg_P" => mt.EP_Freshwater_kg_P,
        "EP_Marine_kg_N" => mt.EP_Marine_kg_N,
        "ETP_FW_CTUe" => mt.ETP_FW_CTUe,
        "GWP_Biogenic_kg_CO2" => mt.GWP_Biogenic_kg_CO2,
        "GWP_Fossil_kg_CO2" => mt.GWP_Fossil_kg_CO2,
        "GWP_LULUC_kg_CO2" => mt.GWP_LULUC_kg_CO2,
        "GWP_Total_kg_CO2" => mt.GWP_Total_kg_CO2,
        "HT_CI_CTUh" => mt.HT_CI_CTUh,
        "HT_CM_CTUh" => mt.HT_CM_CTUh,
        "HT_CO_CTUh" => mt.HT_CO_CTUh,
        "HT_NCI_CTUh" => mt.HT_NCI_CTUh,
        "HT_NCM_CTUh" => mt.HT_NCM_CTUh,
        "HT_NCO_CTUh" => mt.HT_NCO_CTUh,
        "HTTP_C_CTUh" => mt.HTTP_C_CTUh,
        "HTTP_NC_CTUh" => mt.HTTP_NC_CTUh,
        "IRP_kBq_U235" => mt.IRP_kBq_U235,
        "LU_Pt" => mt.LU_Pt,
        "ODP_kg_CFC11" => mt.ODP_kg_CFC11,
        "PM_Disease_Inc" => mt.PM_Disease_Inc,
        "POCP_kg_NMVOC" => mt.POCP_kg_NMVOC,
        "WDP_m3_Depriv" => mt.WDP_m3_Depriv,
        _ => 0 // Default value if the field is not found
    };
}


    }
}
