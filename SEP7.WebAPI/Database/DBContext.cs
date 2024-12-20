using Microsoft.EntityFrameworkCore;
using SEP7.WebAPI.Models;


namespace SEP7.Database.Data

{
  public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options) { }

        public DbSet<MaterialsTotal> MaterialsTotals {get; set;}
        public DbSet<User> Users { get; set; }

        public DbSet<HQ_Usage> HQ_Usages {get; set;}

        public DbSet<Product> Products {get; set;}
        public DbSet<MaterialData> MatData {get; set;}
        


protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HQ_Usage>()
                .HasKey(h => new { h.Year });  // Composite key

        }

    }

    

}