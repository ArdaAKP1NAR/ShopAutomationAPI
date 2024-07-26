using ShopLibrary.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Models
{
    public class Customer : BaseEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }
        public ClubCard? ClubCard { get; set; }
        public long? ClubCardId { get; set; } 
    }
}
