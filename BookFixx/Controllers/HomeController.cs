using BookFixx.Models;
using BookFixx.database;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace BookFixx.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        DataBaseLayer d = new DataBaseLayer();

        public ActionResult Index()
        {
            var books = d.Books.Include(b => b.Category).ToList();
            ViewBag.IsAdmin = User.IsInRole("Admin");
            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Profile()
        {
            var user = GetUserFromSession();
            return View(user);
        }

        private RegisterModel GetUserFromSession()
        {
            string username = User.Identity.Name;

            var user = d.Users
                .Where(u => u.Username == username)
                .Select(u => new RegisterModel
                {
                    Username = u.Username,
                    TC = u.TC,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.phoneNumber,
                    UserID = u.UserID,
                })
                .FirstOrDefault();

            return user;
        }
    }
}
