using ShopLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Models
{
    public class Shop : BaseEntity
    {
        [MaxLength(255)]
        public required string Name { get; init; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
