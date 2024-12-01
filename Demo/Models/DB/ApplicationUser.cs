using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Demo.Models.DB
{
    public class ApplicationUser : IdentityUser
    {
        public Guid CustomerID;
        public Customer Customer { get; set; }

    }
}
