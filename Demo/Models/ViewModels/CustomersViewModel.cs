using System.ComponentModel.DataAnnotations;

namespace Demo.Models.ViewModels
{
    public class CustomersViewModel
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]

        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
