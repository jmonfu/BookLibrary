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
        internal HomeBookLibraryContext context = new HomeBookLibraryContext();
        internal DbSet<Loan> dbSet;

        public LoansRepository()
        {
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
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(Loan entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            context.SaveChanges();
        }

        public void Update(Loan entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}