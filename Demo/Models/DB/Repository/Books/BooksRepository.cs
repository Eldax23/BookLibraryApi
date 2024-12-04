using Demo.Models.DB.Entites;
using Demo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models.DB.Repository.Books
{
    public class BooksRepository : IBooksRepository
    {
        private readonly AppDbContext context;

        public BooksRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task AddBookAsync(BookViewModel model)
        {
            Book book = new Book()
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Genre = model.Genre,
                Price = model.Price,
                NumOfCopies = model.NumOfCopies,
                ISBN = model.ISBN,
                Image = model.Image,
                Publisher = model.Publisher, 
                PublishedDate = model.PublishedDate 
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(int id)
        {
            Book? book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Book Cannot Be Found.");
            }
        }

        public async Task<List<BookViewModel>> GetAllAsync()
        {
            List<BookViewModel> bookViewModels = await context.Books.Select(book => new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Price = book.Price,
                NumOfCopies = book.NumOfCopies,
                ISBN = book.ISBN,
                Image = book.Image,
                Publisher = book.Publisher, 
                PublishedDate = book.PublishedDate
            }).ToListAsync();

            return bookViewModels;
        }

        public async Task<string> GetBookTitleById(int id)
        {
            Book? book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book != null)
            {
                return book.Title;
            }
            else
            {
                return "Book Doesn't Exist";
            }
        }

        public async Task<int> GetBookIdByTitle(string title)
        {
            Book? book = await context.Books.FirstOrDefaultAsync(b => b.Title == title);
            if (book != null)
            {
                return book.Id;
            }
            else
            {
                return -1;
            }
        }

        public async Task<BookViewModel> GetByIdAsync(int id)
        {
           Book? book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                BookViewModel bookVm = new BookViewModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    Price = book.Price,
                    NumOfCopies = book.NumOfCopies,
                    ISBN = book.ISBN,
                    Image = book.Image,
                    Publisher = book.Publisher,
                    PublishedDate = book.PublishedDate
                };
                return bookVm;
            }
            else return null;
        }

        public async Task UpdateBookAsync(int id, BookViewModel model)
        {
            Book? book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                id = book.Id;
                book.Title = model.Title;
                book.Author = model.Author;
                book.Genre = model.Genre;
                book.Price = model.Price;
                book.NumOfCopies = model.NumOfCopies;
                book.ISBN = model.ISBN;
                book.Image = model.Image;
                book.Publisher = model.Publisher; 
                book.PublishedDate = model.PublishedDate;
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Book Not Found");
            }
        }
    }
}
