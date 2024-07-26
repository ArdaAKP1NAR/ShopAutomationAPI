using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories.Interface
{
    public interface IProductRepository
    {
        Task UpdateRange(ICollection<Product> entities);
        Task<Product> AddAsync(Product entity);
        IQueryable<Product> GetAll();
        Task<Product> GetByIdAsync(long id); 
        Task DeleteAsyncById(long id);
        Task UpdateAsync(Product entity);
    }
}
