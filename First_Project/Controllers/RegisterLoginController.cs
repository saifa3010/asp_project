using First_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace First_Project.Controllers
{
    public class RegisterLoginController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RegisterLoginController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;

        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Fname,Lname,Phonenumber,Imagepath,ImageFile")] Useraccf useraccf, string Email, string Password)
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

                _context.Add(useraccf);
                await _context.SaveChangesAsync();
                //user login
                Userloginf user = new Userloginf(); //create user login object
                user.Email = Email;
                user.Password = Password;
                user.RoleId = 2;
                user.AccId = useraccf.Id;

                _context.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Register));
            }
            return View(useraccf);
        }



        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] Userloginf userlogin)
        {
            var auth = _context.Userloginfs.Where(x => x.Email == userlogin.Email && x.Password == userlogin.Password).FirstOrDefault();


            //ViewBag.Login = 0;

            if (auth != null)
            {
                switch (auth.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AccId", (int)auth.AccId);

                        HttpContext.Session.SetString("Admin", userlogin.Email);

                        return RedirectToAction("Index", "Home");

                    case 2:
                        HttpContext.Session.SetInt32("AccId", (int)auth.AccId);

                        HttpContext.Session.SetInt32("Login", 1);

                        return RedirectToAction("Store", "Interface");



                }


            }
            return View();
        }

    }
}
