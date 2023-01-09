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
    [Route("api/v1/event")]
    [ApiController]
    [ApiKeyAuth]
    public class EventApiController : ControllerBase
    {
        private readonly sloveniatrips _context;

        public EventApiController(sloveniatrips context)
        {
            _context = context;
        }

        // GET: api/EventApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
          if (_context.Events == null)
          {
              return NotFound();
          }
            return await _context.Events.ToListAsync();
        }

        // GET: api/EventApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
          if (_context.Events == null)
          {
              return NotFound();
          }
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/EventApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.ID)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/EventApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
          if (_context.Events == null)
          {
              return Problem("Entity set 'sloveniatrips.Events'  is null.");
          }
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.ID }, @event);
        }

        // DELETE: api/EventApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return (_context.Events?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [HttpGet]
        [Route("region/{regionId}/{profileId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEventsByRegion(int regionId, string profileId)
        {
            if (_context.Events == null)
            {
                return NotFound();
            }

            // Find the events in the specified region
            var events = await _context.Events.Where(e => e.regionID == regionId).ToListAsync();
            if (events == null)
            {
                return NotFound();
            }

            // Find the profile_has_events records for the given profile
            var profileHasEvents = await _context.Profile_Has_Events.Where(phe => phe.ProfileID == profileId).ToListAsync();

            // Remove the events that the profile has already signed up for from the list
            events = events.Where(e => !profileHasEvents.Any(phe => phe.EventID == e.ID)).ToList();

            return events;
        }
        
        [HttpGet]
        [Route("profile/past/{profileId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetPastEventsByProfile(string profileId)
        {
            if (_context.Profile_Has_Events == null || _context.Events == null)
            {
                return NotFound();
            }

            // Find the events that the profile is registered for
            var profileEvents = await _context.Profile_Has_Events.Where(p => p.ProfileID == profileId).ToListAsync();
            if (profileEvents == null)
            {
                return NotFound();
            }

            // Find the event entities for the events that the profile is registered for
            var events = new List<Event>();
            foreach (var profileEvent in profileEvents)
            {
                var evt = await _context.Events.FindAsync(profileEvent.EventID);
                if (evt != null)
                {
                    events.Add(evt);
                }
            }

            // Return only the events that have already happened (current date is after the event date)
            return events.Where(e => e.Date < DateTime.Now).ToList();
        }

        [HttpGet]
        [Route("profile/upcoming/{profileId}")]
        public async Task<ActionResult<IEnumerable<Event>>> GetUpcomingEventsByProfile(string profileId)
        {
            if (_context.Profile_Has_Events == null || _context.Events == null)
            {
                return NotFound();
            }

            // Find the events that the profile is registered for
            var profileEvents = await _context.Profile_Has_Events.Where(p => p.ProfileID == profileId).ToListAsync();
            if (profileEvents == null)
            {
                return NotFound();
            }

            // Find the event entities for the events that the profile is registered for
            var events = new List<Event>();
            foreach (var profileEvent in profileEvents)
            {
                var evt = await _context.Events.FindAsync(profileEvent.EventID);
                if (evt != null)
                {
                    events.Add(evt);
                }
            }

            // Return only the events that have not happened yet (current date is before the event date)
            return events.Where(e => e.Date >= DateTime.Now).ToList();
        }
    }
}
