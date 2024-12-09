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
            
            // Add Users data
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
            await context.SaveChangesAsync();  // Save User records

            // Step 4: Seed the HQ_Usage data
      var usageData = new[]
            {
                new HQ_Usage
                {
                    UsageType = "Electricity",
                    Year = "2023",
                    EnergyConsumption = 3509473,
                    EnergyType = "kWh",
                    EnergyCost = 3796540,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 237,
                    UnitPrice = 1.08
                },
                new HQ_Usage
                {
                    UsageType = "Solarpanels",
                    Year = "2023",
                    EnergyConsumption = 517875,
                    EnergyType = "kWh",
                    EnergyCost = 0,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 0,
                    UnitPrice = 0
                },
                new HQ_Usage
                {
                    UsageType = "DistrictHeating",
                    Year = "2023",
                    EnergyConsumption = 217,
                    EnergyType = "MWh",
                    EnergyCost = 115490,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 15,
                    UnitPrice = 0.53
                },
                new HQ_Usage
                {
                    UsageType = "Natural Gas",
                    Year = "2023",
                    EnergyConsumption = 8604,
                    EnergyType = "Nm_2",
                    EnergyCost = 44100,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 19,
                    UnitPrice = 0.47
                },
                new HQ_Usage
                {
                    UsageType = "Propan",
                    Year = "2023",
                    EnergyConsumption = 3148,
                    EnergyType = "kg",
                    EnergyCost = 36490,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 93,
                    UnitPrice = 0.91
                },
                new HQ_Usage
                {
                    UsageType = "Hydrogen",
                    Year = "2023",
                    EnergyConsumption = 708,
                    EnergyType = "kg",
                    EnergyCost = 332380,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 0,
                    UnitPrice = 14.07
                },
                new HQ_Usage
                {
                    UsageType = "Oil",
                    Year = "2023",
                    EnergyConsumption = 38353,
                    EnergyType = "Litres",
                    EnergyCost = 414127,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 104,
                    UnitPrice = 1.06
                },
                new HQ_Usage
                {
                    UsageType = "Petrol",
                    Year = "2023",
                    EnergyConsumption = 34672,
                    EnergyType = "Litres",
                    EnergyCost = 420151,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 83,
                    UnitPrice = 1.33
                },
                new HQ_Usage
                {
                    UsageType = "Diesel",
                    Year = "2023",
                    EnergyConsumption = 13301,
                    EnergyType = "litres",
                    EnergyCost = 147263,
                    Currency = "DKK",
                    Co2_Emissions_Tons = 35,
                    UnitPrice = 11.11
                }
            };

            foreach (var usage in usageData)
            {
                // Check if the record already exists
                var existingUsage = await context.HQ_Usages
                    .FirstOrDefaultAsync(u => u.UsageType == usage.UsageType && u.Year == usage.Year);

                if (existingUsage == null)
                {
                    // Add the new record if it does not exist
                    context.HQ_Usages.Add(usage);
                    await context.SaveChangesAsync();
                }
                else
                {
                    // Optionally update the existing record if needed
                    existingUsage.EnergyConsumption = usage.EnergyConsumption;
                    existingUsage.EnergyType = usage.EnergyType;
                    existingUsage.EnergyCost = usage.EnergyCost;
                    existingUsage.Currency = usage.Currency;
                    existingUsage.Co2_Emissions_Tons = usage.Co2_Emissions_Tons;
                    existingUsage.UnitPrice = usage.UnitPrice;

                    await context.SaveChangesAsync();
                }
            }
    }
}
}
