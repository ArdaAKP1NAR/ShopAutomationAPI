using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories
{
    public class CustomerRepository(ShopContext context) : BaseRepository<Customer>(context),ICustomerRepository
    {
    }
}
