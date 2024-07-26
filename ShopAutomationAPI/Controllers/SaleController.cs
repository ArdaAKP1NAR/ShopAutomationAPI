using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopAutomationAPI.Service;
using ShopAutomationAPI.Shared.Parameters;

namespace ShopAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SaleController(SaleService SaleService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> FinalizeSale([FromQuery]FinalizeSaleParameters finalizeSaleParameters)
        {
            try 
            {
                string receipt = await SaleService.FinalizeSale(finalizeSaleParameters);
                return Ok(receipt);
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(SessionNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<long>> AddProductToSession([FromQuery]AddProductToSessionParameters addProductToSessionParameters)
        {
           // session id 0 ise yeni session olustur ve kodunu geri ver

           if (addProductToSessionParameters.SessionId == 0)
            {
                addProductToSessionParameters.SessionId = await SaleService.OpenSession();
            }
            try
            {
                await SaleService.AddProductToSession(addProductToSessionParameters);
                return Ok(addProductToSessionParameters.SessionId);
            }
            catch (ProductNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (SessionNotFoundException ex)
            {
                return new ContentResult() { Content= ex.Message, StatusCode = 404 };
            }
        }
        [HttpPost]
        public async Task<ActionResult<long>> UpdateSale([FromQuery]UpdateSaleParameters updateSaleParameters)
        {
            await UpdateSale(updateSaleParameters);    
            return Ok($"Sale updated succesfuly Id = {updateSaleParameters.SaleId}");
        }
    }
}
