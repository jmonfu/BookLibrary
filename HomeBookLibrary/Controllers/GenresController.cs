using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using HomeBookLibrary.DAL;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class GenresController : BaseController
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Genres
        public IQueryable<GenreDTO> GetGenres()
        {
            var genreDto = new GenreDTO();
            return GetAll(unitOfWork.GenreRepository, genreDto);
        }

        // GET: api/Genres/5
        [ResponseType(typeof (GenreDTO))]
        public async Task<IHttpActionResult> GetGenre(int id)
        {
            var genreDto = new GenreDTO();
            return await GetById(unitOfWork.GenreRepository, genreDto, id);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutGenre(int id, Genre genre)
        {
            return Put(unitOfWork.GenreRepository, id, genre);
        }

        // POST: api/Genres
        [ResponseType(typeof (GenreDTO))]
        public IHttpActionResult PostGenre(Genre genre)
        {
            var genreDto = new GenreDTO();
            return Post(unitOfWork.GenreRepository, genreDto, genre);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof (GenreDTO))]
        public async Task<IHttpActionResult> DeleteGenre(int id)
        {
            var genreDto = new GenreDTO();
            return await Delete(unitOfWork.GenreRepository, genreDto, id);
        }
    }
}