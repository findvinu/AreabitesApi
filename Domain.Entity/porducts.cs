using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Categories Category { get; set; }
    }
}
