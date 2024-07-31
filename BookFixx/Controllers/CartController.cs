using BookFixx.database;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;


namespace BookFixx.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly DataBaseLayer db = new DataBaseLayer();

        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return HttpNotFound();
            }

            var cart = db.Carts.Include(c => c.CartItems.Select(x => x.Book)).FirstOrDefault(ci => ci.UserID == user.UserID);

            if (cart == null)
            {
                ViewBag.BasketCount = 0; 
                return View(new List<CartItem>());
            }

            
            ViewBag.BasketCount = cart.CartItems.Sum(ci => ci.Quantity);

            return View(cart.CartItems);
        }


        public ActionResult AddToCart(int bookId)
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return HttpNotFound();
            }

            var cart = db.Carts.FirstOrDefault(c => c.UserID == user.UserID);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserID = user.UserID,
                    CreatedAt = DateTime.Now
                };
                db.Carts.Add(cart);
                db.SaveChanges();
            }

            cart.AddBook(db, bookId);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveCart(int bookId)
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return HttpNotFound();
            }

            var cart = db.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.UserID == user.UserID);
            if (cart == null)
            {
                return HttpNotFound();
            }

            cart.RemoveBook(db, bookId);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult QuantityProcess(int bookId, int quantity)
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return HttpNotFound();
            }

            var cart = db.Carts.FirstOrDefault(c => c.UserID == user.UserID);
            if (cart == null)
            {
                return HttpNotFound();
            }

            cart.UpdateQuantity(db, bookId, quantity);
            return RedirectToAction("Index", "Cart");
        }
    }
}
