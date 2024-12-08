using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SEP7.WebAPI.Models
{
  public class Product
{
    [Key]
    public string ProductID { get; set; }

    [Required]
    public string ProductName { get; set; }

    // Navigation properties
    
    public virtual ICollection<MaterialData> MaterialData { get; set; }
       
       
    

    public virtual MaterialsTotal MaterialsTotal { get; set; }  // Navigation property to MaterialsTotal
}

}
