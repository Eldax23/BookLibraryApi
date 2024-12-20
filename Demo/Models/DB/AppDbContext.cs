﻿using Demo.Models.DB.Entites;
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

            builder.Entity<BookCopy>().HasOne(bc => bc.Book)
                .WithMany(b => b.BookCopies)
                .HasForeignKey(bc => bc.BookId);


            
            builder.Entity<Borrowing>().HasOne(b => b.BookCopy)
                .WithMany(bc => bc.Borrowings)
                .HasForeignKey(b => b.CopyId);

            builder.Entity<Borrowing>().HasOne(b => b.Customer)
                .WithMany(c => c.Borrowings)
                .HasForeignKey(b => b.CustomerId);
            

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<BookCopy> BookCopies { get; set; }

        public DbSet<Borrowing> Borrowings { get; set; }
    }
}
