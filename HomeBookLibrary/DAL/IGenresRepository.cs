using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public interface IGenresRepository
    {
        IQueryable<Genre> GetGenres(string includeProperties = "");
        Genre GetGenreById(int id, string includeProperties = "");
        void Insert(Genre entity);
        void Delete(int id);
        void Delete(Genre entityToDelete);
        void Update(Genre entityToUpdate);

    }
}
