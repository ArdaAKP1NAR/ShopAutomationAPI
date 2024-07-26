using ShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary.Repositories.Interface
{
    public interface IClubCardRepository
    {
        Task<ClubCard> AddAsync(ClubCard entity);
        IQueryable<ClubCard> GetAll();
        Task<ClubCard> GetByIdAsync(long id);
        Task UpdateAsync(ClubCard entity);
    }
}
