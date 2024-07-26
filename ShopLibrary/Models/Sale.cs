using ShopLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Models
{
    public class Sale : BaseEntity
    { 
        public List<Product> Products { get; set; }
        public Customer Customer { get; set; }
        public long CustomerId { get; set; }
        public double TotalPrice { get; set; }
    }
}
