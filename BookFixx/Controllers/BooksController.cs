using BookFixx.database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace BookFixx.Controllers
{
    [Authorize(Roles = "admin")]
    public class BooksController : Controller
    {
        private DataBaseLayer db = new DataBaseLayer();

        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Category).ToList();
            return View(books);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book model, HttpPostedFileBase ImageData)
        {
            if (ModelState.IsValid)
            {
                if (ImageData != null && ImageData.ContentLength > 0)
                {
                    using (var binaryRead = new BinaryReader(ImageData.InputStream))
                    {
                        // Read the image data as a byte array
                        byte[] imageData = binaryRead.ReadBytes(ImageData.ContentLength);

                        // Convert the byte array to a Base64 string
                        model.ImageData = Convert.ToBase64String(imageData);
                    }
                }

                db.Books.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index","Books");
            }

            ViewBag.Categories = new SelectList(db.Categories, "CategoryID", "CategoryName", model.CategoryID);
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var categories = db.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "CategoryID", "CategoryName", book.CategoryID);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book model, HttpPostedFileBase ImageData)
        {
            if (ModelState.IsValid)
            {
                var book = db.Books.Find(model.BookID);
                if (book == null)
                {
                    return HttpNotFound();
                }

                // Update book details
                book.Title = model.Title;
                book.Author = model.Author;
                book.Publisher = model.Publisher;
                book.Description = model.Description;
                book.Stock = model.Stock;
                book.Price = model.Price;
                book.CategoryID = model.CategoryID;

                // Handle image upload if a new file is uploaded
                if (ImageData != null && ImageData.ContentLength > 0)
                {
                    using (var binaryRead = new BinaryReader(ImageData.InputStream))
                    {
                        byte[] imageData = binaryRead.ReadBytes(ImageData.ContentLength);
                        book.ImageData = Convert.ToBase64String(imageData);
                    }
                }

                // Save changes to the database
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            // If we got this far, something went wrong, redisplay form
            ViewBag.Categories = new SelectList(db.Categories, "CategoryID", "CategoryName", model.CategoryID);
            return View(model);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
