using Demo.Models.ViewModels;

namespace Demo.Models.DB.Repository.Borrowings
{
    public interface IBorrowingsRepository
    {
        Task<List<BorrowingViewModel>> GetAll();
        Task<BorrowingViewModel> GetBorrowingById(int id);
        Task BorrowBookAsync(BorrowingViewModel model);
        Task UpdateBorrowing(int id , BorrowingViewModel newModel);

        Task<List<BorrowingViewModel>> BorrowedBooksByCustomerIdAsync(Guid customerId);
        Task ReturnBookAsync(int id);
    }
}
