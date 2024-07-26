using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories.Interface
{
    public interface ISessionRepository
    {
        Task<Session> AddAsync(Session entity);
        Task DeleteAsyncById(long id);
        IQueryable<Session> GetAll();
        Task<Session> GetByIdAsync(long id);
        Task UpdateAsync(Session entity); 
        Task UpdateRange(ICollection<Session> entities);

    }
}
