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
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Modified { get; init; }
        public int RoleId { get; set; }
        public string Status { get; set; }
    }

}