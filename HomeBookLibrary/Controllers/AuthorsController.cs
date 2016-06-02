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
using AutoMapper.QueryableExtensions;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class AuthorsController : ApiController
    {
        private HomeBookLibraryContext db = new HomeBookLibraryContext();

        // GET: api/Authors
        public IQueryable<AuthorDTO> GetAuthors()
        {
            return db.Authors.ProjectTo<AuthorDTO>();
        }

        // GET: api/Authors/5
        [ResponseType(typeof(AuthorDTO))]
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            var author = await db.Authors.FindAsync(id);
            var dto = AutoMapper.Mapper.Map<AuthorDTO>(author);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            db.Entry(author).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        [ResponseType(typeof(AuthorDTO))]
        public async Task<IHttpActionResult> PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(author);
            await db.SaveChangesAsync();

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(author);

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, dto);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(AuthorDTO))]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            var author = await db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            await db.SaveChangesAsync();

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(author);

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

        private bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.Id == id) > 0;
        }
    }
}