using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Shared.Exceptions;
using ShopAutomationAPI.Exceptions;
using ShopAutomationAPI.Shared.Parameters;
using ShopLibrary;
using ShopLibrary.Models;
using ShopLibrary.Repositories;
using ShopLibrary.Repositories.Interface;
using ShopLibrary.ViewModels;
using System.Linq;
using System.Text;

namespace ShopAutomationAPI.Service
{
    public class SaleService(ShopContext context, IProductRepository productRepository,ISessionRepository sessionRepository,ISaleRepository saleRepository,ICustomerRepository customerRepository)
    {
        public async Task AddProductToSession(AddProductToSessionParameters addProductToSessionParameters) // abi Id leri nasıl yazdırabilirim clubcard discount
        {

            var product = await productRepository.GetAll().SingleOrDefaultAsync(a => a.Id == addProductToSessionParameters.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException($"Product with Id {addProductToSessionParameters.ProductId} not found");
            }

            var session = await sessionRepository.GetAll().SingleOrDefaultAsync(A => A.Id == addProductToSessionParameters.SessionId); // performans etkiler mi where kullanmama gerek var mı ?
            if (session == null)
            {
                throw new SessionNotFoundException($"Session with Id {addProductToSessionParameters.SessionId} not found");
            }

            var sessionProduct = await context.SessionProducts.FirstOrDefaultAsync(a => (a.ProductId == addProductToSessionParameters.ProductId) && (a.SessionId == addProductToSessionParameters.SessionId));

            if (sessionProduct != null)
            {
                sessionProduct.Amount++;
            }
            else
            {
                var productToBeAdded = new SessionProduct()
                {
                    SessionId = addProductToSessionParameters.SessionId,
                    ProductId = addProductToSessionParameters.ProductId,
                    Amount = 1
                };

                await context.SessionProducts.AddAsync(productToBeAdded);
            }

            await context.SaveChangesAsync();
            //Productı sessiona ekle 
        }

        public async Task<long> OpenSession()
        {
            var session =  await sessionRepository.AddAsync(new());
            return session.Id;
            //sessionRepo dan yeni bir session oluştur
        }

