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
    public class OrderfsController : Controller
    {
        private readonly ModelContext _context;

        public OrderfsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }
        // GET: Orderfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelContext = _context.Orderves.Include(o => o.Acc);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orderfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var orderf = await _context.Orderves
                .Include(o => o.Acc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderf == null)
            {
                return NotFound();
            }

            return View(orderf);
        }

        // GET: Orderfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname");
            return View();
        }

        // POST: Orderfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateOrder,AccId")] Orderf orderf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", orderf.AccId);
            return View(orderf);
        }

        // GET: Orderfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var orderf = await _context.Orderves.FindAsync(id);
            if (orderf == null)
            {
                return NotFound();
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", orderf.AccId);
            return View(orderf);
        }

        // POST: Orderfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,DateOrder,AccId")] Orderf orderf)
        {
            if (id != orderf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderfExists(orderf.Id))
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
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", orderf.AccId);
            return View(orderf);
        }

        // GET: Orderfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderf = await _context.Orderves
                .Include(o => o.Acc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderf == null)
            {
                return NotFound();
            }

            return View(orderf);
        }

        // POST: Orderfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var orderf = await _context.Orderves.FindAsync(id);
            _context.Orderves.Remove(orderf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderfExists(decimal id)
        {
            return _context.Orderves.Any(e => e.Id == id);
        }
    }
}
