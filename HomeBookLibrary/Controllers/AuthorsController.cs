using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HomeBookLibrary.DAL;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class AuthorsController : BaseController
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Authors
        public IQueryable<AuthorDTO> GetAuthors()
        {
            var authorDto = new AuthorDTO();
            return GetAll(unitOfWork.AuthorRepository, authorDto);
        }

        // GET: api/Authors/5
        [ResponseType(typeof (AuthorDTO))]
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            var authorDto = new AuthorDTO();
            return await GetById(unitOfWork.AuthorRepository, authorDto, id);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> PutAuthor(int id, Author author)
        {
            return Put(unitOfWork.AuthorRepository, id, author);
        }

        // POST: api/Authors
        [ResponseType(typeof (AuthorDTO))]
        public IHttpActionResult PostAuthor(Author author)
        {
            var authorDto = new AuthorDTO();
            return Post(unitOfWork.AuthorRepository, authorDto, author);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof (AuthorDTO))]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            var authorDto = new AuthorDTO();
            return await Delete(unitOfWork.AuthorRepository, authorDto, id);
        }
    }
}