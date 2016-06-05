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
        internal HomeBookLibraryContext Context = new HomeBookLibraryContext();
        internal DbSet<Book> dbSet;

        public BooksRepository(HomeBookLibraryContext context)
        {
            Context = context;
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
            Context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(Book entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public virtual void Update(Book entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
        }

    }
}