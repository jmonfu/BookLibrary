using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public class BooksRepository : IBooksRepository
    {
        internal HomeBookLibraryContext context = new HomeBookLibraryContext();
        internal DbSet<Book> dbSet;

        public BooksRepository()
        {
            dbSet = context.Set<Book>();
        }

        public virtual IQueryable<Book> GetBooks(string includeProperties = "")
        {

            IQueryable<Book> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public virtual Book GetBookById(int id, string includeProperties = "")
        {
            IQueryable<Book> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(x => x.Id == id);
        }

        public virtual void Insert(Book entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Book entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }

        public virtual void Update(Book entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }

    }
}