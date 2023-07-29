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
    public class CategoryfsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CategoryfsController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Categoryfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View(await _context.Categoryfs.ToListAsync());
        }

        // GET: Categoryfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var categoryf = await _context.Categoryfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryf == null)
            {
                return NotFound();
            }

            return View(categoryf);
        }

        // GET: Categoryfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }

        // POST: Categoryfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,Imagepath", "ImageFile")] Categoryf categoryf)
        {
            if (ModelState.IsValid)
            {

                if (categoryf.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;


                    string imageName = Guid.NewGuid().ToString() + "_" + categoryf.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/image/", imageName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await categoryf.ImageFile.CopyToAsync(fileStream);
                    }
                    categoryf.Imagepath = imageName;
                }


                _context.Add(categoryf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryf);
        }

        // GET: Categoryfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var categoryf = await _context.Categoryfs.FindAsync(id);
            if (categoryf == null)
            {
                return NotFound();
            }
            return View(categoryf);
        }

        // POST: Categoryfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,Imagepath","ImageFile")] Categoryf categoryf)
        {
            if (id != categoryf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (categoryf.ImageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" +
                        categoryf.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(categoryf.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/image/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))

                        {
                            await categoryf.ImageFile.CopyToAsync(fileStream);
                        }
                        categoryf.Imagepath = fileName;

                    }


                    _context.Update(categoryf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryfExists(categoryf.Id))
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
            return View(categoryf);
        }

        // GET: Categoryfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var categoryf = await _context.Categoryfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryf == null)
            {
                return NotFound();
            }

            return View(categoryf);
        }

        // POST: Categoryfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var categoryf = await _context.Categoryfs.FindAsync(id);
            _context.Categoryfs.Remove(categoryf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryfExists(decimal id)
        {
            return _context.Categoryfs.Any(e => e.Id == id);
        }
    }
}
