using ShopLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Models
{
    public class Discount : BaseEntity
    {
        public CardType CardType { get; set; }
        public List<Product> Products { get; set; }
        public int DiscountAmount {  get; set; }
    }
}
