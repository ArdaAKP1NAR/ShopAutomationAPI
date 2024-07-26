using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopAutomationAPI.Service;
using ShopLibrary.Models;
using ShopLibrary.Repositories;
using ShopLibrary.ViewModels;

namespace ShopAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController(DiscountService discountService) : ControllerBase
    {

        [HttpPost]
        [Route("AddDiscountToProduct/DiscountId = {DiscountId},ProductId = {ProductId}")]
        public async Task<ActionResult<long>> AddDiscountToProduct(DiscountViewModel discountViewModel, long ProductId, long DiscountId = 0)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await discountService.AddDiscountToProduct(discountViewModel, ProductId, DiscountId));
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
        [HttpPost]
        [Route("DeleteDiscount/DiscountId = {DiscountId}")]
        public async Task<IActionResult> DeleteDiscount(long DiscountId)
        {
            try
            {
                await discountService.DeleteDiscount(DiscountId);
                return Ok($"Discount removed succesfuly Id = {DiscountId}");
            }
            catch (DiscountNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [Route("UpdateDiscount/DiscountId = {DiscountId},DiscountAmount = {DiscountAmount}")]
        public async Task<IActionResult> UpdateDiscount(long DiscountId, int DiscountAmount)
        {
            try
            {
                await discountService.UpdateDiscount(DiscountId, DiscountAmount);
                return Ok($"Discount updated succesfuly with Id {DiscountId} new Amount = {DiscountAmount}");
            }
            catch (DiscountNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidDiscountException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}