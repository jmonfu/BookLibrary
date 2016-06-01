using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeBookLibrary.Models.DTO
{
    public class LoanDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateLoaned { get; set; }
        public string BookTitle { get; set; }

    }
}