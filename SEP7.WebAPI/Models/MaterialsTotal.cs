using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEP7.WebAPI.Models
{
    public class MaterialsTotal
{
    [Key]
    public int MaterialId { get; set; }

    [Required]
    public string MaterialName { get; set; }

        public double ADP_Fossil_MJ { get; set; }

        public double ADP_Minerals_Metals { get; set; }

        public double AP_Mol_H_Eq { get; set; }

        public double E_Fi_CTUe { get; set; }

        public double E_Fm_CTUe { get; set; }

        public double E_Fo_CTUe { get; set; }

        public double EP_Terrestrial_Mol_N_Eq { get; set; }

        public double EP_Freshwater_kg_P { get; set; }

        public double EP_Marine_kg_N { get; set; }

        public double ETP_FW_CTUe { get; set; }

        public double GWP_Biogenic_kg_CO2 { get; set; }

        public double GWP_Fossil_kg_CO2 { get; set; }

        public double GWP_LULUC_kg_CO2 { get; set; }

        public double GWP_Total_kg_CO2 { get; set; }

        public double HT_CI_CTUh { get; set; }

        public double HT_CM_CTUh { get; set; }

        public double HT_CO_CTUh { get; set; }

        public double HT_NCI_CTUh { get; set; }

        public double HT_NCM_CTUh { get; set; }

        public double HT_NCO_CTUh { get; set; }

        public double HTTP_C_CTUh { get; set; }

        public double HTTP_NC_CTUh { get; set; }

        public double IRP_kBq_U235 { get; set; }

        public double LU_Pt { get; set; }

        public double ODP_kg_CFC11 { get; set; }

        public double PM_Disease_Inc { get; set; }

        public double POCP_kg_NMVOC { get; set; }

        public double WDP_m3_Depriv { get; set; }


    public string ProductID { get; set; }
    public virtual Product Product { get; set; }
    }
}
