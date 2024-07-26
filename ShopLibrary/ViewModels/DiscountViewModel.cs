using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.ViewModels
{
    public class DiscountViewModel
    {
        public CardType SelectedCardType { get; set; }
        //[Required]
        //[Range(0, 100, ErrorMessage = "Discount amount must be between 0 and 100.")]
        public int DiscountAmount { get; set; }
    }
}
