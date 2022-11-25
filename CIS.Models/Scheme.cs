using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Models
{
    public class Scheme
    {
        public int Id { get; set; }
        public string SchemeName { get; set; }
        public Category Category { get; set; }
        public string SchemeDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
