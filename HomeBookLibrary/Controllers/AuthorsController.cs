using System.Data.Entity.Infrastructure;
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
    public class AuthorsController : BaseController
    {
        private IUnitOfWork UnitOfWork;

        public AuthorsController()
        {
            UnitOfWork = new UnitOfWork();
        }

        public AuthorsController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // GET: api/Authors
        public IQueryable<AuthorDTO> GetAuthors()
        {
            return UnitOfWork.AuthorsRepository.GetAuthors("").ProjectTo<AuthorDTO>();
        }

        // GET: api/Authors/5
        [ResponseType(typeof (AuthorDTO))]
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            var item = await Task.Run(() => UnitOfWork.AuthorsRepository.GetAuthorById(id));

            AuthorDTO dtoDetail;

            if (item != null)
            {
                dtoDetail = Mapper.Map<AuthorDTO>(item);
            }
            else
            {
                return NotFound();
            }

            return Ok(dtoDetail);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof (void))]
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

            UnitOfWork.AuthorsRepository.Update(author);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (UnitOfWork.AuthorsRepository.GetAuthorById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        [ResponseType(typeof (AuthorDTO))]
        public IHttpActionResult PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UnitOfWork.AuthorsRepository.Insert(author);
            UnitOfWork.Save();

            var outputDto = Mapper.Map<AuthorDTO>(author);

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, outputDto);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof (AuthorDTO))]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            var item = await Task.Run(() => UnitOfWork.AuthorsRepository.GetAuthorById(id));
            if (item == null)
            {
                return NotFound();
            }

            UnitOfWork.AuthorsRepository.Delete(id);
            UnitOfWork.Save();

            var dto = Mapper.Map<BookDTO>(item);

            return Ok(dto);
        }
    }
}