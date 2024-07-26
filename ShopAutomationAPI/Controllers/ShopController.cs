using Microsoft.AspNetCore.Mvc;
using ShopAutomationAPI.Service;
using ShopLibrary;
using ShopLibrary.Repositories;
using ShopLibrary.Repositories.Interface;
using ShopLibrary.ViewModels;
using ShopLibrary.Models;
namespace ShopAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        private ShopService ShopService;
        public ShopController(ShopService ShopService)
        {
            this.ShopService = ShopService;
        }
        [HttpPost]
        [Route("AddShop")]
        public async Task<ActionResult<long>> AddShop(ShopViewModel shopViewModel)
        {
            var Shop = new Shop()
            {
                Name = shopViewModel.Name,
            };
            
            await ShopService.AddShop(Shop);
            return Ok(Shop.Id);  
        
        }
    }
}
