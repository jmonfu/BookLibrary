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
using AutoMapper.QueryableExtensions;
using HomeBookLibrary.DAL;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class GenresController : ApiController
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Genres
        public IQueryable<GenreDTO> GetGenres()
        {
            return unitOfWork.GenreRepository.Get().ProjectTo<GenreDTO>();
        }

        // GET: api/Genres/5
        [ResponseType(typeof(GenreDTO))]
        public async Task<IHttpActionResult> GetGenre(int id)
        {
            var genre = await Task.Run(() => unitOfWork.GenreRepository.GetById(id));
            var dto = AutoMapper.Mapper.Map<GenreDTO>(genre);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGenre(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genre.Id)
            {
                return BadRequest();
            }

            unitOfWork.GenreRepository.Update(genre);

            try
            {
                unitOfWork.Save();
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
        public IHttpActionResult PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.GenreRepository.Insert(genre);
            unitOfWork.Save();

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(genre);

            return CreatedAtRoute("DefaultApi", new { id = genre.Id }, dto);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof(GenreDTO))]
        public async Task<IHttpActionResult> DeleteGenre(int id)
        {
            var genre = await Task.Run(() => unitOfWork.GenreRepository.GetById(id));

            if (genre == null)
            {
                return NotFound();
            }

            unitOfWork.GenreRepository.Delete(id);
            unitOfWork.Save();

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(genre);

            return Ok(dto);
        }

        private bool GenreExists(int id)
        {
            return unitOfWork.GenreRepository.GetById(id) != null;
        }

    }
}