using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AttributeRouting.Web.Mvc;
using AutoMapper.QueryableExtensions;
using HomeBookLibrary.DAL;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class BooksController : ApiController
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            return unitOfWork.BookRepository.Get().ProjectTo<BookDTO>();
        }


        // GET: api/Books/5
        [ResponseType(typeof(BookDetailDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await Task.Run(() => unitOfWork.BookRepository.GetById(id, includeProperties:"Author,Genre"));

            BookDetailDTO dto = null;

            if (book != null)
            {
                dto = AutoMapper.Mapper.Map<BookDetailDTO>(book);
            }
            else
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpGet]
        public IQueryable<BookDTO> BookFilter(int authorId, int titleId, int genreId, int isbn)
        {
            var books = unitOfWork.BookRepository.Get();
            var filteredBooks = books;
            if (authorId > 0)
                filteredBooks = filteredBooks.Where(x => x.AuthorId == authorId);
            if (titleId > 0)
                filteredBooks = filteredBooks.Where(x => x.Id == titleId);
            if (genreId > 0)
                filteredBooks = filteredBooks.Where(x => x.GenreId == genreId);
            if (isbn > 0)
                filteredBooks = filteredBooks.Where(x => x.Id == isbn);

            return filteredBooks.ProjectTo<BookDTO>();
        }


        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            unitOfWork.BookRepository.Update(book);

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(BookDTO))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.BookRepository.Insert(book);
            unitOfWork.Save();

            var dto = AutoMapper.Mapper.Map<BookDTO>(book);

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(BookDTO))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            var book = await Task.Run(() => unitOfWork.BookRepository.GetById(id));
            if (book == null)
            {
                return NotFound();
            }

            unitOfWork.BookRepository.Delete(id);
            unitOfWork.Save();

            var dto = AutoMapper.Mapper.Map<BookDTO>(book);

            return Ok(dto);
        }

        private bool BookExists(int id)
        {
            return unitOfWork.BookRepository.GetById(id) != null;
        }
    }
}