using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopAutomationAPI.Service;
using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using ShopLibrary.ViewModels;

namespace ShopAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ClubCardController : ControllerBase
    {
        private ClubCardService clubCardService;
        public ClubCardController(ClubCardService clubCardService)
        {
            this.clubCardService = clubCardService;
        }
        [HttpPost]
        public async Task<ActionResult<long>> AddClubCard(ClubCardViewModel clubCardViewModel, long ProductId)
        {
            try
            {
                return Ok(await clubCardService.AddClubCard(clubCardViewModel, ProductId));
            }
            catch (InvalidDiscountException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
