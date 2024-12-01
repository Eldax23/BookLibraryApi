using System.ComponentModel.DataAnnotations;

namespace Demo.Models.ViewModels
{
    public class RegisterViewModel
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Address {  get; set; }

        [MaxLength(50)]
        public string UserName { get; set; }
        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
