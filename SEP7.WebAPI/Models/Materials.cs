using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SEP7.WebAPI.Models

{

   public class MaterialData
{
    [Key]
    public int MaterialDataId { get; set; }

    [ForeignKey("Product")]  
    public string ProductID { get; set; }

    [ForeignKey("MaterialsTotal")]  
    public int MaterialId { get; set; }

    public string MaterialType { get; set; }
    public float TotalWeight { get; set; }

    // Navigation properties
    public virtual Product Product { get; set; }
    public virtual MaterialsTotal MaterialsTotal { get; set; }
}
}