using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CIS.Models
{
    public class Beneficiary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        [Required]
        public string LastName { get; init; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; init; }
        public PhysicallyDisability PhysicallyDisability { get; init; }
        public int Income { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string EMail { get; set; }
        public DateTime? Modified { get; init; }
        public string Status { get; set; }
        public bool IsVeteran { get; set; }
        public bool IsActive { get; set; }

        public Beneficiary()
        {
            PhysicallyDisability = new PhysicallyDisability();
            Gender = new Gender();
        }
    }

}