using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HomeBookLibrary.DAL;

namespace HomeBookLibrary.Models
{
    public class Genre : IEntityWithId
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}