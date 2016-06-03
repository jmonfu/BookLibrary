using System;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private GenericRepository<Author> _authoRepository;
        private GenericRepository<Book> _bookRepository;
        private GenericRepository<Genre> _genreRepository;
        private GenericRepository<Loan> _loanRepository;
        private readonly HomeBookLibraryContext context = new HomeBookLibraryContext();

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public GenericRepository<Book> BookRepository
        {
            get
            {
                if (_bookRepository == null)
                {
                    _bookRepository = new GenericRepository<Book>(context);
                }
                return _bookRepository;
            }
        }

        public GenericRepository<Author> AuthorRepository
        {
            get
            {
                if (_authoRepository == null)
                {
                    _authoRepository = new GenericRepository<Author>(context);
                }
                return _authoRepository;
            }
        }

        public GenericRepository<Loan> LoanRepository
        {
            get
            {
                if (_loanRepository == null)
                {
                    _loanRepository = new GenericRepository<Loan>(context);
                }
                return _loanRepository;
            }
        }

        public GenericRepository<Genre> GenreRepository
        {
            get
            {
                if (_genreRepository == null)
                {
                    _genreRepository = new GenericRepository<Genre>(context);
                }
                return _genreRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }
    }
}