using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusic.Models;
using MyMusic.ViewModels;
using System.Net;

namespace MyMusic.Controllers
{
    public class MusicCartController : Controller
    {

        MvcMusicStoreEntities db = new MvcMusicStoreEntities();
       
        // GET: /MusicCart/
        public ActionResult Index()

        {   // create the user/guid
            var cart = MusicCart.GetMusicCart(this.HttpContext);

            // Set up ViewModel
            var viewModel = new MusicCartViewModel
            {
                CartAlbums = cart.GetCartAlbums(),
                CartTotal = cart.GetCartTotal()
            };
       
            return View(viewModel);
        }
        
       
        public ActionResult AddMusicCart(int id)
        {
            // Retrieve the album from bd
            var addedAlbum = db.Albums.Single(album => album.AlbumId == id);
            // Add album to the shopping cart  
            var cart = MusicCart.GetMusicCart(this.HttpContext);
            cart.AddMusicCart(addedAlbum);
            // Go back to the main page for more shopping
            return RedirectToAction("Index", "Store");
        }
        //
 
  
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = MusicCart.GetMusicCart(this.HttpContext);
            // Get the name of the album to display confirmation
            string albumName = db.Carts.Single(item => item.RecordId == id).Album.Title;
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);
            // Validation success
            TempData["Success"] = albumName + " " + "has been removed from your shopping cart.";
            return RedirectToAction("Index", "MusicCart");
        }
       

    
        [ChildActionOnly]
        public ActionResult CartReHeader()
        {
            var cart = MusicCart.GetMusicCart(this.HttpContext);
            ViewData["CartCount"] = cart.GetCount();
            return PartialView("_CartReHeader");
        }
    }
}
