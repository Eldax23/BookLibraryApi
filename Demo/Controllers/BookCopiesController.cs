using System.Diagnostics.Eventing.Reader;
using Demo.Models.DB.Repository.BookCopies;
using Demo.Models.DB.Repository.Books;
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
        private readonly IBooksRepository booksRepository;

        public BookCopiesController(IBookCopiesRepository bookCopiesRepository , IBooksRepository booksRepository)
        {
            this.bookCopiesRepository = bookCopiesRepository;
            this.booksRepository = booksRepository;
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBookCopy(int id , BookCopyViewModel newModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(await booksRepository.GetBookIdByTitle(newModel.BookName) == -1)
                    {
                        return BadRequest("BookTitle Must Exist");
                    }
                    await bookCopiesRepository.UpdateBookCopy(id, newModel);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
               await bookCopiesRepository.DeleteBookCopyAsync(id);
                return Ok();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }


    }
}
