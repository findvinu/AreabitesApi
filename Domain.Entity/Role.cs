using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entity
{
    public class Role: IdentityRole<int>
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Required]
        //public int Id { get; set; }
        //[Required]
        //public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
