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
    public class GenresController : ApiController
    {
        private HomeBookLibraryContext db = new HomeBookLibraryContext();

        // GET: api/Genres
        public IQueryable<GenreDTO> GetGenres()
        {
            return db.Genres.ProjectTo<GenreDTO>();
        }

        // GET: api/Genres/5
        [ResponseType(typeof(GenreDTO))]
        public async Task<IHttpActionResult> GetGenre(int id)
        {
            var genre = await db.Genres.FindAsync(id);
            var dto = AutoMapper.Mapper.Map<GenreDTO>(genre);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.Id)
            {
                return BadRequest();
            }

            db.Entry(genre).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // POST: api/Genres
        [ResponseType(typeof(GenreDTO))]
        public async Task<IHttpActionResult> PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Genres.Add(genre);
            await db.SaveChangesAsync();

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(genre);

            return CreatedAtRoute("DefaultApi", new { id = genre.Id }, dto);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof(GenreDTO))]
        public async Task<IHttpActionResult> DeleteGenre(int id)
        {
            var genre = await db.Genres.FindAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(genre);
            await db.SaveChangesAsync();

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(genre);

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

        private bool GenreExists(int id)
        {
            return db.Genres.Count(e => e.Id == id) > 0;
        }
    }
}