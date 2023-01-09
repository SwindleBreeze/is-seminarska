using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Filters;
namespace web.Controllers_API
{
    [Route("api/v1/review")]
    [ApiController]
    [ApiKeyAuth]
    public class ReviewApiController : ControllerBase
    {
        private readonly sloveniatrips _context;

        public ReviewApiController(sloveniatrips context)
        {
            _context = context;
        }

        // GET: api/ReviewApi
        [HttpGet]
        [ApiKeyAuth]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
          if (_context.Reviews == null)
          {
              return NotFound();
          }
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/ReviewApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
          if (_context.Reviews == null)
          {
              return NotFound();
          }
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/ReviewApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.ID)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReviewApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
          if (_context.Reviews == null)
          {
              return Problem("Entity set 'sloveniatrips.Reviews'  is null.");
          }
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReview", new { id = review.ID }, review);
        }

        // DELETE: api/ReviewApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // [HttpPost("upsert")]
        // public async Task<ActionResult<Review>> UpsertReview(string profileId, int eventId, string comment, int grade)
        // {
        //     // Check if a review for the given event and profile already exists
        //     var existingReview = await _context.Reviews.FirstOrDefaultAsync(
        //         r => r.EventID == eventId && r.ProfileID == profileId
        //     );

        //     // If a review already exists, update it
        //     if (existingReview != null)
        //     {
        //         existingReview.grade = grade;
        //         existingReview.comment = comment;
        //         _context.Entry(existingReview).State = EntityState.Modified;
        //     }
        //     // If a review doesn't already exist, insert a new one
        //     else
        //     {
        //         _context.Reviews.Add(new Review { ProfileID = profileId, EventID = eventId, grade = grade, comment = comment });
        //     }

        //     await _context.SaveChangesAsync();
        //     return NoContent();
        // }

        [HttpPost("upsert")]
        public async Task<ActionResult<Review>> UpsertReview(string comment, int grade, string profileId, int eventId)
        {
            // Check if a review for the given event and profile already exists
            var existingReview = await _context.Reviews.FirstOrDefaultAsync(
                r => r.EventID == eventId && r.ProfileID == profileId
            );

            // If a review already exists, update it
            if (existingReview != null)
            {
                existingReview.grade = grade;
                existingReview.comment = comment;
                _context.Entry(existingReview).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return existingReview;
            }

            // If a review does not already exist, create a new one
            var review = new Review
            {
                comment = comment,
                grade = grade,
                ProfileID = profileId,
                EventID = eventId
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetReview", new { id = review.ID }, review);
        }

        private bool ReviewExists(int id)
        {
            return (_context.Reviews?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
