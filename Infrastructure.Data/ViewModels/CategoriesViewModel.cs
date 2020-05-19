using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Data.ViewModels
{
    public class CategoriesViewModel
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        public string CategoryCode { get; set; }
        [Required]
        public List<Products> Products { get; set; }

        public Categories Create()
        {
            return new Categories
            {
                Name = Name
            };
        }
    }
}
