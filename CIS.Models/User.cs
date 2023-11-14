using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
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


        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }

        public string Status { get; set; }
        public User()
        {
            Roles = new Role();
        }
    }

    public class UserViewModel
    {
        public User user { get; set; }
        public IList<Role> roles { get; set; }
        public UserViewModel()
        {
            roles = new List<Role>();
        }
    }

}