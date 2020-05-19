using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateModified { get; set; }
        [JsonIgnore]
        public string ProductCode { get; set; }
        public Categories Category { get; set; }
    }
}
