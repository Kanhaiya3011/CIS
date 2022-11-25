﻿using System.Reflection;

namespace CIS.Models
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Modified { get; init; }
        public Role Role { get; set; }
    }

}