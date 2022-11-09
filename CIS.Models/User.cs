using System.Reflection;

namespace CIS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public Gender Gender { get; init; }
        public DateTime? Modified { get; init; }
    }

}