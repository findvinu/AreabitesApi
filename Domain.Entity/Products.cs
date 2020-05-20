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
        public string DateModified { get; set; }
        public string ProductCode { get; set; }
        public Categories Category { get; set; }
    }
}
