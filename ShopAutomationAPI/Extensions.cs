using ShopAutomationAPI.Service;
using ShopLibrary.Repositories;
using ShopLibrary.Repositories.Interface;

namespace ShopAutomationAPI;
public static class Extensions
{
    public static void AddServices(this IServiceCollection Services)
    {
        Services.AddScoped<IShopRepository,ShopRepository>();
        Services.AddScoped<IProductRepository,ProductRepository>();
        Services.AddScoped<IClubCardRepository,ClubCardRepository>();
        Services.AddScoped<IDiscountRepository,DiscountRepository>();
        Services.AddScoped<ICustomerRepository,CustomerRepository>(); 
        Services.AddScoped<ISaleRepository,SaleRepository>();
        Services.AddScoped<ISessionRepository,SessionRepository>();


        Services.AddScoped<ShopService>();
        Services.AddScoped<ProductService>();
        Services.AddScoped<ClubCardService>();
        Services.AddScoped<SaleService>();
        Services.AddScoped<DiscountService>();
        Services.AddScoped<CustomerService>();

    }
}
