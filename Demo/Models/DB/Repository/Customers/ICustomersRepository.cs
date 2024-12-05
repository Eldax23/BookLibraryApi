using Demo.Models.DB.Entites;
using Demo.Models.ViewModels;

namespace Demo.Models.DB.Repository.Customers
{
    public interface ICustomersRepository
    {
        Task<List<CustomersViewModel>> GetAll();
        Task<CustomersViewModel> GetCustomerByName(string name);
        Task UpdateCustomer(Guid id , CustomersViewModel customer);

        
    }
}
