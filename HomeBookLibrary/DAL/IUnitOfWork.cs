using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public interface IUnitOfWork
    {
        IBooksRepository BookRepository { get; }
        IAuthorsRepository AuthorsRepository { get; }
        IGenresRepository GenresRepository { get; }
        ILoansRepository LoansRepository { get; }
        void Save();
        void Dispose();
    }
}