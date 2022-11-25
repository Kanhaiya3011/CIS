using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Models.Relations
{
    public class BeneficiarySchemeApplied
    {
        public int Id { get; set; }
        public Beneficiary Beneficiary { get; set; }
        public Scheme Scheme { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ApplicationStatus { get; set; }
    }
}
