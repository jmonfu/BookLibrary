using System;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly HomeBookLibraryContext _context = new HomeBookLibraryContext();
        private IBooksRepository _booksRepository;

        private ILoansRepository _loansRepository;
        private IAuthorsRepository _authorsRepository;
        private IGenresRepository _genresRepository;


        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IBooksRepository BookRepository
        {
            get
            {
                if (_booksRepository == null)
                {
                    _booksRepository = new BooksRepository(_context);
                }
                return _booksRepository;
            }

        }

        public IAuthorsRepository AuthorsRepository
        {
            get
            {
                if (_authorsRepository == null)
                {
                    _authorsRepository = new AuthorsRepository(_context);
                }
                return _authorsRepository;
            }

        }

        public IGenresRepository GenresRepository
        {
            get
            {
                if (_genresRepository == null)
                {
                    _genresRepository = new GenresRepository(_context);
                }
                return _genresRepository;
            }

        }

        public ILoansRepository LoansRepository
        {
            get
            {
                if (_loansRepository == null)
                {
                    _loansRepository = new LoansRepository(_context);
                }
                return _loansRepository;
            }

        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }

}