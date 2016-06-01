using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBookLibrary.Models.DTO
{
    public class BookDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ISBN { get; set; }
        public string Summary { get; set; }
        public bool IsAvailable { get; set; }
        public string AuthorName { get; set; }
        public string GenreType { get; set; }
    }
}