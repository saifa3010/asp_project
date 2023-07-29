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
    public class ReportsController : Controller
    {
        private readonly ModelContext _context;

        public ReportsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Reports.Include(r => r.Category).Include(r => r.Order).Include(r => r.Product).Include(r => r.Useracc);
            return View(await modelContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Category)
                .Include(r => r.Order)
                .Include(r => r.Product)
                .Include(r => r.Useracc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "Id");
            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id");
            ViewData["UseraccId"] = new SelectList(_context.Useraccfs, "Id", "Id");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UseraccId,OrderId,ProductId,CategoryId")] Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "Id", report.CategoryId);
            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id", report.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id", report.ProductId);
            ViewData["UseraccId"] = new SelectList(_context.Useraccfs, "Id", "Id", report.UseraccId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "Id", report.CategoryId);
            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id", report.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id", report.ProductId);
            ViewData["UseraccId"] = new SelectList(_context.Useraccfs, "Id", "Id", report.UseraccId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,UseraccId,OrderId,ProductId,CategoryId")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "Id", report.CategoryId);
            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id", report.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id", report.ProductId);
            ViewData["UseraccId"] = new SelectList(_context.Useraccfs, "Id", "Id", report.UseraccId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Category)
                .Include(r => r.Order)
                .Include(r => r.Product)
                .Include(r => r.Useracc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(decimal id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }



        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }
        public IActionResult Report()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelData =  _context.Reports.Include(r => r.Category).Include(r => r.Order).Include(r => r.Product).Include(r => r.Useracc);


            return View(modelData);




        }
    }
}