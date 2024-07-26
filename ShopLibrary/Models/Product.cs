using ShopLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Models
{
    public class Product : BaseEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
        public bool IsActive { get; set; } = true;
        public Discount? Discount { get; set; }
        public long? discountId { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
