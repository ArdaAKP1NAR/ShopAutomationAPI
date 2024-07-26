using Microsoft.EntityFrameworkCore;
using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories
{
    public class ProductRepository(ShopContext context) : BaseRepository<Product>(context),IProductRepository
    {
        /*public async Task<List<Product>> GetAllActiveProducts()
        {
            return await GetAll().Where(a => a.IsActive).ToListAsync();
        }
        */
        
    }
}
