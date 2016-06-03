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
    public class LoansController : ApiController
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();

        // GET: api/Loans
        public IQueryable<LoanDTO> GetLoans()
        {
            return unitOfWork.LoanRepository.Get().ProjectTo<LoanDTO>();
        }

        // GET: api/Loans/5
        [ResponseType(typeof(LoanDetailsDTO))]
        public async Task<IHttpActionResult> GetLoan(int id)
        {
            var loan = await Task.Run(() => unitOfWork.LoanRepository.GetById(id));
            var dto = AutoMapper.Mapper.Map<LoanDetailsDTO>(loan);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // PUT: api/Loans/5
        [ResponseType(typeof(void))]
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

            unitOfWork.LoanRepository.Update(loan);

            try
            {
                unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
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

        // POST: api/Loans
        [ResponseType(typeof(LoanDTO))]
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
            var dto = AutoMapper.Mapper.Map<LoanDTO>(loan);
            return CreatedAtRoute("DefaultApi", new { id = loan.Id }, dto);
        }

        // DELETE: api/Loans/5
        [ResponseType(typeof(LoanDTO))]
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

            var dto = AutoMapper.Mapper.Map<LoanDTO>(loan);

            return Ok(dto);
        }

        private bool LoanExists(int id)
        {
            return unitOfWork.LoanRepository.GetById(id) != null;
        }


    }
}