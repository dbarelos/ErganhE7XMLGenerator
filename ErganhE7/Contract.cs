using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npoi.Mapper.Attributes;

namespace ErganhE7
{
    public class Contract
    {
        public decimal apodoxes;

        [Column("ΟΝΟΜΑ")]
        public string onoma { get; set; }
        [Column("ΕΠΙΘΕΤΟ")]
        public string eponymo { get; set; }
        [Column("ΜΗΤΡΩΝΥΜΟ")]
        public string onoma_mitros { get; set; }
        [Column("ΠΑΤΡΩΝΥΜΟ")]
        public string onoma_patros { get; set; }
        
        [Column("ΗΜΕΡΟΜΗΝΙΑ ΠΡΟΣΛΗΨΗΣ")]
        public DateTime hireDate { get; set; }
        
        
        public string proslipsidate { 
            get 
            {
                if (hireDate == null) return "";
                return hireDate.ToString("dd/MM/yyyy");
            } 
        }
        [Column("ΗΜΕΡΟΜΗΝΙΑ ΓΕΝΝΗΣΗΣ")]
        public DateTime hmerGennhshs { get; set; }
        public string birthdate 
        { 
            get 
            {
                return hmerGennhshs.ToString("dd/MM/yyyy");
            } 
        }
        public AnaggeliaE7TypeF_sex sex
        {
            get
            {
                if (fylo != null && fylo.ToUpper().Equals("ΓΥΝΑΙΚΑ"))
                {
                    return AnaggeliaE7TypeF_sex.female;
                }
                else
                    return AnaggeliaE7TypeF_sex.male;
            }
        }
        [Column("ΑΦΜ")]
        public string vat { get; set; }
        public string afm { 
            get
            {
                if (vat == null || vat == "") return "";
                return vat.Replace("'", "");
            } 
        }

        [Column("ΦΥΛΟ")]
        public string fylo { get; set; }

        [Column("ΑΔΤ")]
        public string adt { get; set; }
        [Column("ΟΙΚΟΓΕΝΕΙΑΚΗ ΚΑΤΑΣΤΑΣΗ")]
        public string eggamosKatastash { get; set; }

        public AnaggeliaE7TypeF_marital_status maritalStatus
        {
            get
            {
                if(eggamosKatastash != null)
                {
                    if (eggamosKatastash.ToUpper().StartsWith("ΈΓΓΑΜ")) return AnaggeliaE7TypeF_marital_status.married;
                    if (eggamosKatastash.ToUpper().StartsWith("ΆΓΑΜ")) return AnaggeliaE7TypeF_marital_status.notMarried;
                    if (eggamosKatastash.ToUpper().StartsWith("ΧΗΡ")) return AnaggeliaE7TypeF_marital_status.widowed;
                    if (eggamosKatastash.ToUpper().StartsWith("ΔΙΑΖ")) return AnaggeliaE7TypeF_marital_status.divorced;
                }
                return AnaggeliaE7TypeF_marital_status.notMarried;
            }
        }
        
        public string epipedo_morfosis
        {
            get
            {
                if(morfosi != null)
                {
                    if (this.morfosi.Equals("ΑΕΙ")) return "11";
                    if (this.morfosi.StartsWith("ΜΕΤΑΠΤ")) return "12";
                    if (this.morfosi.StartsWith("ΙΕΚ ΜΕ ΠΙΣ")) return "9";
                }
                return "11"; // default = AEI
            }
        }
        [Column("ΤΕΚΝΑ")]
        public string arithmos_teknon { get; set; }
        [Column("ΑΜΚΑ")]
        public string socialInsuranceNumber { get; set; }

        public string amka
        {
            get
            {
                if (socialInsuranceNumber == null || socialInsuranceNumber == "") return "";
                return socialInsuranceNumber.Replace("'", "");
            }
        }
        [Column("ΜΟΡΦΩΤΙΚΟ ΕΠΙΠΕΔΟ")]
        public string morfosi { get; set; }
        [Column("ΕΙΔΟΣ ΑΠΑΣΧΟΛΗΣΗΣ")]
        public string plhrhs_merikh { get; set; }
        public AnaggeliaE7TypeF_kathestosapasxolisis kathestosApasxolisis
        {
            get
            {
                if(plhrhs_merikh != null)
                {
                    if (plhrhs_merikh.ToUpper().Contains("ΜΕΙΩΜ") || plhrhs_merikh.ToUpper().Contains("ΩΡΟΜ")) return AnaggeliaE7TypeF_kathestosapasxolisis.merikh;
                }
                return AnaggeliaE7TypeF_kathestosapasxolisis.plhrhs;
            }
        }
        [Column("ΚΩΔΙΚΟΣ ΕΙΔΙΚΟΤΗΤΑΣ ΕΦΚΑ")]
        public string kodikosEidikotitas { get; set; }
        
        [Column("ΚΛΑΔΟΣ")]
        public string eidikotita { get; set; }

        [Column("ΗΜΕΡΟΜΗΝΙΑ ΑΠΟΛΥΣΗΣ")]
        public DateTime apolysisdate { get; set; }
        public DateTime lastWorkedOnDate
        {
            get
            {
                return apolysisdate;
            }
        }
        [Column("ΣΥΜΒΑΤΙΚΗ ΗΜΕΡΟΜΗΝΙΑ ΛΗΞΗΣ ΣΥΜΒΑΣΗΣ")]
        public DateTime symvatikhHmeromhniaLh3hsSymvashs { get; set; }

        [Column("ΜΙΣΘΟΣ")]
        public decimal misthos { get; set; }
    }
}
