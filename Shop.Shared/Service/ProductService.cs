using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopAutomationAPI.Shared.Parameters;
using ShopLibrary.Models;
using ShopLibrary.Repositories;
using ShopLibrary.Repositories.Interface;
using ShopLibrary.ViewModels;
using System.Text.RegularExpressions;

namespace ShopAutomationAPI.Service
{
    public class ProductService(IProductRepository productRepository,IShopRepository shopRepository)
    {
        public async Task AddProduct(Product product,long ShopId)
        {
            var regex = new Regex(@"^[a-zA-Z\s]+$"); // ürün fiyatı güncelleme indirim güncelleme 

            if (!regex.IsMatch(product.Name))
            {
                throw new InvalidProductNameException("Name can only contain letters");
            }
            var regex1 = new Regex("^[0-9]*$");
            // QuantityInStock değerini string'e dönüştür
            string quantityInStockString = product.QuantityInStock.ToString();

            if (!regex1.IsMatch(quantityInStockString))
            {
                throw new InvalidProductException("QuantityInStock can only contain numbers");
            }
            var regex2 = new Regex("^[0-9]*$");
            
         
            var newProduct = await productRepository.AddAsync(product);
            var Shop = await shopRepository.GetByIdAsync(ShopId);
            
            if (Shop == null)
            {
                throw new ShopNotFoundException(($"Shop with Id {ShopId} not found"));
            }
            
            if (newProduct.Price <= 0)
            {
                throw new InvalidProductException("The price of the product cannot be lower than 0 or equal");
            }
            
            if (newProduct.QuantityInStock < 0)
            {
                throw new InvalidProductException("The quantityinstock of the product cannot be lower than 0");
            }
            
            Shop.Products.Add(newProduct);
            await shopRepository.UpdateAsync(Shop);
        }
        public async Task UpdateProduct(UpdateProductParameters updateProductParameters)
        {
            var ProductToUpdate = await productRepository.GetByIdAsync(updateProductParameters.ProductId);
            if (ProductToUpdate == null)
            {
                throw new ProductNotFoundException($"Product with Id {updateProductParameters.ProductId} not found");
            }
            
            ProductToUpdate.Price = updateProductParameters.Price;
            ProductToUpdate.QuantityInStock = updateProductParameters.QuantityInStock;
            
            if (ProductToUpdate.Price <= 0)
            {
                throw new InvalidProductException("The price of the product cannot be lower than 0 or equal");
            }

            if (ProductToUpdate.QuantityInStock < 0)
            {
                throw new InvalidProductException("The quantityinstock of the product cannot be lower than 0");
            }

            await productRepository.UpdateAsync(ProductToUpdate);
        }
        public async Task DeleteProduct(long ProductId)
        {
            var Product = await productRepository.GetByIdAsync(ProductId);
            if (Product == null)
            {
                throw new ProductNotFoundException($"Product with Id {ProductId} not found");
            }
            await productRepository.DeleteAsyncById(ProductId);
        }
    }
}
