using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using First_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace First_Project.Controllers
{
    public class AboutfsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;


        public AboutfsController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Aboutfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View(await _context.Aboutfs.ToListAsync());
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Aboutfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var aboutf = await _context.Aboutfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutf == null)
            {
                return NotFound();
            }

            return View(aboutf);
        }

        // GET: Aboutfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }

        // POST: Aboutfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imagepath,Team,Text1,Info1,Info2,ImageFile")] Aboutf aboutf)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (ModelState.IsValid)
            {

                if (aboutf.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;


                    string imageName = Guid.NewGuid().ToString() + "_" + aboutf.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/image/", imageName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await aboutf.ImageFile.CopyToAsync(fileStream);
                    }
                    aboutf.Imagepath = imageName;
                }


                _context.Add(aboutf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutf);
        }

        // GET: Aboutfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var aboutf = await _context.Aboutfs.FindAsync(id);
            if (aboutf == null)
            {
                return NotFound();
            }
            return View(aboutf);
        }

        // POST: Aboutfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Imagepath,Team,Text1,Info1,Info2,ImageFile")] Aboutf aboutf)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id != aboutf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutf.ImageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" +
                        aboutf.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(aboutf.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/image/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))

                        {
                            await aboutf.ImageFile.CopyToAsync(fileStream);
                        }
                        aboutf.Imagepath = fileName;

                    }

                    _context.Update(aboutf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutfExists(aboutf.Id))
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
            return View(aboutf);
        }

        // GET: Aboutfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var aboutf = await _context.Aboutfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aboutf == null)
            {
                return NotFound();
            }

            return View(aboutf);
        }

        // POST: Aboutfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var aboutf = await _context.Aboutfs.FindAsync(id);
            _context.Aboutfs.Remove(aboutf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutfExists(decimal id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return _context.Aboutfs.Any(e => e.Id == id);
        }
    }
}
