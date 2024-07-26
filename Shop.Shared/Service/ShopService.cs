using ShopLibrary.Models;
using ShopLibrary;
using ShopLibrary.Repositories;
using ShopLibrary.Repositories.Interface;
using Shared.Exceptions;
using System.Text.RegularExpressions;

namespace ShopAutomationAPI.Service
{
    public class ShopService(IShopRepository shopRepository)
    {
        public async Task AddShop(Shop shop)
        {
            var regex = new Regex(@"^[a-zA-Z\s]+$");

            if (!regex.IsMatch(shop.Name))
            {
                throw new InvalidCustomerNameException("Name can only contain letters");
            }

            await shopRepository.AddAsync(shop);
        }
    }
}
