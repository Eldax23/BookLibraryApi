using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Demo.Models.DB
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        public string Address { get; set; }
    }
}
