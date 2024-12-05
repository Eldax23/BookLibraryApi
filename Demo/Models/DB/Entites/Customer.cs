using System.ComponentModel.DataAnnotations;

namespace Demo.Models.DB.Entites
{
    public class Customer
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]

        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; }

    }
}
