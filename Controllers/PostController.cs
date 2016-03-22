using MyMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMusic.Controllers
{
    public class PostController : Controller
    {

        private MvcMusicStoreEntities db = new MvcMusicStoreEntities();
        // GET: Forum
        public ActionResult Index()
        {
            var posts = db.Posts.ToList();
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View(posts);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostGenre(int GenreId)
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", GenreId);

            var postGenre = (from a in db.Posts
                             where a.GenreId == GenreId
                               select a).ToList();

           return View("Index", postGenre.OrderByDescending(s => s.PostDate));
        }

        // ascending order
         [HttpGet]
        public ActionResult LastPost(int GenreId)
        {
            var lastPosts = (from a in db.Posts
                             where a.GenreId == GenreId
                             select a).ToList();

            return View("MyCurrentPost", lastPosts);
        }
        
    }
}