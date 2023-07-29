using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace First_Project.Controllers
{
	public class StoreHomeController : Controller
	{
		public IActionResult Store()
		{

            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
		}
        public async Task<IActionResult> Logout()

        {

            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login", "RegisterLogin");


        }
    }
}
