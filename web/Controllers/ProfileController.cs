using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly sloveniatrips _context;

        public ProfileController(sloveniatrips context)
        {
            _context = context;
        }

        // GET: Profile
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var sloveniatrips = _context.Profiles.Include(a => a.region);
            return View(await sloveniatrips.ToListAsync());
        }

        // GET: Profile/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Profiles
                .Include(a => a.region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: Profile/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewData["regionID"] = new SelectList(_context.Regions, "ID", "ID");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileID,FirstName,LastName,City,password,regionID,registrationDate,DoB,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["regionID"] = new SelectList(_context.Regions, "ID", "ID", applicationUser.regionID);
            return View(applicationUser);
        }

        // GET: Profile/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Profiles.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            ViewData["regionID"] = new SelectList(_context.Regions, "ID", "ID", applicationUser.regionID);
            return View(applicationUser);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProfileID,FirstName,LastName,City,password,regionID,registrationDate,DoB,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["regionID"] = new SelectList(_context.Regions, "ID", "ID", applicationUser.regionID);
            return View(applicationUser);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Profiles == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.Profiles
                .Include(a => a.region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Profile/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Profiles == null)
            {
                return Problem("Entity set 'sloveniatrips.Profiles'  is null.");
            }
            var applicationUser = await _context.Profiles.FindAsync(id);
            if (applicationUser != null)
            {
                _context.Profiles.Remove(applicationUser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(string id)
        {
          return (_context.Profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
