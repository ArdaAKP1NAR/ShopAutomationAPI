using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.ViewModels
{
    public class ProductsViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int QuantityInStock { get; set; }
    }
}
