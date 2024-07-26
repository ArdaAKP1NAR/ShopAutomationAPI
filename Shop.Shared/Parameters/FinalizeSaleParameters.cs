using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAutomationAPI.Shared.Parameters
{
    public class FinalizeSaleParameters
    {
        public long CustomerId { get; set; }
        public long SessionId { get; set; }
    }
}
