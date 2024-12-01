using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.Models.DB
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<ApplicationUser>( u=> u.CustomerID);
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
