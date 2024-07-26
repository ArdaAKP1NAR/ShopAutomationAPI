using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopAutomationAPI.Service;
using ShopAutomationAPI.Shared.Parameters;
using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using ShopLibrary.ViewModels;

namespace ShopAutomationAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private ProductService productService;
        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductsViewModel productViewModel, long ShopId)
        {
            try
            {
                var product = new Product()
                {
                    Name = productViewModel.Name,
                    QuantityInStock = productViewModel.QuantityInStock,
                    Price = productViewModel.Price,
                };
                await productService.AddProduct(product, ShopId);
                return Ok($" The product added with Id {product.Id} succesfuly");
            }
            catch (ShopNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidProductException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<long>> UpdateProduct([FromBody]UpdateProductParameters updateProductParameters)
        {
            try
            {
                await productService.UpdateProduct(updateProductParameters); //ürün atma işlemi ekleme işlemi hepsi toplam ürün üzerinden yapılacak.
                return Ok($"The product updated Id = {updateProductParameters.ProductId} ");
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidProductException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(long ProductId)
        {
            try
            {
                await productService.DeleteProduct(ProductId);
                return Ok("product removed succesfuly");
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
