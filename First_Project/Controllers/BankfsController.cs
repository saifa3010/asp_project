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
    public class BankfsController : Controller
    {
        private readonly ModelContext _context;

        public BankfsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Bankfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View(await _context.Bankfs.ToListAsync());
        }

        // GET: Bankfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

                return NotFound();
            }

            var bankf = await _context.Bankfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankf == null)
            {
                return NotFound();
            }

            return View(bankf);
        }

        // GET: Bankfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }

        // POST: Bankfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpireDate,CardNumber,Cvv,Balance")] Bankf bankf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bankf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankf);
        }

        // GET: Bankfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var bankf = await _context.Bankfs.FindAsync(id);
            if (bankf == null)
            {
                return NotFound();
            }
            return View(bankf);
        }

        // POST: Bankfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ExpireDate,CardNumber,Cvv,Balance")] Bankf bankf)
        {
            if (id != bankf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankfExists(bankf.Id))
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
            return View(bankf);
        }

        // GET: Bankfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var bankf = await _context.Bankfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankf == null)
            {
                return NotFound();
            }

            return View(bankf);
        }

        // POST: Bankfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var bankf = await _context.Bankfs.FindAsync(id);
            _context.Bankfs.Remove(bankf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankfExists(decimal id)
        {
            return _context.Bankfs.Any(e => e.Id == id);
        }
    }
}
