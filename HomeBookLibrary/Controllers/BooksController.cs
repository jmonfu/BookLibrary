﻿using System;
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
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class BooksController : ApiController
    {
        private HomeBookLibraryContext db = new HomeBookLibraryContext();

        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            return db.Books.ProjectTo<BookDTO>();
        }


        // GET: api/Books/5
        [ResponseType(typeof(BookDetailDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await db.Books.Include(a => a.Author).Include(g => g.Genre).SingleOrDefaultAsync(b => b.Id == id);
            var dto = AutoMapper.Mapper.Map<BookDetailDTO>(book);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpGet]
        public IQueryable<BookDTO> BookFilter(int authorId, int titleId, int genreId, int isbn)
        {
            var books = db.Books.ProjectTo<BookDTO>();
            var filteredBooks = books;
            if (authorId > 0)
                filteredBooks = filteredBooks.Where(x => x.AuthorId == authorId);
            if (titleId > 0)
                filteredBooks = filteredBooks.Where(x => x.Id == titleId);
            if (genreId > 0)
                filteredBooks = filteredBooks.Where(x => x.GenreId == genreId);
            if (isbn > 0)
                filteredBooks = filteredBooks.Where(x => x.Id == isbn);

            return filteredBooks;
        }


        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();

            var dto = AutoMapper.Mapper.Map<BookDTO>(book);

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(BookDTO))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            var book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            var dto = AutoMapper.Mapper.Map<BookDTO>(book);

            return Ok(dto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}