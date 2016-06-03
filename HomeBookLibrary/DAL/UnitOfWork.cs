using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.DAL
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private HomeBookLibraryContext context = new HomeBookLibraryContext();
        private GenericRepository<Book> _bookRepository;
        private GenericRepository<Author> _authoRepository;
        private GenericRepository<Loan> _loanRepository;
        private GenericRepository<Genre> _genreRepository;

        public GenericRepository<Book> BookRepository
        {
            get
            {
                if (this._bookRepository == null)
                {
                    this._bookRepository = new GenericRepository<Book>(context);
                }
                return _bookRepository;
            }
        }

        public GenericRepository<Author> AuthorRepository
        {
            get
            {
                if (this._authoRepository == null)
                {
                    this._authoRepository = new GenericRepository<Author>(context);
                }
                return _authoRepository;
            }
        }

        public GenericRepository<Loan> LoanRepository
        {
            get
            {
                if (this._loanRepository == null)
                {
                    this._loanRepository = new GenericRepository<Loan>(context);
                }
                return _loanRepository;
            }
        }

        public GenericRepository<Genre> GenreRepository
        {
            get
            {
                if (this._genreRepository == null)
                {
                    this._genreRepository = new GenericRepository<Genre>(context);
                }
                return _genreRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}