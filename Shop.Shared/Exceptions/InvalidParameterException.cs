using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAutomationAPI.Shared.Exceptions
{
    public class InvalidParameterException(string Message) : Exception(Message)
    {
    }
}
