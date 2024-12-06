using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SEP7.WebAPI.Models
{
  public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    public string ProductName { get; set; }

    // Navigation properties
        [JsonIgnore]
    public virtual ICollection<MaterialData> MaterialData { get; set; }
       
       
        [JsonIgnore]

    public virtual MaterialsTotal MaterialsTotal { get; set; }  // Navigation property to MaterialsTotal
}

}
