using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using First_Project.Models;
using Microsoft.AspNetCore.Http;

namespace First_Project.Controllers
{
    public class TastimonielsController : Controller
    {
        private readonly ModelContext _context;

        public TastimonielsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Tastimoniels
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelContext = _context.Tastimoniels.Include(t => t.Acc);
            return View(await modelContext.ToListAsync());
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }
        // GET: Tastimoniels/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tastimoniel = await _context.Tastimoniels
                .Include(t => t.Acc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tastimoniel == null)
            {
                return NotFound();
            }

            return View(tastimoniel);
        }

        // GET: Tastimoniels/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname");
            return View();
        }

        // POST: Tastimoniels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,Status,Publishdate,AccId")] Tastimoniel tastimoniel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tastimoniel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", tastimoniel.AccId);
            return View(tastimoniel);
        }

        // GET: Tastimoniels/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var tastimoniel = await _context.Tastimoniels.FindAsync(id);
            if (tastimoniel == null)
            {
                return NotFound();
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", tastimoniel.AccId);
            return View(tastimoniel);
        }

        // POST: Tastimoniels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Message,Status,Publishdate,AccId")] Tastimoniel tastimoniel)
        {
            if (id != tastimoniel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tastimoniel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TastimonielExists(tastimoniel.Id))
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
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", tastimoniel.AccId);
            return View(tastimoniel);
        }

        // GET: Tastimoniels/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var tastimoniel = await _context.Tastimoniels
                .Include(t => t.Acc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tastimoniel == null)
            {
                return NotFound();
            }

            return View(tastimoniel);
        }

        // POST: Tastimoniels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var tastimoniel = await _context.Tastimoniels.FindAsync(id);
            _context.Tastimoniels.Remove(tastimoniel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TastimonielExists(decimal id)
        {
            return _context.Tastimoniels.Any(e => e.Id == id);
        }
    }
}
