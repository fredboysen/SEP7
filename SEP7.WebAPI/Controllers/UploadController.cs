using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP7.WebAPI.Models;
using CsvHelper;
using CsvHelper.Configuration;
using SEP7.Database.Data;

namespace SEP7.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly ApplicationDB _context;

        public UploadController(ApplicationDB context)
        {
            _context = context;
        }

        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv()
        {
            var file = Request.Form.Files[0];

            if (file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // User provides product details before uploading the file
            var productName = Request.Form["productName"];
            var productId = int.Parse(Request.Form["productId"]);

            // First, create the product entry
            var newProduct = new Product
            {
                ProductID = productId,
                ProductName = productName,
            };

            _context.Products.Add(newProduct);

            var newMaterialTotal = new MaterialsTotal
            {
                ProductID = productId
            };

            _context.MaterialsTotals.Add(newMaterialTotal);
            await _context.SaveChangesAsync();  

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<MaterialData>(); 

                foreach (var record in records)
                {
                    var materialData = new MaterialData
                    {
                        ProductID = productId,
                        MaterialId = record.MaterialId,
                        MaterialType = record.MaterialType,
                        TotalWeight = record.TotalWeight
                    };

                    _context.MatData.Add(materialData); 
            }

            await _context.SaveChangesAsync(); 

            return Ok("CSV data uploaded and product created successfully.");
        }
    }


    }
}
