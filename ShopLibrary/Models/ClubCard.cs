using ShopLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Models
{
    public class ClubCard : BaseEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public CardType CardType { get; set; }
        public Discount? Discount { get; set; }
    }
}
