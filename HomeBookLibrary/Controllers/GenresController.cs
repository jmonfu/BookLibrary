﻿using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomeBookLibrary.DAL;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class GenresController : BaseController
    {
        private GenresRepository _genresRepository = new GenresRepository();

        public GenresController()
        {
        }

        public GenresController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // GET: api/Genres
        public IQueryable<GenreDTO> GetGenres()
        {
            return _genresRepository.GetGenres("").ProjectTo<GenreDTO>();
        }

        // GET: api/Genres/5
        [ResponseType(typeof (GenreDTO))]
        public async Task<IHttpActionResult> GetGenre(int id)
        {
            var item = await Task.Run(() => _genresRepository.GetGenreById(id));

            GenreDTO dtoDetail;

            if (item != null)
            {
                dtoDetail = Mapper.Map<GenreDTO>(item);
            }
            else
            {
                return NotFound();
            }

            return Ok(dtoDetail);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof (void))]
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

            _genresRepository.Update(genre);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_genresRepository.GetGenreById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Genres
        [ResponseType(typeof (GenreDTO))]
        public IHttpActionResult PostGenre(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _genresRepository.Insert(genre);
            UnitOfWork.Save();

            var outputDto = Mapper.Map<GenreDTO>(genre);

            return CreatedAtRoute("DefaultApi", new { id = genre.Id }, outputDto);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof (GenreDTO))]
        public async Task<IHttpActionResult> DeleteGenre(int id)
        {
            var item = await Task.Run(() => _genresRepository.GetGenreById(id));
            if (item == null)
            {
                return NotFound();
            }

            _genresRepository.Delete(id);
            UnitOfWork.Save();

            var dto = Mapper.Map<BookDTO>(item);

            return Ok(dto);
        }
    }
}