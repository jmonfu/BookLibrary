using System;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private BooksRepository _booksRepository;
        private LoansRepository _loansRepository;
        private AuthorsRepository _authorsRepository;
        private GenresRepository _genresRepository;

        //private GenericRepository<Author> _authoRepository;
        //private GenericRepository<Book> _bookRepository;
        //private GenericRepository<Genre> _genreRepository;
        //private GenericRepository<Loan> _loanRepository;
        private readonly HomeBookLibraryContext _context = new HomeBookLibraryContext();

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public BooksRepository BookRepository
        {
            get
            {
                if (_booksRepository == null)
                {
                    _booksRepository = new BooksRepository();
                }
                return _booksRepository;
            }

        }

        public AuthorsRepository AuthorsRepository
        {
            get
            {
                if (_authorsRepository == null)
                {
                    _authorsRepository = new AuthorsRepository();
                }
                return _authorsRepository;
            }

        }

        public GenresRepository GenresRepository
        {
            get
            {
                if (_genresRepository == null)
                {
                    _genresRepository = new GenresRepository();
                }
                return _genresRepository;
            }

        }

        public LoansRepository LoansRepository
        {
            get
            {
                if (_loansRepository == null)
                {
                    _loansRepository = new LoansRepository();
                }
                return _loansRepository;
            }

        }

        //public GenericRepository<Book> BookRepository
        //{
        //    get
        //    {
        //        if (_bookRepository == null)
        //        {
        //            _bookRepository = new GenericRepository<Book>(_context);
        //        }
        //        return _bookRepository;
        //    }
        //}

        //public GenericRepository<Author> AuthorRepository
        //{
        //    get
        //    {
        //        if (_authoRepository == null)
        //        {
        //            _authoRepository = new GenericRepository<Author>(_context);
        //        }
        //        return _authoRepository;
        //    }
        //}

        //public GenericRepository<Loan> LoanRepository
        //{
        //    get
        //    {
        //        if (_loanRepository == null)
        //        {
        //            _loanRepository = new GenericRepository<Loan>(_context);
        //        }
        //        return _loanRepository;
        //    }
        //}

        //public GenericRepository<Genre> GenreRepository
        //{
        //    get
        //    {
        //        if (_genreRepository == null)
        //        {
        //            _genreRepository = new GenericRepository<Genre>(_context);
        //        }
        //        return _genreRepository;
        //    }
        //}


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