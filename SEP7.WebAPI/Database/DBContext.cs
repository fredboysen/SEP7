using Microsoft.EntityFrameworkCore;
using SEP7.WebAPI.Models;


namespace SEP7.Database.Data

{
  public class ApplicationDB : DbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options) { }

        public DbSet<Materials> Materials {get; set;}

        public DbSet<Routes> Routes {get; set;}
        
    }

}