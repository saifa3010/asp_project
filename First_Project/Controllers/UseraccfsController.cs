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
    public class UseraccfsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UseraccfsController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Useraccfs
        public async Task<IActionResult> Index()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View(await _context.Useraccfs.ToListAsync());
        }

        // GET: Useraccfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var useraccf = await _context.Useraccfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useraccf == null)
            {
                return NotFound();
            }

            return View(useraccf);
        }

        // GET: Useraccfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }

        // POST: Useraccfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,Imagepath,Phonenumber,ImageFile")] Useraccf useraccf)
        {
            if (ModelState.IsValid)
            {
                if (useraccf.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;


                    string imageName = Guid.NewGuid().ToString() + "_" + useraccf.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/image/", imageName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await useraccf.ImageFile.CopyToAsync(fileStream);
                    }
                    useraccf.Imagepath = imageName;
                }



                _context.Add(useraccf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(useraccf);
        }

        // GET: Useraccfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var useraccf = await _context.Useraccfs.FindAsync(id);
            if (useraccf == null)
            {
                return NotFound();
            }
            return View(useraccf);
        }

        // POST: Useraccfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fname,Lname,Imagepath,Phonenumber,ImageFile")] Useraccf useraccf)
        {
            if (id != useraccf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (useraccf.ImageFile != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" +
                        useraccf.ImageFile.FileName;
                        string extension =
                        Path.GetExtension(useraccf.ImageFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/image/",
                        fileName);
                        using (var fileStream = new FileStream(path,
                        FileMode.Create))

                        {
                            await useraccf.ImageFile.CopyToAsync(fileStream);
                        }
                        useraccf.Imagepath = fileName;

                    }

                    _context.Update(useraccf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccfExists(useraccf.Id))
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
            return View(useraccf);
        }

        // GET: Useraccfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var useraccf = await _context.Useraccfs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (useraccf == null)
            {
                return NotFound();
            }

            return View(useraccf);
        }

        // POST: Useraccfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var useraccf = await _context.Useraccfs.FindAsync(id);
            _context.Useraccfs.Remove(useraccf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseraccfExists(decimal id)
        {
            return _context.Useraccfs.Any(e => e.Id == id);
        }
    }
}
