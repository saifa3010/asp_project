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
    public class ProductOrderfsController : Controller
    {
        private readonly ModelContext _context;

        public ProductOrderfsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: ProductOrderfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelContext = _context.ProductOrderves.Include(p => p.Order).Include(p => p.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductOrderfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var productOrderf = await _context.ProductOrderves
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOrderf == null)
            {
                return NotFound();
            }

            return View(productOrderf);
        }

        // GET: ProductOrderfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id");
            return View();
        }

        // POST: ProductOrderfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status,ProductId,OrderId,Quantity")] ProductOrderf productOrderf)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (ModelState.IsValid)
            {
                _context.Add(productOrderf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id", productOrderf.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id", productOrderf.ProductId);
            return View(productOrderf);
        }

        // GET: ProductOrderfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var productOrderf = await _context.ProductOrderves.FindAsync(id);
            if (productOrderf == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id", productOrderf.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id", productOrderf.ProductId);
            return View(productOrderf);
        }

        // POST: ProductOrderfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Status,ProductId,OrderId,Quantity")] ProductOrderf productOrderf)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id != productOrderf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productOrderf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductOrderfExists(productOrderf.Id))
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
            ViewData["OrderId"] = new SelectList(_context.Orderves, "Id", "Id", productOrderf.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Productfs, "Id", "Id", productOrderf.ProductId);
            return View(productOrderf);
        }

        // GET: ProductOrderfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var productOrderf = await _context.ProductOrderves
                .Include(p => p.Order)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOrderf == null)
            {
                return NotFound();
            }

            return View(productOrderf);
        }

        // POST: ProductOrderfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var productOrderf = await _context.ProductOrderves.FindAsync(id);
            _context.ProductOrderves.Remove(productOrderf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductOrderfExists(decimal id)
        {
            return _context.ProductOrderves.Any(e => e.Id == id);
        }
    }
}
