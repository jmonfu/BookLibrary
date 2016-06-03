using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper.QueryableExtensions;
using HomeBookLibrary.DAL;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            var bookDto = new BookDTO();
            return GetAll(unitOfWork.BookRepository, bookDto);
        }


        // GET: api/Books/5
        [ResponseType(typeof (BookDetailDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var includeProperties = "Author,Genre";
            var bookDetailDto = new BookDetailDTO();
            return await GetById(unitOfWork.BookRepository, bookDetailDto, id, includeProperties);
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
        [ResponseType(typeof (void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            return Put(unitOfWork.BookRepository, id, book);
        }

        // POST: api/Books
        [ResponseType(typeof (BookDTO))]
        public IHttpActionResult PostBook(Book book)
        {
            var bookDto = new BookDTO();
            return Post(unitOfWork.BookRepository, bookDto, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof (BookDTO))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            var bookDto = new BookDTO();
            return await Delete(unitOfWork.BookRepository, bookDto, id);
        }
    }
}