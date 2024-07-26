using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using ShopAutomationAPI.Service;
using ShopAutomationAPI.Shared.Parameters;
using ShopLibrary.Models;
using ShopLibrary.ViewModels;
using System.Text.RegularExpressions;
using ShopAutomationAPI.Shared.Exceptions;
namespace ShopAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]  //controllerName action func name
    public class CustomerController : ControllerBase
    {
        private CustomerService CustomerService;
        public CustomerController(CustomerService customerService)
        {
            this.CustomerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerViewModel customerViewModel)
        {
            /*
              if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var a = new Regex("@\"^[a-zA-Z\\s]+$\"");

            if (!a.IsMatch(customerViewModel.Name))
            {
                return BadRequest("Name can only contain letters");
            }*/
            var customer = new Customer()
            {
                Name = customerViewModel.Name,
            };
            await CustomerService.AddCustomer(customer);
            return Ok("The customer has been added successfully");
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomerWithClubCard([FromForm]CustomerWithClubCardViewModel customerViewModel)
        {
            if (customerViewModel.ClubId == null)
            {
                throw new InvalidParameterException("Parameter clubid cannot be null");
            }
            try
            {
                var customer = new Customer()
                {
                    ClubCardId = customerViewModel.ClubId,
                    Name = customerViewModel.Name,
                };
                await CustomerService.AddCustomerWithClubCard(customer, (long)customerViewModel.ClubId);
                return Ok($"The customer has added succesfuly with Id {customer.Id}.");
            }
            catch (ClubCardNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<long>> UpdateCustomer([FromQuery] UpdateCustomerParameters customerParameters)
        {
            try
            {
                await CustomerService.UpdateCustomer(customerParameters);
                return Ok($"Customer updated succesfuly Id {customerParameters.CustomerId}");
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ClubCardNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
