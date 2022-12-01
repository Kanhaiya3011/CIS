using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CIS.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
    }

}