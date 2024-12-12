using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP7.WebAPI.Models
{
    public class HQ_Usage
    {
        public string Year { get; set; }  

        public int EnergyConsumption { get; set; }  

        public int Electricity { get; set; }  

        public int Solarpanels { get; set; }  

        public int DistrictHeating { get; set; }  

        public int NaturalGas { get; set; }  

        public int Propan { get; set; } 
         public int Hydrogen { get; set; } 
         public int Oil { get; set; } 
         public int Petrol { get; set; } 
          public int Diesel { get; set; } 

    }
}
