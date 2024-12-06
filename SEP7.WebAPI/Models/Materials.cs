using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SEP7.WebAPI.Models

{

   public class MaterialData
{
    [Key]
    public int MaterialDataId { get; set; }

    [ForeignKey("Product")]  // Explicit Foreign Key
    public int ProductID { get; set; }

    [ForeignKey("MaterialsTotal")]  // Explicit Foreign Key
    public int MaterialId { get; set; }

    public string MaterialType { get; set; }
    public float TotalWeight { get; set; }

    // Navigation properties
    [JsonIgnore]
    public virtual Product Product { get; set; }
    public virtual MaterialsTotal MaterialsTotal { get; set; }
}
}