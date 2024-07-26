using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories.Interface
{
    public interface IShopRepository
    {
        Task<Shop> AddAsync(Shop entity);
        IQueryable<Shop> GetAll();
        Task<Shop> GetByIdAsync(long id);
        Task UpdateAsync(Shop entity);
    }
}
