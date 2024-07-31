using BookFixx.database;
using BookFixx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookFixx.Controllers
{
    
    public class AccountController : Controller
    {
        private DataBaseLayer db = new DataBaseLayer();

       
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    TC = model.TC,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password, 
                    Email = model.Email,
                    phoneNumber = model.PhoneNumber,
                    Role = "üye" 
                };

                db.Users.Add(user);
                db.SaveChanges();

                return RedirectToAction("Login", "Login");
            }

            return View(model);
        }
    }

}