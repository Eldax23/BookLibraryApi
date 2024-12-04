using Demo.Models.DB.Entites;
using Demo.Models.ViewModels;

namespace Demo.Models.DB.Repository.Books
{
    public interface IBooksRepository
    {
        Task<List<BookViewModel>> GetAllAsync();
        Task<BookViewModel> GetByIdAsync(int id);
        Task AddBookAsync(BookViewModel model);
        Task UpdateBookAsync(int id , BookViewModel model);
        Task DeleteBookAsync(int id);

        Task<int> GetBookIdByTitle(string title);
        Task<string> GetBookTitleById(int id);

        
    }
}
