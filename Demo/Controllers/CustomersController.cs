using Demo.Models.DB.Repository.Customers;
using Demo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {
            this.customersRepository = customersRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            List<CustomersViewModel> result = await customersRepository.GetAll();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id , [FromBody] CustomersViewModel newCustomer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await customersRepository.UpdateCustomer(id, newCustomer);
                    return StatusCode(StatusCodes.Status204NoContent);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
