using System.ComponentModel.DataAnnotations;
using HomeBookLibrary.DAL;

namespace HomeBookLibrary.Models
{
    public class Author : IEntityWithId
    {
        [Required]
        public string Name { get; set; }

        public int Id { get; set; }
    }
}