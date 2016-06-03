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
    public class AuthorsController : ApiController
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Authors
        public IQueryable<AuthorDTO> GetAuthors()
        {
            return unitOfWork.AuthorRepository.Get().ProjectTo<AuthorDTO>();
        }

        // GET: api/Authors/5
        [ResponseType(typeof(AuthorDTO))]
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            var author = await Task.Run(() => unitOfWork.AuthorRepository.GetById(id));
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

            unitOfWork.AuthorRepository.Update(author);

            try
            {
                unitOfWork.Save();
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
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            unitOfWork.AuthorRepository.Insert(author);
            unitOfWork.Save();

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(author);

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, dto);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(AuthorDTO))]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            var author = await Task.Run(() => unitOfWork.AuthorRepository.GetById(id));

            if (author == null)
            {
                return NotFound();
            }

            unitOfWork.AuthorRepository.Delete(id);
            unitOfWork.Save(); 

            var dto = AutoMapper.Mapper.Map<AuthorDTO>(author);

            return Ok(dto);
        }

        private bool AuthorExists(int id)
        {
            return unitOfWork.AuthorRepository.GetById(id) != null;
        }

    }
}