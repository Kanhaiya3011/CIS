using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CIS.Models
{
    public class Scheme
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SchemeName { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; init; }
        public PhysicallyDisability PhysicallyDisability { get; init; }
        public int BelowIncomeEligibility { get; set; }
        public string SchemeDescription { get; set; }
        public bool IsVeteran { get; set; }
        public bool IsActive { get; set; }
        public Scheme()
        {
            PhysicallyDisability = new PhysicallyDisability();
            Category = new Category();
            Gender = new Gender();
        }
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
