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
    public class BooksController : BaseController
    {
        private BooksRepository _bookRepository = new BooksRepository();

        public BooksController()
        {
        }

        public BooksController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            return _bookRepository.GetBooks("").ProjectTo<BookDTO>();
        }


        // GET: api/Books/5
        [ResponseType(typeof (BookDetailDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var includeProperties = "Author,Genre";
            var item = await Task.Run(() => _bookRepository.GetBookById(id, includeProperties));

            BookDetailDTO dtoDetail;

            if (item != null)
            {
                dtoDetail = Mapper.Map<BookDetailDTO>(item);
            }
            else
            {
                return NotFound();
            }

            return Ok(dtoDetail);
        }

        [HttpGet]
        public IQueryable<BookDTO> BookFilter(int authorId, int titleId, int genreId, int isbn)
        {
            var books = UnitOfWork.BookRepository.GetBooks();
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
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            _bookRepository.Update(book);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_bookRepository.GetBookById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(BookDTO))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookRepository.Insert(book);
            UnitOfWork.Save();

            var outputDto = Mapper.Map<BookDTO>(book);

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, outputDto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(BookDTO))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            var item = await Task.Run(() => _bookRepository.GetBookById(id));
            if (item == null)
            {
                return NotFound();
            }

            _bookRepository.Delete(id);
            UnitOfWork.Save();

            var dto = Mapper.Map<BookDTO>(item);

            return Ok(dto);
        }
    }
}