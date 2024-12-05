using Demo.Models.DB.Entites;
using Demo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models.DB.Repository.Customers
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly AppDbContext context;

        public CustomersRepository(AppDbContext context)
        {
            this.context = context;
        }



        public async Task<List<CustomersViewModel>> GetAll()
        {
            List<CustomersViewModel> customersViewModels = await context.Customers.Select(c => new CustomersViewModel()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DateOfBirth = c.DateOfBirth,
                Address = c.Address,

            }).ToListAsync();

            return customersViewModels;
        }

        public async Task<CustomersViewModel> GetCustomerByName(string name)
        {
            Customer? customer = await context.Customers.FirstOrDefaultAsync(c => c.FirstName + c.LastName == name);
            if (customer != null)
            {
                CustomersViewModel model = new CustomersViewModel()
                {
                    Id= customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth,
                    Address = customer.Address,
                };

                return model;
            }
            else
            {
                throw new Exception("Customer With that name doesn't exist");
            }
        }

        public async Task UpdateCustomer(Guid id, CustomersViewModel customer)
        {
            Customer? oldCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (oldCustomer != null)
            {
                oldCustomer.FirstName = customer.FirstName;
                oldCustomer.LastName = customer.LastName;
                oldCustomer.Address = customer.Address;
                oldCustomer.DateOfBirth = customer.DateOfBirth;
                await context.SaveChangesAsync();
            }
            else
            {

                throw new Exception("Customer With that name doesn't exist");
            }
        }
    }
}
