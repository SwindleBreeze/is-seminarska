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
    [Route("api/v1/region")]
    [ApiController]
    public class RegionApiController : ControllerBase
    {
        private readonly sloveniatrips _context;

        public RegionApiController(sloveniatrips context)
        {
            _context = context;
        }

        // GET: api/RegionApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
          if (_context.Regions == null)
          {
              return NotFound();
          }
            return await _context.Regions.ToListAsync();
        }

        // GET: api/RegionApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
        {
          if (_context.Regions == null)
          {
              return NotFound();
          }
            var region = await _context.Regions.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            return region;
        }

        // PUT: api/RegionApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegion(int id, Region region)
        {
            if (id != region.ID)
            {
                return BadRequest();
            }

            _context.Entry(region).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegionExists(id))
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

        // POST: api/RegionApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Region>> PostRegion(Region region)
        {
          if (_context.Regions == null)
          {
              return Problem("Entity set 'sloveniatrips.Regions'  is null.");
          }
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegion", new { id = region.ID }, region);
        }

        // DELETE: api/RegionApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            if (_context.Regions == null)
            {
                return NotFound();
            }
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegionExists(int id)
        {
            return (_context.Regions?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
