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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBorrowing(int id, [FromBody] BorrowingViewModel newVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await borrowingsRepository.UpdateBorrowing(id , newVM);
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
