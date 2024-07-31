using BookFixx.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookFixx.Controllers
{   [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        private DataBaseLayer d = new DataBaseLayer();

        public ActionResult Index()
        {
            var users = d.Users.ToList();
            var books = d.Books.ToList();

            // Toplam kullanıcı ve toplam kitap sayısı
            ViewBag.TotalUsers = users.Count;
            ViewBag.TotalBooks = books.Count;

            var model = new Tuple<IEnumerable<User>, IEnumerable<Book>>(users, books);
            return View(model);
        }
    }
}