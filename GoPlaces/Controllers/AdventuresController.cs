using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoPlaces.Data;
using GoPlaces.Models;
using Microsoft.AspNetCore.Identity;

namespace GoPlaces.Controllers
{
    public class AdventuresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdventuresController(ApplicationDbContext ctx,
                          UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = ctx;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        //GET: All Public Adventures
        public async Task<IActionResult> AllAdventuresIndex(string searchString)
        {
            var applicationDbContext = _context.Adventures.Where(a => a.IsPublic == true).Include(a => a.User).Include(a => a.Places);

            if (searchString != null)
            {
                applicationDbContext = _context.Adventures.Where(a => a.IsPublic == true && (a.Title.ToLower().Contains(searchString.ToLower()) || a.User.UserHandle.ToLower().Contains(searchString.ToLower()))).Include(a => a.User).Include(a => a.Places);
            }

            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Adventures
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.Adventures.Where(a => a.UserId == user.Id).Include(a => a.User).Include(a => a.Places);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Adventures/Details/5
        // first part of this view should be 2 buttons: 'add/check-into place' || 'see adventure deets'
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var user = await GetCurrentUserAsync();
            //ViewData["CurrentUserId"] = user.Id;

            var adventure = await _context.Adventures
                .Include(a => a.User)
                .Include(a => a.Places)
                .FirstOrDefaultAsync(m => m.AdventureId == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        // GET: Adventures/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();

            ViewData["UserId"] = user?.Id;
            return View();
        }

        // POST: Adventures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adventure adventure)
        {
           // adventure.DateCreated = DateTime.Now;
                _context.Adventures.Add(adventure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            //ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", adventure.UserId);
            //return View(adventure);
        }

        // GET: Adventures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.Adventures.FindAsync(id);
            if (adventure == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", adventure.UserId);
            return View(adventure);
        }

        // POST: Adventures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdventureId,PlaceId,Title,Description,UserId,DateCreated,IsPublic")] Adventure adventure)
        {
            if (id != adventure.AdventureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adventure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdventureExists(adventure.AdventureId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", adventure.UserId);
            return View(adventure);
        }

        // GET: Adventures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.Adventures
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.AdventureId == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        // POST: Adventures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adventure = await _context.Adventures.FindAsync(id);
            _context.Adventures.Remove(adventure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdventureExists(int id)
        {
            return _context.Adventures.Any(e => e.AdventureId == id);
        }
    }
}
