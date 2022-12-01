using System.ComponentModel.DataAnnotations;

namespace CIS.Models.Relations
{
    public class BeneficiarySchemeApplied
    {
        [Key]
        public int Id { get; set; }
        public int Beneficiary { get; set; }
        public int Scheme { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ApplicationStatus { get; set; }
    }
    public class ViewModelBeneficiarySchemeApplied
    {
        public IList<Beneficiary> Beneficiary { get; set; }
        public IList<Scheme> Schemes { get; set; }
        public IList<BeneficiarySchemeApplied> SchemeApplied {get; set;}

        public ViewModelBeneficiarySchemeApplied()
        {
            Beneficiary = new List<Beneficiary>();
            Schemes = new List<Scheme>();
            SchemeApplied = new List<BeneficiarySchemeApplied>();
        }

}
