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
    public class PaymantfsController : Controller
    {
        private readonly ModelContext _context;

        public PaymantfsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Paymantfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelContext = _context.Paymantfs.Include(p => p.Paymant);
            return View(await modelContext.ToListAsync());
        }

        // GET: Paymantfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var paymantf = await _context.Paymantfs
                .Include(p => p.Paymant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymantf == null)
            {
                return NotFound();
            }

            return View(paymantf);
        }

        // GET: Paymantfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            ViewData["PaymantId"] = new SelectList(_context.ProductOrderves, "Id", "Id");
            return View();
        }

        // POST: Paymantfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Balance,PayDate,PaymantId")] Paymantf paymantf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymantf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymantId"] = new SelectList(_context.ProductOrderves, "Id", "Id", paymantf.PaymantId);
            return View(paymantf);
        }

        // GET: Paymantfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var paymantf = await _context.Paymantfs.FindAsync(id);
            if (paymantf == null)
            {
                return NotFound();
            }
            ViewData["PaymantId"] = new SelectList(_context.ProductOrderves, "Id", "Id", paymantf.PaymantId);
            return View(paymantf);
        }

        // POST: Paymantfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Balance,PayDate,PaymantId")] Paymantf paymantf)
        {
            if (id != paymantf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymantf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymantfExists(paymantf.Id))
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
            ViewData["PaymantId"] = new SelectList(_context.ProductOrderves, "Id", "Id", paymantf.PaymantId);
            return View(paymantf);
        }

        // GET: Paymantfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var paymantf = await _context.Paymantfs
                .Include(p => p.Paymant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymantf == null)
            {
                return NotFound();
            }

            return View(paymantf);
        }

        // POST: Paymantfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var paymantf = await _context.Paymantfs.FindAsync(id);
            _context.Paymantfs.Remove(paymantf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymantfExists(decimal id)
        {
            return _context.Paymantfs.Any(e => e.Id == id);
        }
    }
}
