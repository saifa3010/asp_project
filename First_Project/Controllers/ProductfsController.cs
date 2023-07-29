using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using First_Project.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace First_Project.Controllers
{
    public class ProductfsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductfsController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;

        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }
        // GET: Productfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelContext = _context.Productfs.Include(p => p.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: Productfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var productf = await _context.Productfs
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productf == null)
            {
                return NotFound();
            }

            return View(productf);
        }

        // GET: Productfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "CategoryName");
            return View();
        }

        // POST: Productfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductfName,Imagepath,Price,Sale,Description,CategoryId,ImageFile")] Productf productf)
        {
            if (ModelState.IsValid)
            {
                if (productf.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;


                    string imageName = Guid.NewGuid().ToString() + "_" + productf.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/image/", imageName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await productf.ImageFile.CopyToAsync(fileStream);
                    }
                    productf.Imagepath = imageName;
                }

                _context.Add(productf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "CategoryName", productf.CategoryId);
            return View(productf);
        }

        // GET: Productfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var productf = await _context.Productfs.FindAsync(id);
            if (productf == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "CategoryName", productf.CategoryId);
            return View(productf);
        }

        // POST: Productfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ProductfName,Imagepath,Price,Sale,Description,CategoryId,ImageFile")] Productf productf)
        {
            if (id != productf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (productf.ImageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" +
                        productf.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(productf.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/image/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))

                        {
                            await productf.ImageFile.CopyToAsync(fileStream);
                        }
                        productf.Imagepath = fileName;

                    }
                    _context.Update(productf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductfExists(productf.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categoryfs, "Id", "CategoryName", productf.CategoryId);
            return View(productf);
        }

        // GET: Productfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var productf = await _context.Productfs
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productf == null)
            {
                return NotFound();
            }

            return View(productf);
        }

        // POST: Productfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productf = await _context.Productfs.FindAsync(id);
            _context.Productfs.Remove(productf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductfExists(decimal id)
        {
            return _context.Productfs.Any(e => e.Id == id);
        }
    }
}
