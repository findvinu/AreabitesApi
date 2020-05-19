using Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Infrastructure.Data.ViewModels
{
    public class ProductsViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Categories Category { get; set; }
        [JsonIgnore]
        public string ProductCode { get; set; }
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        public Products Create()
        {
            return new Products
            {
                Name = Name,
                ProductCode=ProductCode
    };
        }
    }
}

