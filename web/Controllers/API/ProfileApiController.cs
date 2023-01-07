using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using web.Filters;
namespace web.Controllers_API
{
    [Route("api/v1/profile")]
    [ApiController]
    [ApiKeyAuth]
    public class ProfileApiController : ControllerBase
    {
        private readonly sloveniatrips _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileApiController(UserManager<ApplicationUser> userManager,sloveniatrips context)
        {
            _context = context;
            _userManager = userManager;
        }

        public class UserUpdateProfileModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string PhoneNumber { get; set; }
            public int regionID { get; set; }
            public DateTime dob { get; set; }
        }

        // GET: api/ProfileApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetProfiles()
        {
          if (_context.Profiles == null)
          {
              return NotFound();
          }
            return await _context.Profiles.ToListAsync();
        }

        // GET: api/ProfileApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUser>> GetApplicationUser(string id)
        {
          if (_context.Profiles == null)
          {
              return NotFound();
          }
            var applicationUser = await _context.Profiles.FindAsync(id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            return applicationUser;
        }

        // PUT: api/ProfileApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        //PUT with user defined
        [HttpPut("userupdate/{id}")]
        public async Task<IActionResult> PutApplicationUser(string id, [FromBody]UserUpdateProfileModel model)
        {
            var applicationUser = await _context.Profiles.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            var parts = model.Name.Split(" ");
            applicationUser.FirstName = parts[0];
            applicationUser.LastName = parts[1];
            applicationUser.Email = model.Email;
            applicationUser.PhoneNumber = model.PhoneNumber;
            applicationUser.regionID = model.regionID;
            applicationUser.DoB = model.dob;
            applicationUser.UserName = model.Username;
            applicationUser.SecurityStamp = Guid.NewGuid().ToString();

        // Check if the new username is already taken
            var usernameTaken = await _userManager.FindByNameAsync(model.Username);
            if (usernameTaken != null && usernameTaken.Id != applicationUser.Id)
            {
                return Conflict("Username is already taken.");
            }

            // Check if the new email is already taken
            var emailTaken = await _userManager.FindByEmailAsync(model.Email);
            if (emailTaken != null && emailTaken.Id != applicationUser.Id)
            {
                return Conflict("Email is already taken.");
            }
            
            // Update the user's name and email
            var result = await _userManager.SetUserNameAsync(applicationUser, model.Username);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            result = await _userManager.SetEmailAsync(applicationUser, model.Email);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            result = await _userManager.UpdateAsync(applicationUser);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Save the changes to the database
            _context.Entry(applicationUser).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/ProfileApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> PostApplicationUser(ApplicationUser applicationUser)
        {
          if (_context.Profiles == null)
          {
              return Problem("Entity set 'sloveniatrips.Profiles'  is null.");
          }
            _context.Profiles.Add(applicationUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/ProfileApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser(string id)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            var applicationUser = await _context.Profiles.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(applicationUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationUserExists(string id)
        {
            return (_context.Profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
