using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Entity
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
    }
}
