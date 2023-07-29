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
    public class UserloginfsController : Controller
    {
        private readonly ModelContext _context;

        public UserloginfsController(ModelContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }

        // GET: Userloginfs
        public async Task<IActionResult> Index()
        {
            

            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var modelContext = _context.Userloginfs.Include(u => u.Acc).Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Userloginfs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var userloginf = await _context.Userloginfs
                .Include(u => u.Acc)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userloginf == null)
            {
                return NotFound();
            }

            return View(userloginf);
        }

        // GET: Userloginfs/Create
        public IActionResult Create()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname");
            ViewData["RoleId"] = new SelectList(_context.Rolefs, "Id", "Rolename");
            return View();
        }

        // POST: Userloginfs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,RoleId,AccId")] Userloginf userloginf)
        {
            HttpContext.Session.SetString("user", userloginf.Email);

            if (ModelState.IsValid)
            {
                _context.Add(userloginf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", userloginf.AccId);
            ViewData["RoleId"] = new SelectList(_context.Rolefs, "Id", "Rolename", userloginf.RoleId);
            return View(userloginf);
        }

        // GET: Userloginfs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var userloginf = await _context.Userloginfs.FindAsync(id);
            if (userloginf == null)
            {
                return NotFound();
            }
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", userloginf.AccId);
            ViewData["RoleId"] = new SelectList(_context.Rolefs, "Id", "Rolename", userloginf.RoleId);
            return View(userloginf);
        }

        // POST: Userloginfs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Email,Password,RoleId,AccId")] Userloginf userloginf)
        {
            if (id != userloginf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userloginf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserloginfExists(userloginf.Id))
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
            ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", userloginf.AccId);
            ViewData["RoleId"] = new SelectList(_context.Rolefs, "Id", "Rolename", userloginf.RoleId);
            return View(userloginf);
        }

        // GET: Userloginfs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var userloginf = await _context.Userloginfs
                .Include(u => u.Acc)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userloginf == null)
            {
                return NotFound();
            }

            return View(userloginf);
        }

        // POST: Userloginfs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userloginf = await _context.Userloginfs.FindAsync(id);
            _context.Userloginfs.Remove(userloginf);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserloginfExists(decimal id)
        {
            return _context.Userloginfs.Any(e => e.Id == id);
        }
    }
}
