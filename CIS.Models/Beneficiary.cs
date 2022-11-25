using System.Reflection;

namespace CIS.Models
{
    public class Beneficiary
    {
        public int Id { get; set; }
        public string FirstName { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }
        public Gender Gender { get; init; }
        public double PhoneNumber { get; set; }
        public string EMail { get; set; }
        public DateTime? Modified { get; init; }
    }

}