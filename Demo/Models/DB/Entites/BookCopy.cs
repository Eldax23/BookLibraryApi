﻿using Demo.Models.Enums;

namespace Demo.Models.DB.Entites
{
    public class BookCopy
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }

        public string  Status { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; }
    }
}
