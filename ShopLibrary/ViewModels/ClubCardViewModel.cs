using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.ViewModels
{
    public class ClubCardViewModel
    {
        public string Name { get; set; }
        public DiscountViewModel DiscountViewModel { get; set; }
    }
}
