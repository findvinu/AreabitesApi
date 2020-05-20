using Domain.Entity;
using Newtonsoft.Json;
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
        [JsonIgnore]
        public DateTime DateModified { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public string CategoryCode { get; set; }
        [JsonIgnore]
        public List<Products> Products { get; set; }

        public Categories Create()
        {
            return new Categories
            {
                Name = Name,
                CategoryCode=CategoryCode
                
            };
        }
    }
}
