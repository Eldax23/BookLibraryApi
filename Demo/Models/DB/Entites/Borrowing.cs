namespace Demo.Models.DB.Entites
{
    public class Borrowing
    {

        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime BorrowDate { get; set; }
        public string Status { get; set; }
        public double FinesAmount { get; set; }
        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }
        public int CopyId {  get; set; }

        public BookCopy BookCopy { get; set; }
    }
}
