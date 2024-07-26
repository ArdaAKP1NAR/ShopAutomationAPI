using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.ViewModels
{
    public class ShopViewModel
    {
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
