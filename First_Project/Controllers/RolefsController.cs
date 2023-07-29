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
    public class RolefsController : Controller
    {
        private readonly ModelContext _context;

        public RolefsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Rolefs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View(await _context.Rolefs.ToListAsync());
        }

        // GET: Rolefs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var rolef = await _context.Rolefs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolef == null)
            {
                return NotFound();
            }

            return View(rolef);
        }

        // GET: Rolefs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }

        // POST: Rolefs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rolename")] Rolef rolef)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rolef);
        }

        // GET: Rolefs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var rolef = await _context.Rolefs.FindAsync(id);
            if (rolef == null)
            {
                return NotFound();
            }
            return View(rolef);
        }

        // POST: Rolefs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Rolename")] Rolef rolef)
        {
            if (id != rolef.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolef);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolefExists(rolef.Id))
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
            return View(rolef);
        }

        // GET: Rolefs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var rolef = await _context.Rolefs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolef == null)
            {
                return NotFound();
            }

            return View(rolef);
        }

        // POST: Rolefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var rolef = await _context.Rolefs.FindAsync(id);
            _context.Rolefs.Remove(rolef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolefExists(decimal id)
        {
            return _context.Rolefs.Any(e => e.Id == id);
        }
    }
}
