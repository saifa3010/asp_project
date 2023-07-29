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
    public class HomefsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomefsController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {

            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Homefs
        public async Task<IActionResult> Index()
        {

            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");



            return View(await _context.Homefs.ToListAsync());
        }



        // GET: Homefs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var homef = await _context.Homefs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homef == null)
            {
                return NotFound();
            }

            return View(homef);
        }

        // GET: Homefs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }

        // POST: Homefs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imagepath1,Text1,Text2,Email,Phonenumber,ImageFile")] Homef homef)
        {


            if (ModelState.IsValid)
            {

                if (homef.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;


                    string imageName = Guid.NewGuid().ToString() + "_" + homef.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/image/", imageName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await homef.ImageFile.CopyToAsync(fileStream);
                    }
                    homef.Imagepath1 = imageName;
                }




                _context.Add(homef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homef);
        }

        // GET: Homefs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var homef = await _context.Homefs.FindAsync(id);
            if (homef == null)
            {
                return NotFound();
            }
            return View(homef);
        }

        // POST: Homefs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Imagepath1,Text1,Text2,Email,Phonenumber,ImageFile")] Homef homef)
        {
            


            if (id != homef.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (homef.ImageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" +
                        homef.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(homef.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/Image/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))

                        {
                          await homef.ImageFile.CopyToAsync(fileStream);
                        }
                        homef.Imagepath1 = fileName;

                    }

                    _context.Update(homef);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomefExists(homef.Id))
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
            return View(homef);
        }

        // GET: Homefs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var homef = await _context.Homefs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homef == null)
            {
                return NotFound();
            }

            return View(homef);
        }

        // POST: Homefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var homef = await _context.Homefs.FindAsync(id);
            _context.Homefs.Remove(homef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomefExists(decimal id)
        {
            return _context.Homefs.Any(e => e.Id == id);
        }
    }
}
