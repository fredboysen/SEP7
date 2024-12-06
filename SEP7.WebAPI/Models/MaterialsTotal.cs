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

        public float ADP_Fossil_MJ { get; set; }

        public float ADP_Minerals_Metals { get; set; }

        public float AP_Mol_H_Eq { get; set; }

        public float E_Fi_CTUe { get; set; }

        public float E_Fm_CTUe { get; set; }

        public float E_Fo_CTUe { get; set; }

        public float EP_Terrestrial_Mol_N_Eq { get; set; }

        public float EP_Freshwater_kg_P { get; set; }

        public float EP_Marine_kg_N { get; set; }

        public float ETP_FW_CTUe { get; set; }

        public float GWP_Biogenic_kg_CO2 { get; set; }

        public float GWP_Fossil_kg_CO2 { get; set; }

        public float GWP_LULUC_kg_CO2 { get; set; }

        public float GWP_Total_kg_CO2 { get; set; }

        public float HT_CI_CTUh { get; set; }

        public float HT_CM_CTUh { get; set; }

        public float HT_CO_CTUh { get; set; }

        public float HT_NCI_CTUh { get; set; }

        public float HT_NCM_CTUh { get; set; }

        public float HT_NCO_CTUh { get; set; }

        public float HTTP_C_CTUh { get; set; }

        public float HTTP_NC_CTUh { get; set; }

        public float IRP_kBq_U235 { get; set; }

        public float LU_Pt { get; set; }

        public float ODP_kg_CFC11 { get; set; }

        public float PM_Disease_Inc { get; set; }

        public float POCP_kg_NMVOC { get; set; }

        public float WDP_m3_Depriv { get; set; }


    public int ProductID { get; set; }
    public virtual Product Product { get; set; }
    }
}
