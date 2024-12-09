using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP7.WebAPI.Models
{
    public class HQ_Usage
    {
        public string UsageType { get; set; }  // The type of energy usage (e.g., Electricity, Natural Gas, etc.)

        public string Year { get; set; }  // The year for the usage data (e.g., 2023)

        public int EnergyConsumption { get; set; }  // Energy consumed (e.g., kWh, MWh, etc.)

        public string EnergyType { get; set; }  // Type of energy (e.g., kWh, Nm_2, Litres, etc.)

        public int EnergyCost { get; set; }  // The total cost of the energy consumed

        public string Currency { get; set; }  // The currency used for the energy cost (e.g., DKK)

        public int Co2_Emissions_Tons { get; set; }  // The CO2 emissions in tons

        public double UnitPrice { get; set; }  // The price per unit of energy (e.g., per kWh, per litre)
    }
}
