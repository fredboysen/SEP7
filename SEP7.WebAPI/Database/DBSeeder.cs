using SEP7.WebAPI.Models;
using SEP7.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace SEP7.WebAPI.Data
{
    public class DbSeeder
    {
        public static async Task Seed(ApplicationDB context)
        {
            
            context.Database.ExecuteSqlRaw("PRAGMA foreign_keys = ON;");

           
            var existingProduct = await context.Products
                                                .FirstOrDefaultAsync(p => p.ProductID == "4000-10-10");

            if (existingProduct == null)
            {
                
                var product = new Product
                {
                    ProductID = "4000-10-10", 
                    ProductName = "Toiletpaperholder stainless steel",
                    ImageUrl = "Image1.jpg"
                };

                
                context.Products.Add(product);
                await context.SaveChangesAsync();  

                   var materialTotal = new MaterialsTotal
                {
                    ProductID = product.ProductID, 
                    MaterialId = 1,
                    MaterialName = "Steel",
                    ADP_Fossil_MJ = 28769,
                    ADP_Minerals_Metals = 0.01720,
                    AP_Mol_H_Eq = 0.22480,
                    E_Fi_CTUe = 413.33,
                    E_Fm_CTUe = 4783.79,
                    E_Fo_CTUe = 7.55990,
                    EP_Terrestrial_Mol_N_Eq = 0.61630,
                    EP_Freshwater_kg_P = 6.37080,
                    EP_Marine_kg_N = 0.04980,
                    ETP_FW_CTUe = 5.29500,
                    GWP_Biogenic_kg_CO2 = 0.40510,
                    GWP_Fossil_kg_CO2 = 19.6,
                    GWP_LULUC_kg_CO2 = 0.04567,
                    GWP_Total_kg_CO2 = 20.06569,
                    HT_CI_CTUh = 0,
                    HT_CM_CTUh = 0.00021,
                    HT_CO_CTUh = 0.00010,
                    HT_NCI_CTUh = 0.0002,
                    HT_NCM_CTUh = 0.0001,
                    HT_NCO_CTUh = 0.96000,
                    HTTP_C_CTUh = 0.00034,
                    HTTP_NC_CTUh = 0.00010,
                    IRP_kBq_U235 = 1.64705,
                    LU_Pt = 281.0320,
                    ODP_kg_CFC11 = 0.000248,
                    PM_Disease_Inc = 0.000342,
                    POCP_kg_NMVOC = 0.28662,
                    WDP_m3_Depriv = 88.4937846
                };

              
                context.MaterialsTotals.Add(materialTotal);
                await context.SaveChangesAsync();  

                var materialData = new MaterialData
                {
                    ProductID = product.ProductID, 
                    MaterialId = materialTotal.MaterialId, 
                    MaterialType = "Stainless Steel",
                    TotalWeight = 100.5f
                };

                context.MatData.Add(materialData);
                await context.SaveChangesAsync();  
            }
            else
            {
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
            await context.SaveChangesAsync();  

           
             var usageData = new[]
            {
                new HQ_Usage
                {
                    Year = "2019",
                    EnergyConsumption = 6771265,
                    Electricity = 3937289,
                    Solarpanels = 1761584,
                    DistrictHeating = 59029,
                    NaturalGas = 24664,
                    Propan = 415970,
                    Hydrogen = 0,
                    Oil = 217350,
                    Petrol = 355380,
                    Diesel = 0
                },
                new HQ_Usage
                {
                    Year = "2020",
                    EnergyConsumption = 6449223,
                    Electricity = 4106597,
                    Solarpanels = 95230,
                    DistrictHeating = 1525183,
                    NaturalGas = 53858,
                    Propan = 219239,
                    Hydrogen = 0,
                    Oil = 156150,
                    Petrol = 267403,
                    Diesel = 0
                },
                new HQ_Usage
                {
                    Year = "2021",
                    EnergyConsumption = 6793259,
                    Electricity = 4293922,
                    Solarpanels = 271800,
                    DistrictHeating = 1724617,
                    NaturalGas = 60912,
                    Propan = 24120,
                    Hydrogen = 0,
                    Oil = 196491,
                    Petrol = 221397,
                    Diesel = 0
                },
                new HQ_Usage
                {
                    Year = "2022",
                    EnergyConsumption = 5937258,
                    Electricity = 4218564,
                    Solarpanels = 22104,
                    DistrictHeating = 209950,
                    NaturalGas = 757832,
                    Propan = 51290,
                    Hydrogen = 24867,
                    Oil = 248450,
                    Petrol = 204071,
                    Diesel = 222234
                },
                new HQ_Usage
                {
                    Year = "2023",
                    EnergyConsumption = 5211447,
                    Electricity = 5211447,
                    Solarpanels = 517875,
                    DistrictHeating = 216760,
                    NaturalGas = 94644,
                    Propan = 40224,
                    Hydrogen = 23623,
                    Oil = 389551,
                    Petrol = 316558,
                    Diesel = 132480
                }
            };

            foreach (var usage in usageData)
            {
                
                var existingUsage = await context.HQ_Usages
                    .FirstOrDefaultAsync(u => u.Year == usage.Year);

                if (existingUsage == null)
                {
                    
                    context.HQ_Usages.Add(usage);
                    await context.SaveChangesAsync();
                }
                else
                {
                    
                    existingUsage.Electricity = usage.Electricity;
                    existingUsage.Solarpanels = usage.Solarpanels;
                    existingUsage.DistrictHeating = usage.DistrictHeating;
                    existingUsage.NaturalGas = usage.NaturalGas;
                    existingUsage.Propan = usage.Propan;
                    existingUsage.Hydrogen = usage.Hydrogen;
                    existingUsage.Oil = usage.Oil;
                    existingUsage.Petrol = usage.Petrol;
                    existingUsage.Diesel = usage.Diesel;

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}