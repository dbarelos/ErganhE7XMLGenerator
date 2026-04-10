using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using ErganhE7;

public partial class AnaggeliaE7NType
{
    public void initialize()
    {
        this.f_rel_protocol = "";
        this.f_rel_date = "";

        this.f_eponymo = "";
        this.f_onoma = "";
        this.f_onoma_patros = "";
        this.f_onoma_mitros = "";

        this.f_birthdate = "";
        this.f_yphkoothta = "";
        this.f_typos_taytothtas = "";
        this.f_ar_taytothtas = "";
        this.f_ekdousa_arxh = "";
        this.f_date_ekdosis = "";
        this.f_date_ekdosis_lixi = "";

        this.f_res_permit_inst_type = "";
        this.f_res_permit_inst_ar = "";
        this.f_res_permit_inst_lixi = "";

        this.f_res_permit_ap_type = "";
        this.f_res_permit_ap_ar = "";
        this.f_res_permit_ap_lixi = "";

        this.f_res_permit_visa_ar = "";
        this.f_res_permit_visa_from = "";
        this.f_res_permit_visa_to = "";

        this.f_arithmos_teknon = "";
        this.f_afm = "";
        this.f_doy = "";
        this.f_amika = "";
        this.f_amka = "";
        this.f_code_anergias = "";
        this.f_ar_vivliou_anilikou = "";
        this.f_epipedo_morfosis = "";

        this.f_kad_pararthmatos = "";
        this.f_kallikratis_pararthmatos = "";
        this.f_ypiresia_sepe = "";
        this.f_ypiresia_oaed = "";
        this.f_aa_pararthmatos = "";

        this.f_eidikothta = "";
        this.f_apodoxes = "";
        this.f_proslipsidate = "";
        this.f_lixisymbashdate = "";
        this.f_apolysisdate = "";
        this.f_comments = "";
        this.f_logosperatosiscomments = "";

        this.f_foreign_file = new byte[] { };
        this.f_young_file = new byte[] { };
    }


    public void copyFromContract(Contract contract)
    {
        this.f_afm = contract.afm;
        this.f_eponymo = contract.eponymo;
        this.f_onoma = contract.onoma;
        this.f_onoma_patros = contract.onoma_patros;
        this.f_onoma_mitros = contract.onoma_mitros;
        this.f_birthdate = contract.birthdate;

        this.f_sex = (contract.sex == AnaggeliaE7NTypeF_sex.male)
            ? AnaggeliaE7NTypeF_sex.male
            : AnaggeliaE7NTypeF_sex.female;

        this.f_ar_taytothtas = contract.adt;
        this.f_typos_taytothtas = "ΔAT";
        this.f_marital_status = (AnaggeliaE7NTypeF_marital_status)contract.maritalStatus;
        this.f_arithmos_teknon = contract.arithmos_teknon;
        this.f_amka = contract.amka;

        this.f_xaraktirismos = AnaggeliaE7NTypeF_xaraktirismos.ypallhlos;
        this.f_sxeshapasxolisis = AnaggeliaE7NTypeF_sxeshapasxolisis.OrismenouXronou;

        this.f_eidikothta = contract.kodikosEidikotitas;
        this.f_apodoxes = contract.misthos.ToString("N2", new CultureInfo("el-GR"));
        this.f_proslipsidate = contract.proslipsidate;

        this.f_epipedo_morfosis = contract.epipedo_morfosis;

        this.f_apolysisdate = contract.apolysisdate.ToString("dd/MM/yyyy");
        this.f_lixisymbashdate = contract.symvatikhHmeromhniaLh3hsSymvashs.ToString("dd/MM/yyyy");

        this.f_logosperatosis = AnaggeliaE7NTypeF_logosperatosis.LhxhSympefwnhmenouXronou;

        this.f_kathestosapasxolisis = (AnaggeliaE7NTypeF_kathestosapasxolisis)contract.kathestosApasxolisis;

        this.f_logosperatosiscomments = "";
        if(!String.IsNullOrEmpty(contract.comments)) this.f_logosperatosiscomments = contract.comments;
    }
}

