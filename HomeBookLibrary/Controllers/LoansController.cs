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
        private IUnitOfWork UnitOfWork;

        public LoansController()
        {
            UnitOfWork = new UnitOfWork();
        }

        public LoansController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // GET: api/Loans
        public IQueryable<LoanDTO> GetLoans()
        {
            return UnitOfWork.LoansRepository.GetLoans("").ProjectTo<LoanDTO>();
        }

        // GET: api/Loans/5
        [ResponseType(typeof (LoanDetailsDTO))]
        public async Task<IHttpActionResult> GetLoan(int id)
        {
            var item = await Task.Run(() => UnitOfWork.LoansRepository.GetLoanById(id));

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

            UnitOfWork.LoansRepository.Update(loan);

            try
            {
                UnitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (UnitOfWork.LoansRepository.GetLoanById(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Loans
        [ResponseType(typeof(LoanDTO))]
        public async Task<IHttpActionResult> PostLoan(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //also update the isAvailable parameter inside the book table            
            var book = await Task.Run(() => UnitOfWork.BookRepository.GetBookById(loan.BookId));

            if (book != null)
            {
                book.IsAvailable = false;
                UnitOfWork.BookRepository.Update(book);
                UnitOfWork.LoansRepository.Insert(loan);
            }

            var dto = Mapper.Map<LoanDTO>(loan);
            return CreatedAtRoute("DefaultApi", new { id = loan.Id }, dto);
        }

        // DELETE: api/Loans/5
        [ResponseType(typeof(LoanDTO))]
        public async Task<IHttpActionResult> DeleteLoan(int id)
        {
            var loan = await Task.Run(() => UnitOfWork.LoansRepository.GetLoanById(id));

            if (loan == null)
            {
                return NotFound();
            }

            //set the book back to available
            var book = await Task.Run(() => UnitOfWork.BookRepository.GetBookById(loan.BookId));
            if (book != null)
            {
                book.IsAvailable = true;
                UnitOfWork.BookRepository.Update(book);
                UnitOfWork.LoansRepository.Delete(id);
            }

            var dto = Mapper.Map<LoanDTO>(loan);

            return Ok(dto);
        }
    }
}