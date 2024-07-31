using BookFixx.database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookFixx.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        // GET: User
        DataBaseLayer db = new DataBaseLayer();
        public ActionResult Index()
        {
            
            return View(db.Users.ToList());
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.UserID);
                if (user == null)
                {
                    return HttpNotFound();
                }

                // Update user details
                user.Username = model.Username;
                user.TC = model.TC;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Password = model.Password;
                user.Email = model.Email;
                user.phoneNumber = model.phoneNumber;
                user.Role = model.Role;

                // Save changes to the database
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If we got this far, something went wrong, redisplay form
            return View(model);
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
