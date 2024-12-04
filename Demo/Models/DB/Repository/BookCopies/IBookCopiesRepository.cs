using Demo.Models.ViewModels;

namespace Demo.Models.DB.Repository.BookCopies
{
    public interface IBookCopiesRepository
    {
        Task<List<BookCopyViewModel>> GetAllAsync();

        Task<BookCopyViewModel> GetBookCopyById(int id);
        Task AddBookCopyAsync(BookCopyViewModel model);
        Task UpdateBookCopy(int id, BookCopyViewModel newModel);

        Task DeleteBookCopyAsync(int id);
    }
}
