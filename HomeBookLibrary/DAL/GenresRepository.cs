using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public class GenresRepository : IGenresRepository
    {
        internal HomeBookLibraryContext Context = new HomeBookLibraryContext();
        internal DbSet<Genre> dbSet;

        public GenresRepository(HomeBookLibraryContext context)
        {
            Context = context;
            dbSet = context.Set<Genre>();
        }

        public IQueryable<Genre> GetGenres(string includeProperties = "")
        {
            IQueryable<Genre> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Genre GetGenreById(int id, string includeProperties = "")
        {
            IQueryable<Genre> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(x => x.Id == id);
        }

        public void Insert(Genre entity)
        {
            dbSet.Add(entity);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(Genre entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public void Update(Genre entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}