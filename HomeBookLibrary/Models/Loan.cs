using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBookLibrary.Models
{
    public class Loan
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public DateTime DateLoaned { get; set; }

        // Foreign Key
        public int BookId { get; set; }
        // Navigation property
        public Book Book { get; set; }
    }
}