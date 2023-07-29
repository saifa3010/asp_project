using First_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace First_Project.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }


        [HttpGet]
        public async Task<IActionResult> Profile(decimal? id)
        {
            {
                if (id == null)
                {
                    return NotFound();
                }

               

                var log = _context.Userloginfs.FirstOrDefault(x => x.AccId == id);
                var ovr = _context.Useraccfs.FirstOrDefault(x=> x.Id==id);
                ViewBag.Fname= ovr.Fname;
                ViewBag.Lname = ovr.Lname;
                ViewBag.Phonenumber = ovr.Phonenumber;
                ViewBag.Imagepath = ovr.Imagepath;



                var account = await _context.Useraccfs.FindAsync(id);

                Userloginf Uaccount = (from acc in _context.Userloginfs
                                       where acc.AccId.Equals(account.Id)
                                      select acc).FirstOrDefault();

                if (!(Uaccount is null))
                {
                    ViewBag.Email = Uaccount.Email;
                    ViewBag.role = Uaccount.RoleId;
                }


                if (account == null)
                {
                    return NotFound();
                }
                return View(account);
            }
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(decimal? id, [Bind("Id,Phonenumber,Fname,Lname,ImageFile,Imagepath")]
        Useraccf useraccf, Userloginf userLogin,
          string email)
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





            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(useraccf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {


                    throw;

                }
                Userloginf Uaccount = (from acc in _context.Userloginfs
                                       where acc.AccId.Equals(useraccf.Id)
                                      select acc).FirstOrDefault();
                if (Uaccount is null)
                {
                    Uaccount = new Userloginf();
                    Uaccount.AccId = useraccf.Id;
                    Uaccount.Email = email;

                    _context.Add(Uaccount);
                    await _context.SaveChangesAsync();
                }



                else
                {
                    Uaccount.Email = email;

                    _context.Update(Uaccount);
                    await _context.SaveChangesAsync();
                }


                return RedirectToAction("Profile", new { useraccf.Id });
            }
            return View(useraccf);
        }
    }
}
