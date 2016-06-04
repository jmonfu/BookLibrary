using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public interface IUnitOfWork
    {
        BooksRepository BookRepository { get; }
        AuthorsRepository AuthorsRepository { get; }
        GenresRepository GenresRepository { get; }
        LoansRepository LoansRepository { get; }
        //GenericRepository<Author> AuthorRepository { get; }
        //GenericRepository<Book> BookRepository { get; }
        //GenericRepository<Loan> LoanRepository { get; }
        //GenericRepository<Genre> GenreRepository { get; }
        void Save();
        void Dispose();
    }
}