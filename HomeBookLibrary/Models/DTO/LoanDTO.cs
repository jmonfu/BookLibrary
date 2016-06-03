using System;

namespace HomeBookLibrary.Models.DTO
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateLoaned { get; set; }
        public string Comments { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}