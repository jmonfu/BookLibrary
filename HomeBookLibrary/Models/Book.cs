using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBookLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int ISBN { get; set; }
        public string Summary { get; set; }
        public bool IsAvailable { get; set; }

        // Foreign Key
        public int AuthorId { get; set; }
        // Navigation property
        public Author Author { get; set; }

        // Foreign Key
        public int GenreId { get; set; }
        // Navigation property
        public Genre Genre { get; set; }

    }
}