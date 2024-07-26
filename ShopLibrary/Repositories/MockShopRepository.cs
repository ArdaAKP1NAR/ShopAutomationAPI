using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories
{
    internal class MockShopRepository : IShopRepository
    {
        public Task<Shop> AddAsync(Shop entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Shop> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Shop> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Shop entity)
        {
            throw new NotImplementedException();
        }
    }
}
