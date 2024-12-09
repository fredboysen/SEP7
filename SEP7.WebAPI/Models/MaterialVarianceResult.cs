
using System.ComponentModel.DataAnnotations;
namespace SEP7.WebAPI.Models
{
public class MaterialVarianceResult
{
    public string FieldName { get; set; }
    public float MinValue { get; set; }
    public float MaxValue { get; set; }
    public float PercentageVariance { get; set; }
}
}
