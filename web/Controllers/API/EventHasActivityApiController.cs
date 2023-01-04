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
    [Route("api/v1/eventhasactivity")]
    [ApiController]
    public class EventHasActivityApiController : ControllerBase
    {
        private readonly sloveniatrips _context;

        public EventHasActivityApiController(sloveniatrips context)
        {
            _context = context;
        }

        // GET: api/EventHasActivityApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event_has_activity>>> GetEvent_has_activities()
        {
          if (_context.Event_has_activities == null)
          {
              return NotFound();
          }
            return await _context.Event_has_activities.ToListAsync();
        }

        // GET: api/EventHasActivityApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event_has_activity>> GetEvent_has_activity(int id)
        {
          if (_context.Event_has_activities == null)
          {
              return NotFound();
          }
            var event_has_activity = await _context.Event_has_activities.FindAsync(id);

            if (event_has_activity == null)
            {
                return NotFound();
            }

            return event_has_activity;
        }

        // PUT: api/EventHasActivityApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent_has_activity(int id, Event_has_activity event_has_activity)
        {
            if (id != event_has_activity.ID)
            {
                return BadRequest();
            }

            _context.Entry(event_has_activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Event_has_activityExists(id))
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

        // POST: api/EventHasActivityApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event_has_activity>> PostEvent_has_activity(Event_has_activity event_has_activity)
        {
          if (_context.Event_has_activities == null)
          {
              return Problem("Entity set 'sloveniatrips.Event_has_activities'  is null.");
          }
            _context.Event_has_activities.Add(event_has_activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent_has_activity", new { id = event_has_activity.ID }, event_has_activity);
        }

        // DELETE: api/EventHasActivityApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent_has_activity(int id)
        {
            if (_context.Event_has_activities == null)
            {
                return NotFound();
            }
            var event_has_activity = await _context.Event_has_activities.FindAsync(id);
            if (event_has_activity == null)
            {
                return NotFound();
            }

            _context.Event_has_activities.Remove(event_has_activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Event_has_activityExists(int id)
        {
            return (_context.Event_has_activities?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
