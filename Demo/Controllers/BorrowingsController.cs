using Demo.Models.DB.Repository.Borrowings;
using Demo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly IBorrowingsRepository borrowingsRepository;

        public BorrowingsController(IBorrowingsRepository borrowingsRepository)
        {
            this.borrowingsRepository = borrowingsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBorrowings()
        {
            List<BorrowingViewModel> result = await borrowingsRepository.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowingViewModel borrowing)
        {
            if (ModelState.IsValid)
            {
                await borrowingsRepository.BorrowBookAsync(borrowing);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState); 
            }
        }
    }
}
