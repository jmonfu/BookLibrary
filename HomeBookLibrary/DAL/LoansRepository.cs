using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public class LoansRepository : ILoansRepository
    {
        internal HomeBookLibraryContext Context = new HomeBookLibraryContext();
        internal DbSet<Loan> dbSet;

        public LoansRepository(HomeBookLibraryContext context)
        {
            Context = context;
            dbSet = context.Set<Loan>();
        }

        public IQueryable<Loan> GetLoans(string includeProperties = "")
        {
            IQueryable<Loan> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Loan GetLoanById(int id, string includeProperties = "")
        {
            IQueryable<Loan> query = dbSet;

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.SingleOrDefault(x => x.Id == id);
        }

        public void Insert(Loan entity)
        {
            dbSet.Add(entity);
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(Loan entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            Context.SaveChanges();
        }

        public void Update(Loan entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}