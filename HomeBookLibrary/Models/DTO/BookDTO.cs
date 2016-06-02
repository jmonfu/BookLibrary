using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBookLibrary.Models.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public bool IsAvailable { get; set; }
        public int ISBN { get; set; }
        public int GenreId { get; set; }
    }
}