        public async Task CloseSession(long sessionId)
        {
            await sessionRepository.DeleteAsyncById(sessionId);
            //sessionRepo daki sessionı sil
        }
        public async Task<string> FinalizeSale(FinalizeSaleParameters finalizeSaleParameters)
        {
            /*
            var newCustomer = await customerRepository.GetByIdAsync(customerId); 
            List<Product> saledProductList = new List<Product>();
            var saledProductsWithAmount = new List<ProductViewModel>();
            var session = await sessionRepository.GetAll()
                .Include(a => a.Products)
                .ThenInclude(a => a.Discount)
                .SingleAsync(a => a.Id == sessionId);
            
            foreach (var item in session.Products)                  
            {
                var product = await productRepository.GetByIdAsync(item.Id);

                if (product.QuantityInStock >= session.Products.Count) // Amount olayını çözmek kaldı beynim yandı düşünemiyorum 
                {
                    saledProductList.Add(product);
                    product.QuantityInStock -= session.Products.Count;
                    if (product.QuantityInStock <= 0)
                    {
                        product.IsActive = false;
                    }
                    saledProductsWithAmount.Add(new ProductViewModel()
                    {
                        Name = product.Name,    
                        Price = product.Price - item.Discount.DiscountAmount / 100,
                        Amount = session.Products.Count,
                    });
                }
            }

            Sale sale = new Sale()
            {
                CustomerId = newCustomer.Id,
                Products = saledProductList
            }; 

            await saleRepository.AddAsync(sale);
            await CloseSession(sessionId);
            return PrintReceipt(newCustomer, saledProductsWithAmount);
            */
            var Customer = await customerRepository.GetByIdAsync(finalizeSaleParameters.CustomerId);
            if(Customer == null)
            {
                throw new CustomerNotFoundException($"Customer with Id {finalizeSaleParameters.CustomerId} not found");
            }

            var sessionProducts = await context.SessionProducts
                .Where(a => a.SessionId == finalizeSaleParameters.SessionId)
                .Include(a => a.Product)
                .ThenInclude(b => b.Discount)
                .Select(a => new { 
                    Product = a.Product,
                    Amount = a.Amount
                })
                .ToListAsync();
            
            if(sessionProducts == null)
            {
                throw new SessionNotFoundException($"Session with Id {finalizeSaleParameters.SessionId} not found");
            }
            
            var saledProductList = new List<Product>();
            var saledProducts = new List<ProductViewModel>();

            foreach (var sessionProduct in sessionProducts)
            {
                if (sessionProduct.Product.QuantityInStock >= sessionProduct.Amount)
                {
                    saledProductList.Add(sessionProduct.Product);
                    
                    sessionProduct.Product.QuantityInStock -= sessionProduct.Amount;
                    
                    if(sessionProduct.Product.QuantityInStock <= 0)
                    {
                        sessionProduct.Product.IsActive = false;
                    }

                    if (Customer.ClubCardId != null)
                    {
                        var saledProduct = new ProductViewModel()
                        {
                            Name = sessionProduct.Product.Name,
                            Price = sessionProduct.Product.Price * (100 - sessionProduct.Product.Discount.DiscountAmount) / 100,
                            Amount = sessionProduct.Amount
                        };

                        saledProducts.Add(saledProduct);
                    }
                    else
                    {
                        var saledProduct = new ProductViewModel()
                        {
                            Name = sessionProduct.Product.Name,
                            Price = sessionProduct.Product.Price,
                            Amount = sessionProduct.Amount,
                        };

                        saledProducts.Add(saledProduct);
                    }
                }
            }
            double TotalPayment = 0;

            foreach (var product in saledProducts)
            {
                TotalPayment =+ product.Price * product.Amount;
            }


            Sale sale = new Sale()
            {
                CustomerId = Customer.Id,
                Products = saledProductList,
                TotalPrice = TotalPayment
            };
            await saleRepository.AddAsync(sale); 
            await CloseSession(finalizeSaleParameters.SessionId);

            return PrintReceipt(Customer, saledProducts);
        }
        public async Task UpdateSale(UpdateSaleParameters updateSaleParameters)
        {
            var Sale = await saleRepository.GetAll()
                .Include(p => p.Products)
                .SingleAsync(a => a.Id == updateSaleParameters.SaleId);
            
            if (Sale == null)
            {
                throw new SaleNotFoundException($"Sale with Id {updateSaleParameters.SaleId} not found");
            }
            
            var Product = await productRepository.GetByIdAsync(updateSaleParameters.ProductId);
            if (Product == null)
            {
                throw new ProductNotFoundException($"Product with Id {updateSaleParameters.ProductId} not found");
            }
            
            Sale.TotalPrice -= Product.Price;
            Sale.Products.Remove(Product);

            Product.QuantityInStock += 1;

            await saleRepository.UpdateAsync(Sale);
            await productRepository.UpdateAsync(Product);
        }

        private string PrintReceipt(Customer customer, List<ProductViewModel> products)
        {
            StringBuilder receipt = new StringBuilder(); //Temel olarak, StringBuilder büyük miktarda metin verisiyle çalışırken performansı artırmak için kullanılır

            receipt.AppendLine("-- Receipt --"); //Append metodu, StringBuilder sınıfında kullanılan ve StringBuilder nesnesine yeni bir metin eklemek için kullanılan bir metottur.
            receipt.AppendLine($"Müşteri: {customer.Name}");
            receipt.AppendLine("Ürünler:");
            var totalpayment = products.Sum(p => p.Price * p.Amount);
            
            foreach (var product in products)
            {
                receipt.AppendLine($"- Ürün Adı: {product.Name}, Urun Fiyati: {product.Price}, Adet: {product.Amount} ,Fiyat : {product.Price * product.Amount}" );
            }

            receipt.AppendLine("---------------------------------------------");
            receipt.AppendLine($"Toplam Tutar : {totalpayment}");

            return receipt.ToString();
        }
        
    }
   
}
