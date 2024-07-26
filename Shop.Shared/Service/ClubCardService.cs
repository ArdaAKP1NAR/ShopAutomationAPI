using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopLibrary.Migrations;
using ShopLibrary.Models;
using ShopLibrary.Repositories;
using ShopLibrary.Repositories.Interface;
using ShopLibrary.ViewModels;
using System.Text.RegularExpressions;

namespace ShopAutomationAPI.Service
{
    public class ClubCardService(IClubCardRepository clubCardRepository, IProductRepository productRepository ) 
    {
        public async Task<long> AddClubCard(ClubCardViewModel clubCardViewModel, long ProductId)
        {
            var regex = new Regex(@"^[a-zA-Z\s]+$");

            if (!regex.IsMatch(clubCardViewModel.Name))
            {
                throw new InvalidClubCardNameException("Name can only contain letters");
            }

            var Product = await productRepository.GetByIdAsync(ProductId);
            if (Product == null)
            {
                throw new ProductNotFoundException($"Product with Id {ProductId} not found");
            }
            List<Product> Products = new List<Product>() { Product }; //new() {Product} da olabilir.


            var Discount = new Discount // discountId dolduramadım o yüzden update database atamadım
            { 
                DiscountAmount = clubCardViewModel.DiscountViewModel.DiscountAmount,
                CardType = clubCardViewModel.DiscountViewModel.SelectedCardType,
                Products = Products
            };
            
            if (Discount.DiscountAmount >= 100)
            {
                throw new InvalidDiscountException("Discount cannot be greater than 100");
            }

            ClubCard clubCard = new ClubCard()
            {
                Name = clubCardViewModel.Name,
                Discount = Discount
            };

            await productRepository.UpdateAsync(Product);
            return (await clubCardRepository.AddAsync(clubCard)).Id;

        }

        //  public async Task UpdateClubCard(long ClubCardId)
    }
}
