using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public class AuthorsRepository : IAuthorsRepository
    {
        internal HomeBookLibraryContext context = new HomeBookLibraryContext();
        internal DbSet<Author> dbSet;

        public AuthorsRepository()
        {
            dbSet = context.Set<Author>();
        }

        public IQueryable<Author> GetAuthors(string includeProperties = "")
        {
            IQueryable<Author> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Author GetAuthorById(int id, string includeProperties = "")
        {
            IQueryable<Author> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(x => x.Id == id);
        }

        public void Insert(Author entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(Author entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }

        public void Update(Author entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}