using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HomeBookLibrary.DAL;
using Microsoft.SqlServer.Server;

namespace HomeBookLibrary.Models
{
    public class Loan : IEntityWithId
    {
        public int Id { get; set; }
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
    }
}