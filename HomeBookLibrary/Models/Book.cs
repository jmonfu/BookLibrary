using System.ComponentModel.DataAnnotations;
using HomeBookLibrary.DAL;

namespace HomeBookLibrary.Models
{
    public class Book : IEntityWithId
    {
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
        public int Id { get; set; }
    }
}