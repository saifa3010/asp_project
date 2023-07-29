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
    public class ContactfsController : Controller
    {
        private readonly ModelContext _context;

        public ContactfsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Contactfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View(await _context.Contactfs.ToListAsync());
        }

        // GET: Contactfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var contactf = await _context.Contactfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactf == null)
            {
                return NotFound();
            }

            return View(contactf);
        }

        // GET: Contactfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }

        // POST: Contactfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text1,Location,Email,Phonenumber")] Contactf contactf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactf);
        }

        // GET: Contactfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var contactf = await _context.Contactfs.FindAsync(id);
            if (contactf == null)
            {
                return NotFound();
            }
            return View(contactf);
        }

        // POST: Contactfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Text1,Location,Email,Phonenumber")] Contactf contactf)
        {
            if (id != contactf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactfExists(contactf.Id))
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
            return View(contactf);
        }

        // GET: Contactfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var contactf = await _context.Contactfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactf == null)
            {
                return NotFound();
            }

            return View(contactf);
        }

        // POST: Contactfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactf = await _context.Contactfs.FindAsync(id);
            _context.Contactfs.Remove(contactf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactfExists(decimal id)
        {
            return _context.Contactfs.Any(e => e.Id == id);
        }
    }
}
