using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public interface IUnitOfWork
    {
        GenericRepository<Book> BookRepository { get; }
        GenericRepository<Author> AuthorRepository { get; }
        GenericRepository<Loan> LoanRepository { get; }
        GenericRepository<Genre> GenreRepository { get; }
        void Save();
        void Dispose();
    }
}