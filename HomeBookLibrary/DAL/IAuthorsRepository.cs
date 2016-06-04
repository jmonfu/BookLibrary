using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public interface IAuthorsRepository
    {
        IQueryable<Author> GetAuthors(string includeProperties = "");
        Author GetAuthorById(int id, string includeProperties = "");
        void Insert(Author entity);
        void Delete(int id);
        void Delete(Author entityToDelete);
        void Update(Author entityToUpdate);

    }
}
