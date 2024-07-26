using ShopLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Models
{
    public class SessionProduct : BaseEntity
    {
        public long ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public long SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public int Amount { get; set; }
    }
}
