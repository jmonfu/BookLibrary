using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public interface ILoansRepository
    {
        IQueryable<Loan> GetLoans(string includeProperties = "");
        Loan GetLoanById(int id, string includeProperties = "");
        void Insert(Loan entity);
        void Delete(int id);
        void Delete(Loan entityToDelete);
        void Update(Loan entityToUpdate);

    }
}
