using Demo.Models.DB.Entites;
using Demo.Models.DB.Repository.Books;
using Demo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models.DB.Repository.BookCopies
{
    public class BookCopiesRepository : IBookCopiesRepository
    {
        private readonly AppDbContext context;
        private readonly IBooksRepository booksService;

        public BookCopiesRepository(AppDbContext _context , IBooksRepository booksService)
        {
            context = _context;
            this.booksService = booksService;
        }
        public async Task AddBookCopyAsync(BookCopyViewModel model)
        {

            int BookID = await booksService.GetBookIdByTitle(model.BookName);
            if (BookID == -1) throw new Exception("Book Name Doesn't Exists");
            BookCopy bookCopy = new BookCopy()
            {
                BookId = BookID,
                Status = model.Status,
            };

            context.BookCopies.Add(bookCopy);
            await context.SaveChangesAsync();
        }

        public Task DeleteBookCopyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookCopyViewModel>> GetAllAsync()
        {

            List<BookCopyViewModel> bookCopyViewModels = new List<BookCopyViewModel>();
            List<BookCopy> bookCopies = await context.BookCopies.ToListAsync();
            foreach (BookCopy bc in bookCopies)
            {
                string bookTitle = await booksService.GetBookTitleById(bc.BookId);
                BookCopyViewModel model = new BookCopyViewModel()
                {
                    Id = bc.Id,
                    BookName = bookTitle,
                    Status = bc.Status,
                };
                bookCopyViewModels.Add(model);
            }

            return bookCopyViewModels;
        }

        public async Task<BookCopyViewModel> GetBookCopyById(int id)
        {
            BookCopy? bookCopy = await context.BookCopies.FirstOrDefaultAsync(bc => bc.Id == id);
            if (bookCopy != null)
            {
                BookCopyViewModel viewModel = new BookCopyViewModel()
                {
                    Id = bookCopy.Id,
                    BookName = await booksService.GetBookTitleById(bookCopy.BookId),
                    Status = bookCopy.Status,
                };

                return viewModel;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateBookCopy(int id, BookCopyViewModel newModel)
        {
            BookCopy? oldCopy = context.BookCopies.FirstOrDefault(bc => bc.Id == id);
            if (oldCopy != null)
            {
                oldCopy.BookId = await booksService.GetBookIdByTitle(newModel.BookName);
                oldCopy.Status = newModel.Status;
            }
            else
            {
                throw new Exception("Book Copy Not Found");
            }

        }
    }
}
