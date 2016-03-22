using MyMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMusic.Controllers
{
    public class CheckoutController : Controller
    {

        MvcMusicStoreEntities db = new MvcMusicStoreEntities();
        // GET: Checkout


        [Authorize]
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddressAndPayment([Bind(Include = "FirstName,LastName,Address,City,State,PostalCode,Phone,Email,AccountNumber,SecurityCode")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Username = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                //Save Order
                db.Orders.Add(order);
                db.SaveChanges();

            }
            //Process the order
            var cart = MusicCart.GetMusicCart(this.HttpContext);
            cart.CreateOrder(order);

            return RedirectToAction("CheckoutComplete", new { id = order.OrderId });

        }

        // GET: /Checkout/Complete
        public ActionResult CheckoutComplete(int id)
        {
            // Validate customer owns this order
            bool isValid = db.Orders.Any(
            o => o.OrderId == id &&
            o.Username == User.Identity.Name);
            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }


    }
}