using System;
using System.ComponentModel.DataAnnotations;
using HomeBookLibrary.DAL;

namespace HomeBookLibrary.Models
{
    public class Loan : IEntityWithId
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public DateTime DateLoaned { get; set; }
        public string Comments { get; set; }

        // Foreign Key
        public int BookId { get; set; }
        // Navigation property
        public Book Book { get; set; }
        public int Id { get; set; }
    }
}