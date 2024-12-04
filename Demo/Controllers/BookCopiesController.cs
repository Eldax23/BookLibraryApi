using System.Diagnostics.Eventing.Reader;
using Demo.Models.DB.Repository.BookCopies;
using Demo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly IBookCopiesRepository bookCopiesRepository;
        public BookCopiesController(IBookCopiesRepository bookCopiesRepository)
        {
            this.bookCopiesRepository = bookCopiesRepository;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllCopies()
        {
            List<BookCopyViewModel> result = await bookCopiesRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                BookCopyViewModel? bookCopyViewModel = await bookCopiesRepository.GetBookCopyById(id);
                if (bookCopyViewModel == null) return BadRequest();
                return Ok(bookCopyViewModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
       }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewCopy([FromBody]BookCopyViewModel bookCopyViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await bookCopiesRepository.AddBookCopyAsync(bookCopyViewModel);
                    return Ok();
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
