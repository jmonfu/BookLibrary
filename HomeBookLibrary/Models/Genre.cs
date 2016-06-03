using System.ComponentModel.DataAnnotations;
using HomeBookLibrary.DAL;

namespace HomeBookLibrary.Models
{
    public class Genre : IEntityWithId
    {
        [Required]
        public string Type { get; set; }

        public int Id { get; set; }
    }
}