using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories.Interface
{
    public interface ISaleRepository
    {
        Task<Sale> AddAsync(Sale entity);
        IQueryable<Sale> GetAll();
        Task<Sale> GetByIdAsync(long id);
        Task UpdateAsync(Sale entity);
    }
}
