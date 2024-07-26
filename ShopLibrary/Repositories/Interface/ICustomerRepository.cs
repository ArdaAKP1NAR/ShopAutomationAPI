using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer entity);
        IQueryable<Customer> GetAll();
        Task<Customer> GetByIdAsync(long id);
        Task UpdateAsync(Customer entity);
    }
}
