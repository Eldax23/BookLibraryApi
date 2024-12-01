using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Demo.Models.DB
{
    public class ApplicationUser : IdentityUser
    {
        public Guid CustomerID;
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public Customer Customer { get; set; }

    }
}
