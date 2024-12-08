using Demo.Models.DB.Entites;
using Demo.Models.DB.Repository.BookCopies;
using Demo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models.DB.Repository.Borrowings
{
    public class BorrowingsRepository : IBorrowingsRepository
    {
        private readonly AppDbContext context;

        public BorrowingsRepository(AppDbContext context)
        {
            this.context = context;
        }

        private BorrowingViewModel MapToVm(Borrowing borrow)
        {
            BorrowingViewModel vm = new BorrowingViewModel()
            {
                Id = borrow.Id,
                DueDate = borrow.DueDate,
                BorrowDate = borrow.DueDate,
                Status = borrow.Status,
                FinesAmount = borrow.FinesAmount,
                CustomerId = borrow.CustomerId,
                CopyId = borrow.CopyId,

            };

            return vm;

        }

        private Borrowing MapVm(BorrowingViewModel borrow)
        {
            Borrowing model = new Borrowing()
            {
                Id = borrow.Id,
                DueDate = borrow.DueDate,
                BorrowDate = borrow.DueDate,
                Status = borrow.Status,
                FinesAmount = borrow.FinesAmount,
                CustomerId = borrow.CustomerId,
                CopyId = borrow.CopyId,
            };

            return model;

        }
        public async Task BorrowBookAsync(BorrowingViewModel model)
        {
            Borrowing vm = MapVm(model);
            context.Borrowings.Add(vm);
            await context.SaveChangesAsync();
        }

        public async Task ReturnBookAsync(int id)
        {
            Borrowing? borrowing = await context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);
            if (borrowing != null)
            {
                context.Borrowings.Remove(borrowing);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("This Borrowing Record Doesn't Exist");
            }
        }

        public async Task<List<BorrowingViewModel>> GetAll()
        {
            List<BorrowingViewModel> borrowingsResult = await context.Borrowings.Select(b => new BorrowingViewModel()
            {
                Id = b.Id,
                DueDate = b.DueDate,
                BorrowDate = b.BorrowDate,
                FinesAmount = b.FinesAmount,
                Status = b.Status,
                CustomerId = b.CustomerId,
                CopyId = b.CopyId,

            }).ToListAsync();

            return borrowingsResult;
        }

        public async Task<BorrowingViewModel> GetBorrowingById(int id)
        {
            Borrowing? borrowing = await context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);
            if(borrowing != null)
            {
                BorrowingViewModel result = MapToVm(borrowing);
                return result;
            }
            else
            {
                throw new Exception("This Borrowing Record Doesn't Exist");
            }
        }

        public async Task UpdateBorrowing(int id, BorrowingViewModel newModel)
        {
            Borrowing? borrowing = await context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);
            if (borrowing != null)
            {
                borrowing.BorrowDate = newModel.BorrowDate;
                borrowing.Status = newModel.Status;
                borrowing.DueDate = newModel.DueDate;
                borrowing.CopyId = newModel.CopyId;
                borrowing.CustomerId = newModel.CustomerId;
                borrowing.FinesAmount = newModel.FinesAmount;
                await context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Borrowing Does not exist");
            }
        }

        public async Task<List<BorrowingViewModel>> BorrowedBooksByCustomerIdAsync(Guid customerId)
        {
            List<BorrowingViewModel> result = await context.Borrowings.Where(b => b.CustomerId == customerId).Select(b => MapToVm(b)).ToListAsync();
            return result;
        }
    }
}
