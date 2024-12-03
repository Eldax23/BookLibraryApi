namespace Demo.Models.ViewModels
{
    public class BookViewModel
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public double Price { get; set; }
        public int NumOfCopies { get; set; }
        public string ISBN { get; set; }
        public string Image { get; set; }
        public string Publisher { get; set; } 
        public DateTime PublishedDate { get; set; } 
    }
}
