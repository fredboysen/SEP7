
using System.ComponentModel.DataAnnotations;
namespace SEP7.WebAPI.Models
{
public class MaterialVarianceResult
{
    public string FieldName { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
    public double PercentageVariance { get; set; }
}
}
