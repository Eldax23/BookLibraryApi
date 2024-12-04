using Demo.Models.DB.Entites;
using Demo.Models.DB.Repository.Books;
using Demo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<BookViewModel> books = await booksRepository.GetAllAsync();

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            BookViewModel bookVm = await booksRepository.GetByIdAsync(id);

            if (bookVm != null)
            {
                return Ok(bookVm);
            }
            else
            {
                return BadRequest("Book Not Found");
            }

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] BookViewModel bookVm)
        {
            if (ModelState.IsValid)
            {
                await booksRepository.AddBookAsync(bookVm);
                return CreatedAtAction(nameof(GetBookById), new { id = bookVm.Id }, bookVm);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookViewModel bookVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await booksRepository.UpdateBookAsync(id , bookVm);
                    return Ok(bookVm);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await booksRepository.DeleteBookAsync(id);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
            
            
    }
}
