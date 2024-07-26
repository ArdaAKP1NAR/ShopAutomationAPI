using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories
{
    public class ShopRepository(ShopContext context) : BaseRepository<Shop>(context),IShopRepository
    {

    }
}
