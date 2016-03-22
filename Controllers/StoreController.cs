using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMusic.Models;

namespace MyMusic.Controllers
{
    public class StoreController : Controller
    {
        private MvcMusicStoreEntities db = new MvcMusicStoreEntities();

        public ActionResult Index()
        {
            var genres = db.Genres.ToList();
            return View(genres);

        }

    // List all the albuns by genre    
        public ActionResult BrowseAlbuns( int id)
        {   
            var albumsModel = (from a in db.Albums
                               where a.GenreId == id
                               select a).ToList();

            return View(albumsModel);
        }

        [HttpPost]
        public ActionResult MusicSearch(string genre, string musicSearchBox) 
        
        {
            var musicSearch = (from a in db.Albums
                               where a.Genre.Name == genre && (a.Title.Contains(musicSearchBox) || a.Artist.Name.Contains(musicSearchBox))
                                 select a).ToList();

            return View("BrowseAlbuns", musicSearch);
        }

       



    }
}