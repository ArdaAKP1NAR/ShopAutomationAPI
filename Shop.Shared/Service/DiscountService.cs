using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopLibrary.Models;
using ShopLibrary.Repositories.Interface;
using ShopLibrary.ViewModels;

namespace ShopAutomationAPI.Service
{
    public class DiscountService(IDiscountRepository discountRepository,IProductRepository productRepository)
    {
        public async Task<long> AddDiscountToProduct(DiscountViewModel discountViewModel,long ProductId, long DiscountId = 0)
        {
            
            var product = await productRepository.GetByIdAsync(ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product with Id {ProductId} not found");
            }
            List<Product> Products = new List<Product>() { product };

            if (DiscountId == 0)
            {
                Discount discount = new Discount()
                {
                    CardType = discountViewModel.SelectedCardType,
                    DiscountAmount = discountViewModel.DiscountAmount,
                    Products = Products
                };
                
                if (discount.DiscountAmount >= 100)
                {
                    throw new InvalidDiscountException("Discount cannot be greater than 100");
                }
                
                return (await discountRepository.AddAsync(discount)).Id; 

            }
            else
            {
                var discount = await discountRepository.GetByIdAsync(DiscountId);
                discount.Products.Add(product);

                await discountRepository.UpdateAsync(discount);
                await productRepository.UpdateAsync(product);

                return (await discountRepository.AddAsync(discount)).Id;
            }
            // service kısmında düzenleme geçerli deger filan 
            // sessiondan örnek alarak yap ona benzer            
        }
        public async Task DeleteDiscount(long DiscountId)
        {
            var Discount = await discountRepository.GetByIdAsync(DiscountId);
            
            if (Discount == null)
            {
                throw new DiscountNotFoundException($"Discount with Id {DiscountId} not found ");
            }
            else
            {
                await discountRepository.DeleteAsyncById(DiscountId);
            }
        }
        public async Task UpdateDiscount(long DiscountId, int DiscountAmount)
        {
            var Discount = await discountRepository.GetByIdAsync(DiscountId);
            
            if (Discount == null)
            {
                throw new DiscountNotFoundException($"Discount with Id {DiscountId} not found ");
            }
            else
            {
                if (Discount.DiscountAmount >= 100)
                {
                    throw new InvalidDiscountException("Discount cannot be greater than 100");
                }
                else
                {
                    Discount.DiscountAmount = DiscountAmount;
                    await discountRepository.UpdateAsync(Discount);
                }
            }
        }
    }
}
