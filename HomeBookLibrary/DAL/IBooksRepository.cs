using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public interface IBooksRepository
    {
        IQueryable<Book> GetBooks(string includeProperties = "");
        Book GetBookById(int id, string includeProperties = "");
        void Insert(Book entity);
        void Delete(int id);
        void Delete(Book entityToDelete);
        void Update(Book entityToUpdate);
    }
}
