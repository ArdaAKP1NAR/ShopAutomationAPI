using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using ShopAutomationAPI.Shared.Parameters;
using ShopLibrary.Models;
using ShopLibrary.Repositories;
using ShopLibrary.Repositories.Interface;
using System.Text.RegularExpressions;

namespace ShopAutomationAPI.Service
{
    public class CustomerService(ICustomerRepository customerRepository,IClubCardRepository clubCardRepository)
    {
        public async Task AddCustomerWithClubCard(Customer customer,long ClubcardId)
        {
            var regex = new Regex(@"^[a-zA-Z\s]+$");

            if (!regex.IsMatch(customer.Name))
            {
                throw new InvalidCustomerNameException("Name can only contain letters");
            }
            
            var newCustomer = await customerRepository.AddAsync(customer);
            var Clubcard = await clubCardRepository.GetByIdAsync(ClubcardId);
            if (Clubcard == null)
            {
                throw new ClubCardNotFoundException($"Clubcard with Id {ClubcardId} not found");
            }
            
            newCustomer.ClubCardId = ClubcardId;
            await customerRepository.UpdateAsync(customer);
        }
        public async Task AddCustomer(Customer customer)
        {
            var regex = new Regex(@"^[a-zA-Z\s]+$");

            if (!regex.IsMatch(customer.Name))
            {
                throw new InvalidCustomerNameException("Name can only contain letters");
            }
            await customerRepository.AddAsync(customer);
        }
        public async Task UpdateCustomer(UpdateCustomerParameters customerParameters)
        {
            var Customer = await customerRepository.GetByIdAsync(customerParameters.CustomerId);
            if(Customer == null)
            {
                throw new CustomerNotFoundException($"Customer with Id {customerParameters.CustomerId} not found");
            }
            var Clubcard = await clubCardRepository.GetByIdAsync(customerParameters.ClubCardId);
            if ( Clubcard == null)
            {
                throw new ClubCardNotFoundException($"Clubcard with Id {customerParameters.ClubCardId} not found");
            }
            Customer.ClubCardId = customerParameters.ClubCardId;

            await customerRepository.UpdateAsync(Customer);
        }
    }
}
