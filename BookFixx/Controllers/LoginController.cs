using BookFixx.database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BookFixx.Controllers
{
    
    public class LoginController : Controller
    {
        DataBaseLayer d = new DataBaseLayer();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            var info = d.Users.FirstOrDefault(x => x.Username == u.Username && x.Password == u.Password);
            if (info != null)
            {    
                FormsAuthentication.SetAuthCookie(u.Username, false);

                if (info.Role == "admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            
            ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}
