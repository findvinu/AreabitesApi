using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Products> Products { get; set; }
        public string CategoryCode { get; set; }
    }
}
