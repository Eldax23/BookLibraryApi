using System.ComponentModel.DataAnnotations;

namespace Demo.Models.DB.Entites
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Author { get; set; }


        public string Image {  get; set; }

        [MaxLength(50)]

        public double Price { get; set; }
        

        public string ISBN { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public DateTime PublishedDate { get; set; }
        [MaxLength(50)]
        public string Publisher { get; set; }

        public int NumOfCopies { get; set; }

        public ICollection<BookCopy> BookCopies { get; set; }
    }
}
