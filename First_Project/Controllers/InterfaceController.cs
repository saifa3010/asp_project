using First_Project.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;

namespace First_Project.Controllers
{
    public class InterfaceController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public InterfaceController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;

        }
        public IActionResult Store()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            //Home
            var HomePage = from item in _context.Homefs select item;
            ViewBag.HomePage = HomePage;

            var pro = _context.Productfs.Where(x => x.CategoryId == 41).ToList();
            ViewBag.Pro = pro;
            return View();
        }


        public IActionResult Shop()
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var shop = _context.Productfs.ToList();
            ViewBag.Shop = shop;
            return View();
        }






        //[HttpGet]
        //public async Task<IActionResult> Tastimoniels(decimal? id)
        //{
        //    ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

        //    var User = _context.Useraccfs.SingleOrDefault(x => x.Id == id);
        //    ViewBag.user = User;

        //    //ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Id");
        //    return View();
        //    }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Tastimoniels(decimal? id, [Bind("Id,Message,AccId")] Tastimoniel tastimoniel)
        //{
        //   ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

        //    var User = _context.Useraccfs.SingleOrDefault(x => x.Id == id);
        //    ViewBag.user = User;

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(tastimoniel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Store));
        //        //return RedirectToAction("UpdateOpinion", "TestimonialPages", new { Id = testimonialPage.Id });

        //    }
        //    ViewData["AccId"] = new SelectList(_context.Useraccfs, "Id", "Fname", tastimoniel.AccId);
        //    return View(tastimoniel);


        //}

        public IActionResult Cart(int id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            var productOrder = _context.ProductOrderves.Include(x => x.Product).ToList();
            var order = _context.Orderves.Where(x => x.AccId == id).Include(x => x.Acc).ToList();

            var model = from o in order
                        join po in productOrder on o.Id equals po.OrderId
                        select new joinCart { productOrder = po, order = o };
            ViewBag.total = model.Where(x => x.productOrder.Status == "0" && x.order.AccId == id).Sum(x => x.productOrder.Product.Price);
            return View(model);
        }

        public IActionResult AddToCart(int id)
        {

            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");
            Orderf order = new Orderf();
            order.AccId = ViewBag.AccId;
            order.DateOrder = DateTime.Now;
            _context.Add(order);
            _context.SaveChanges();


            ProductOrderf product_order = new ProductOrderf();
            product_order.OrderId = order.Id;
            product_order.ProductId = id;
            //product_order.Quantity = 1;
            product_order.Status = "0";


            _context.Add(product_order);
            _context.SaveChanges();


            return RedirectToAction("Shop", "Interface");

        }



        public async Task<IActionResult> Delete(decimal? id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            if (id == null)
            {
                return NotFound();
            }

            var prodOr = await _context.ProductOrderves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodOr == null)
            {
                return NotFound();
            }

            return View(prodOr);
        }




        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCart(decimal id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");




            var prodOr = await _context.ProductOrderves.FindAsync(id);
            _context.ProductOrderves.Remove(prodOr);
            await _context.SaveChangesAsync();
            return RedirectToAction("Cart", "Interface", new { id = ViewBag.AccId });
        }

        public async Task<IActionResult> mypaymant(int id)
        {
            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");
            var productOrder = _context.ProductOrderves.Include(x => x.Product).Include(o => o.Order).Where(x => x.Status == "1" && x.Order.Acc.Id == id);
            return View(productOrder);
        }


        public IActionResult Checkout()
        {

            ViewBag.AccId = HttpContext.Session.GetInt32("AccId");

            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Checkout([Bind("Id,CardNumber,Cvv,Balance")] Bankf bank)
        //{


        //    ViewBag.AccId = HttpContext.Session.GetInt32("AccId");




        //    var checkcard = _context.Bankfs.Where(x => x.CardNumber == bank.CardNumber && x.Cvv == bank.Cvv).FirstOrDefault();
        //    var amount = _context.ProductOrderves.Where(x => x.Order.AccId == HttpContext.Session.GetInt32("id") && x.Status == "0")
        //        .Sum(x => x.Product.Price);


        //    if (checkcard != null)
        //    {
        //        if (amount == 0)
        //        {
        //            ViewBag.NoProductsInCart = true;
        //        }
        //        else if (checkcard.Balance >= amount)
        //        {

        //            var product = _context.ProductOrderves.Include(x => x.Order).Include(x => x.Product).Where(x => x.OrderId == x.Order.Id).ToList();

        //            Paymantf payment = new Paymantf();
        //            payment.Balance = amount;
        //            payment.PayDate = DateTime.Now;
        //            payment.PaymantId = HttpContext.Session.GetInt32("id");

        //            _context.Add(payment);
        //            _context.SaveChanges();

        //            foreach (var item in product)
        //            {

        //                item.Status = "1";
        //                _context.Update(item);
        //                _context.SaveChanges();

        //            }

        //        }

    }
}



