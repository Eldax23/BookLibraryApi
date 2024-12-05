namespace Demo.Models.ViewModels
{
    public class BorrowingViewModel
    {

        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime BorrowDate { get; set; }
        public string Status { get; set; }
        public double FinesAmount { get; set; }
        public Guid CustomerId { get; set; }

        public int CopyId {  get; set; }

    }
}
