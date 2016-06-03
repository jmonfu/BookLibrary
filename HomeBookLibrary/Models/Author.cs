using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HomeBookLibrary.DAL;

namespace HomeBookLibrary.Models
{
    public class Author : IEntityWithId
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}