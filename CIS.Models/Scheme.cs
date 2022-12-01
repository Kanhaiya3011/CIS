using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIS.Models
{
    public class Scheme
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SchemeName { get; set; }
        public int CategoryId { get; set; }
        public string SchemeDescription { get; set; }
        public bool IsActive { get; set; }
    }
    public class SchemeViewModel
    {
        public Scheme scheme { get; set; }
        public IList<Category> categories { get; set; }
        public SchemeViewModel()
        {
            categories = new List<Category>();
        }
    }
}
