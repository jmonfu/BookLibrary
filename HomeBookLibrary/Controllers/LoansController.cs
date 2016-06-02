using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper.QueryableExtensions;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.Controllers
{
    public class LoansController : ApiController
    {
        private HomeBookLibraryContext db = new HomeBookLibraryContext();

        // GET: api/Loans
        public IQueryable<LoanDTO> GetLoans()
        {
            return db.Loans.ProjectTo<LoanDTO>();
        }

        // GET: api/Loans/5
        [ResponseType(typeof(LoanDetailsDTO))]
        public IHttpActionResult GetLoan(int id)
        {
            var loan = db.Loans.Find(id);
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

            db.Entry(loan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
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
        public IHttpActionResult PostLoan(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //also update the isAvailable parameter inside the book table
            var book = db.Books.FirstOrDefault(x => x.Id == loan.BookId);
            if (book != null)
            {
                book.IsAvailable = false;

                db.Loans.Add(loan);
                db.Entry(book).State = EntityState.Modified;
            }

            db.SaveChanges();

            var dto = AutoMapper.Mapper.Map<LoanDTO>(loan);

            return CreatedAtRoute("DefaultApi", new { id = loan.Id }, dto);
        }

        // DELETE: api/Loans/5
        [ResponseType(typeof(LoanDTO))]
        public IHttpActionResult DeleteLoan(int id)
        {
            var loan = db.Loans.Find(id);
            if (loan == null)
            {
                return NotFound();
            }

            //set the book back to available
            var book = db.Books.FirstOrDefault(x => x.Id == loan.BookId);
            if (book != null)
            {
                book.IsAvailable = true;

                db.Loans.Remove(loan);
                db.Entry(book).State = EntityState.Modified;
            }

            db.SaveChanges();

            var dto = AutoMapper.Mapper.Map<LoanDTO>(loan);

            return Ok(dto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoanExists(int id)
        {
            return db.Loans.Count(e => e.Id == id) > 0;
        }
    }
}