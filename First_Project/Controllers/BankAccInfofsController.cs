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
    public class BankAccInfofsController : Controller
    {
        private readonly ModelContext _context;

        public BankAccInfofsController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: BankAccInfofs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelContext = _context.BankAccInfofs.Include(b => b.Acc);
            return View(await modelContext.ToListAsync());
        }

        // GET: BankAccInfofs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var bankAccInfof = await _context.BankAccInfofs
                .Include(b => b.Acc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccInfof == null)
            {
                return NotFound();
            }

            return View(bankAccInfof);
        }

        // GET: BankAccInfofs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Id");
            return View();
        }

        // POST: BankAccInfofs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cvv,CardNumber,ExpireDate,AccId,Balance")] BankAccInfof bankAccInfof)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (ModelState.IsValid)
            {
                _context.Add(bankAccInfof);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Id", bankAccInfof.AccId);
            return View(bankAccInfof);
        }

        // GET: BankAccInfofs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var bankAccInfof = await _context.BankAccInfofs.FindAsync(id);
            if (bankAccInfof == null)
            {
                return NotFound();
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Id", bankAccInfof.AccId);
            return View(bankAccInfof);
        }

        // POST: BankAccInfofs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Cvv,CardNumber,ExpireDate,AccId,Balance")] BankAccInfof bankAccInfof)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id != bankAccInfof.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bankAccInfof);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankAccInfofExists(bankAccInfof.Id))
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
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Id", bankAccInfof.AccId);
            return View(bankAccInfof);
        }

        // GET: BankAccInfofs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var bankAccInfof = await _context.BankAccInfofs
                .Include(b => b.Acc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bankAccInfof == null)
            {
                return NotFound();
            }

            return View(bankAccInfof);
        }

        // POST: BankAccInfofs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var bankAccInfof = await _context.BankAccInfofs.FindAsync(id);
            _context.BankAccInfofs.Remove(bankAccInfof);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankAccInfofExists(decimal id)
        {
            return _context.BankAccInfofs.Any(e => e.Id == id);
        }
    }
}
