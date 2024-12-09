using Microsoft.AspNetCore.Mvc;
using SEP7.WebAPI.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;
using System.Globalization;
using SEP7.Database.Data;
using System.IO;

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
public async Task<IActionResult> UploadCsvAndImage(IFormFile file, IFormFile imageFile)
{
    if (file == null || file.Length == 0)
    {
        return BadRequest("No file uploaded.");
    }

    try
    {
        // Define the path where you want to store images
        var imageUrl = string.Empty;
        if (imageFile != null && imageFile.Length > 0)
        {
            // Save image and get URL
            imageUrl = await SaveImageAsync(imageFile);
        }

        // Parse the CSV file
        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            var records = csv.GetRecords<dynamic>().ToList();
            
            // Loop through each record
            foreach (var record in records)
            {
                string tableType = record.Table?.ToString(); // Table column is used to determine the type

                if (tableType == "Product")
                {
                    // If it's a Product row
                    var product = new Product
                    {
                        ProductID = record.ProductID?.ToString(),
                        ProductName = record.ProductName?.ToString(),
                        ImageUrl = imageUrl // Save the image URL in the Product table
                    };

                    // Add the new Product to the database
                    _context.Products.Add(product);
                }
                else if (tableType == "MaterialsTotal")
                {
                    // If it's a MaterialsTotal row
                    int materialId;
                    if (!int.TryParse(record.MaterialId?.ToString(), out materialId))
                    {
                        _logger.LogWarning($"Skipping MaterialsTotal row with invalid MaterialId: {record.MaterialId}");
                        continue; // Skip invalid rows
                    }

                    // Check if the MaterialId already exists
                    var existingMaterial = await _context.MaterialsTotals
                                                         .FirstOrDefaultAsync(m => m.MaterialId == materialId);

                    if (existingMaterial != null)
                    {
                        // Update the existing MaterialsTotal record
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
                        // Insert new MaterialsTotal record
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

private async Task<string> SaveImageAsync(IFormFile imageFile)
{
    try
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
        var fullFilePath = Path.Combine(filePath, uniqueFileName);

        using (var stream = new FileStream(fullFilePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return $"/images/{uniqueFileName}";
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error uploading image: {ex.Message}");
        return string.Empty; // Return empty if there's an error saving the image
    }
}

private float ParseFloat(object value)
{
    return float.TryParse(value?.ToString(), out var result) ? result : 0f; // Default to 0 if parsing fails
}

    }
}