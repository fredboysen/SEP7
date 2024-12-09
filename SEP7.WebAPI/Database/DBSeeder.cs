using SEP7.WebAPI.Models;
using SEP7.Database.Data;
using Microsoft.EntityFrameworkCore;


namespace SEP7.WebAPI.Data
{
    public class DbSeeder
    {
        public static async Task Seed(ApplicationDB context)
        {
            // Ensure foreign keys are enabled for SQLite
            context.Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");

            // Check if the product with the given ProductID already exists
            var existingProduct = await context.Products
                                                .FirstOrDefaultAsync(p => p.ProductID == "4000-10-10");

            if (existingProduct == null)
            {
                // Step 1: Create the Product record
                var product = new Product
                {
                    ProductID = "4000-10-10", // Product ID
                    ProductName = "Toiletpaperholder stainless steel",
                    ImageUrl = "Image1.jpg"
                };

                // Add the product to the Products table
                context.Products.Add(product);
                await context.SaveChangesAsync();  // Save changes to create the Product

                // Step 2: Create the MaterialsTotal record
                var materialTotal = new MaterialsTotal
                {
                    ProductID = product.ProductID, // Link to the Product
                    MaterialId = 1,
                    MaterialName = "Steel",
                    ADP_Fossil_MJ = 100.5f,
                    ADP_Minerals_Metals = 200.0f,
                    AP_Mol_H_Eq = 0.5f,
                    E_Fi_CTUe = 1.2f,
                    E_Fm_CTUe = 1.8f,
                    E_Fo_CTUe = 0.9f,
                    EP_Terrestrial_Mol_N_Eq = 0.8f,
                    EP_Freshwater_kg_P = 0.4f,
                    EP_Marine_kg_N = 0.3f,
                    ETP_FW_CTUe = 2.5f,
                    GWP_Biogenic_kg_CO2 = 3.6f,
                    GWP_Fossil_kg_CO2 = 4.5f,
                    GWP_LULUC_kg_CO2 = 2.3f,
                    GWP_Total_kg_CO2 = 10.4f,
                    HT_CI_CTUh = 0.7f,
                    HT_CM_CTUh = 0.5f,
                    HT_CO_CTUh = 1.0f,
                    HT_NCI_CTUh = 1.2f,
                    HT_NCM_CTUh = 0.9f,
                    HT_NCO_CTUh = 1.3f,
                    HTTP_C_CTUh = 0.2f,
                    HTTP_NC_CTUh = 0.3f,
                    IRP_kBq_U235 = 0.1f,
                    LU_Pt = 0.0f,
                    ODP_kg_CFC11 = 0.05f,
                    PM_Disease_Inc = 0.2f,
                    POCP_kg_NMVOC = 0.1f,
                    WDP_m3_Depriv = 2.0f
                };

                // Add the material total to the MaterialsTotal table
                context.MaterialsTotals.Add(materialTotal);
                await context.SaveChangesAsync();  // Save changes to create MaterialsTotal

                // Step 3: Create the MaterialData record
                var materialData = new MaterialData
                {
                    ProductID = product.ProductID, // Link to the Product
                    MaterialId = materialTotal.MaterialId, // Link to the MaterialsTotal
                    MaterialType = "Stainless Steel",
                    TotalWeight = 100.5f
                };

                // Add the material data to the MaterialData table
                context.MatData.Add(materialData);
                await context.SaveChangesAsync();  // Save MaterialData record
            }
            else
            {
                // Handle the case where the product already exists, if necessary
                Console.WriteLine($"Product with ID {"4000-10-10"} already exists.");
            }
    
     var users = new[]
            {
                new User
                {
                    username = "testuser",
                    email = "testuser@example.com",
                    role = "user",
                    password = "test123"
                },
                new User
                {
                    username = "testadmin",
                    email = "testadmin@example.com",
                    role = "admin",
                    password = "test123"
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    
    
    
    }

    
}
