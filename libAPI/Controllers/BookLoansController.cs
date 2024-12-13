using libAPI.Data;
using libAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoansController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookLoansController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/BookLoans
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookLoan>>> GetBookLoans()
        {
            try
            {
                return await _context.BookLoans.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // GET: api/BookLoans/1
        [HttpGet("{id}")]
        public async Task<ActionResult<BookLoan>> GetBookLoan(int id)
        {
            try
            {
                var bookLoan = await _context.BookLoans.FindAsync(id);

                if (bookLoan == null)
                {
                    return NotFound(new { message = "BookLoan not found." });
                }

                return bookLoan;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // PUT: api/BookLoans/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookLoan(int id, BookLoan bookLoan)
        {
            if (id != bookLoan.LoanID)
            {
                return BadRequest(new { message = "BookLoan ID mismatch." });
            }

            _context.Entry(bookLoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BookLoanExists(id))
                {
                    return NotFound(new { message = "BookLoan not found." });
                }
                else
                {
                    return StatusCode(500, new { message = $"A concurrency error occurred: {ex.Message}" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }

            return NoContent();
        }

        // POST: api/BookLoans
        [HttpPost]
        public async Task<ActionResult<BookLoan>> PostBookLoan(BookLoan bookLoan)
        {
            try
            {
                _context.BookLoans.Add(bookLoan);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBookLoan", new { id = bookLoan.LoanID }, bookLoan);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        // DELETE: api/BookLoans/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookLoan>> DeleteBookLoan(int id)
        {
            try
            {
                var bookLoan = await _context.BookLoans.FindAsync(id);
                if (bookLoan == null)
                {
                    return NotFound(new { message = "BookLoan not found." });
                }

                _context.BookLoans.Remove(bookLoan);
                await _context.SaveChangesAsync();

                return bookLoan;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        private bool BookLoanExists(int id)
        {
            return _context.BookLoans.Any(e => e.LoanID == id);
        }
    }
}
