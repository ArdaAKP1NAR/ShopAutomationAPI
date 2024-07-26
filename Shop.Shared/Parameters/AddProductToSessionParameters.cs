using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAutomationAPI.Shared.Parameters
{
    public class AddProductToSessionParameters
    {
        public long ProductId { get; set; }
        public long SessionId { get; set; } = 0;
    }
}
