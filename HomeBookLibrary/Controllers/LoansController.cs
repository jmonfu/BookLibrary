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
    public class LoansController : BaseController
    {
        private LoansRepository _loansRepository = new LoansRepository();
        private BooksRepository _booksRepository = new BooksRepository();

        public LoansController()
        {
        }

        public LoansController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // GET: api/Loans
        public IQueryable<LoanDTO> GetLoans()
        {
            return _loansRepository.GetLoans("").ProjectTo<LoanDTO>();
        }

        // GET: api/Loans/5
        [ResponseType(typeof (LoanDetailsDTO))]
        public async Task<IHttpActionResult> GetLoan(int id)
        {
            var item = await Task.Run(() => _loansRepository.GetLoanById(id));

            LoanDetailsDTO dtoDetail;

            if (item != null)
            {
                dtoDetail = Mapper.Map<LoanDetailsDTO>(item);
            }
            else
            {
                return NotFound();
            }

            return Ok(dtoDetail);
        }

        // PUT: api/Loans/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutLoan(int id, Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loan.Id)
            {
                return BadRequest();
            }

            _loansRepository.Update(loan);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_loansRepository.GetLoanById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Loans
        [ResponseType(typeof (LoanDTO))]
        public async Task<IHttpActionResult> PostLoan(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //also update the isAvailable parameter inside the book table            
            var book = await Task.Run(() => _booksRepository.GetBookById(loan.BookId));

            if (book != null)
            {
                book.IsAvailable = false;
                _booksRepository.Update(book);
                _loansRepository.Insert(loan);
            }

            var dto = Mapper.Map<LoanDTO>(loan);
            return CreatedAtRoute("DefaultApi", new {id = loan.Id}, dto);
        }

        // DELETE: api/Loans/5
        [ResponseType(typeof (LoanDTO))]
        public async Task<IHttpActionResult> DeleteLoan(int id)
        {
            var loan = await Task.Run(() => _loansRepository.GetLoanById(id));

            if (loan == null)
            {
                return NotFound();
            }

            //set the book back to available
            var book = await Task.Run(() => _booksRepository.GetBookById(loan.BookId));
            if (book != null)
            {
                book.IsAvailable = true;
                _booksRepository.Update(book);
                _loansRepository.Delete(id);
            }

            var dto = Mapper.Map<LoanDTO>(loan);

            return Ok(dto);
        }
    }
}