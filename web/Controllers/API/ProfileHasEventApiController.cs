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
    [Route("api/v1/profilehasevent")]
    [ApiController]
    [ApiKeyAuth]
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
        //new post 
        [HttpPost("create/{profileID}/{eventID}")]
        public async Task<ActionResult<Profile_Has_Events>> CreateProfileHasEvent(string profileID, int eventID)
        {
            // Look up the profile and event entities in the database
            var profile = await _context.Profiles.FindAsync(profileID);
            var evt = await _context.Events.FindAsync(eventID);
            if (profile == null || evt == null)
            {
                return NotFound();
            }

            // Check if the relationship already exists
            var existingRelationship = _context.Profile_Has_Events
                .Where(r => r.ProfileID == profileID && r.EventID == eventID)
                .SingleOrDefault();

            if (existingRelationship == null)
            {
                // Create a new Profile_Has_Events object and set its properties
                var profileHasEvent = new Profile_Has_Events();
                profileHasEvent.ProfileID = profile.Id;
                profileHasEvent.EventID = evt.ID;

                // Add the new object to the database and save the changes
                _context.Profile_Has_Events.Add(profileHasEvent);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProfile_Has_Events), new { id = profileHasEvent.ID }, profileHasEvent);
            }
            else
            {
                return Ok(existingRelationship);
            }
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
