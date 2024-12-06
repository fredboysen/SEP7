using Microsoft.EntityFrameworkCore;
using SEP7.WebAPI.Models;


namespace SEP7.Database.Data

{
  public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options) { }

        public DbSet<MaterialsTotal> MaterialsTotals {get; set;}

        public DbSet<Product> Products {get; set;}
        public DbSet<MaterialData> MatData {get; set;}
        
    }

}