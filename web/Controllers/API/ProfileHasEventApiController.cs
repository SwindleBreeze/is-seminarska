using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers_API
{
    [Route("api/v1/profilehasevent")]
    [ApiController]
    public class ProfileHasEventApiController : ControllerBase
    {
        private readonly sloveniatrips _context;

        public ProfileHasEventApiController(sloveniatrips context)
        {
            _context = context;
        }

        // GET: api/ProfileHasEventApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile_Has_Events>>> GetProfile_Has_Events()
        {
          if (_context.Profile_Has_Events == null)
          {
              return NotFound();
          }
            return await _context.Profile_Has_Events.ToListAsync();
        }

        // GET: api/ProfileHasEventApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile_Has_Events>> GetProfile_Has_Events(int id)
        {
          if (_context.Profile_Has_Events == null)
          {
              return NotFound();
          }
            var profile_Has_Events = await _context.Profile_Has_Events.FindAsync(id);

            if (profile_Has_Events == null)
            {
                return NotFound();
            }

            return profile_Has_Events;
        }

        // PUT: api/ProfileHasEventApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile_Has_Events(int id, Profile_Has_Events profile_Has_Events)
        {
            if (id != profile_Has_Events.ID)
            {
                return BadRequest();
            }

            _context.Entry(profile_Has_Events).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Profile_Has_EventsExists(id))
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

        // POST: api/ProfileHasEventApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile_Has_Events>> PostProfile_Has_Events(Profile_Has_Events profile_Has_Events)
        {
          if (_context.Profile_Has_Events == null)
          {
              return Problem("Entity set 'sloveniatrips.Profile_Has_Events'  is null.");
          }
            _context.Profile_Has_Events.Add(profile_Has_Events);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfile_Has_Events", new { id = profile_Has_Events.ID }, profile_Has_Events);
        }

        // DELETE: api/ProfileHasEventApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile_Has_Events(int id)
        {
            if (_context.Profile_Has_Events == null)
            {
                return NotFound();
            }
            var profile_Has_Events = await _context.Profile_Has_Events.FindAsync(id);
            if (profile_Has_Events == null)
            {
                return NotFound();
            }

            _context.Profile_Has_Events.Remove(profile_Has_Events);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Profile_Has_EventsExists(int id)
        {
            return (_context.Profile_Has_Events?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
