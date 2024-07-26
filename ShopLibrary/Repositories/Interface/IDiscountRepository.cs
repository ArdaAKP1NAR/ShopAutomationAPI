using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories.Interface
{
    public interface IDiscountRepository
    {
        Task<Discount> AddAsync(Discount entity);
        IQueryable<Discount> GetAll();
        Task<Discount> GetByIdAsync(long id);
        Task UpdateAsync(Discount entity);
        Task DeleteAsyncById(long id);
    }
}
