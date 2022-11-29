using System.ComponentModel.DataAnnotations;
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
        public Gender Gender { get; init; }
        [Phone]
        public double PhoneNumber { get; set; }
        [EmailAddress]
        [Required]
        public string EMail { get; set; }
        public DateTime? Modified { get; init; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }

}