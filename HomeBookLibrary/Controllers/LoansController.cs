using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using HomeBookLibrary.DAL;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class LoansController : BaseController
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Loans
        public IQueryable<LoanDTO> GetLoans()
        {
            var loanDto = new LoanDTO();
            return GetAll(unitOfWork.LoanRepository, loanDto);
        }

        // GET: api/Loans/5
        [ResponseType(typeof (LoanDetailsDTO))]
        public async Task<IHttpActionResult> GetLoan(int id)
        {
            var loanDto = new LoanDTO();
            return await GetById(unitOfWork.LoanRepository, loanDto, id);
        }

        // PUT: api/Loans/5
        [ResponseType(typeof (void))]
        public IHttpActionResult PutLoan(int id, Loan loan)
        {
            return Put(unitOfWork.LoanRepository, id, loan);
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
            var book = await Task.Run(() => unitOfWork.BookRepository.GetById(loan.BookId));

            if (book != null)
            {
                book.IsAvailable = false;
                unitOfWork.BookRepository.Update(book);
                unitOfWork.LoanRepository.Insert(loan);
            }

            unitOfWork.Save();
            var dto = Mapper.Map<LoanDTO>(loan);
            return CreatedAtRoute("DefaultApi", new {id = loan.Id}, dto);
        }

        // DELETE: api/Loans/5
        [ResponseType(typeof (LoanDTO))]
        public async Task<IHttpActionResult> DeleteLoan(int id)
        {
            var loan = await Task.Run(() => unitOfWork.LoanRepository.GetById(id));

            if (loan == null)
            {
                return NotFound();
            }

            //set the book back to available
            var book = await Task.Run(() => unitOfWork.BookRepository.GetById(loan.BookId));
            if (book != null)
            {
                book.IsAvailable = true;
                unitOfWork.BookRepository.Update(book);
                unitOfWork.LoanRepository.Delete(id);
            }

            unitOfWork.Save();

            var dto = Mapper.Map<LoanDTO>(loan);

            return Ok(dto);
        }
    }
}