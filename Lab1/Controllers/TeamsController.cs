//I, Cael Phillips, student number 000397240, certify that this material is my original work.
//No other person's work has been used without due acknowledgement and I have not made my work available to anyone else.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab1.Data;
using Lab1.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lab1.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// GET: Teams
        [Authorize(Roles = "Manager,Player")]
        public async Task<IActionResult> Index() {
            return View(await _context.Team.ToListAsync());
        }

        // GET: Teams/Details/5
        [Authorize(Roles = "Manager,Player")]
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Team == null) {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamID == id);
            if (team == null) {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create() {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamID,TeamName,Email,EstablishedDate")] Team team) {
            if (ModelState.IsValid) {
                _context.Add(team);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Team == null) {
                return NotFound();
            }

            var team = await _context.Team.FindAsync(id);
            if (team == null) {
                return NotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("TeamID,TeamName,Email,EstablishedDate")] Team team) {
            if (id != team.TeamID) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) {
                    if (!TeamExists(team.TeamID)) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Team == null) {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamID == id);
            if (team == null) {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [Authorize(Roles = "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Team == null) {
                return Problem("Entity set 'ApplicationDbContext.Team'  is null.");
            }
            var team = await _context.Team.FindAsync(id);
            if (team != null) {
                _context.Team.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id) {
            return _context.Team.Any(e => e.TeamID == id);
        }
    }
}
