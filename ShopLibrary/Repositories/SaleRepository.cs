using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories
{
    public class SaleRepository(ShopContext context) :  BaseRepository<Sale>(context),ISaleRepository
    {
    }
}
