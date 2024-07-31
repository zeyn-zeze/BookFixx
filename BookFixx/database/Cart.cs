using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BookFixx.database
{
    [Table("Cart")]
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartID { get; set; }
        public int UserID { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        // Adds a book to the cart
        public void AddBook(DataBaseLayer db, int bookId)
        {
            var book = db.Books.Find(bookId);
            if (book == null)
                throw new Exception("Book not found.");

            var cartItem = db.CartItems.FirstOrDefault(ci => ci.BookID == bookId && ci.CartID == this.CartID);

            if (cartItem != null)
            {
                cartItem.Quantity++;
                db.Entry(cartItem).State = EntityState.Modified;
            }
            else
            {
                cartItem = new CartItem
                {
                    CartID = this.CartID,
                    BookID = bookId,
                    Quantity = 1
                };
                db.CartItems.Add(cartItem);
            }

            db.SaveChanges();
        }

        // Removes a book from the cart
        public void RemoveBook(DataBaseLayer db, int bookId)
        {
            var cartItem = db.CartItems.FirstOrDefault(ci => ci.BookID == bookId && ci.CartID == this.CartID);

            if (cartItem != null)
            {
                db.CartItems.Remove(cartItem);
                db.SaveChanges();
            }
        }

        // Updates the quantity of a cart item
        public void UpdateQuantity(DataBaseLayer db, int bookId, int quantity)
        {
            var cartItem = db.CartItems.FirstOrDefault(ci => ci.BookID == bookId && ci.CartID == this.CartID);

            if (cartItem != null)
            {
                if (quantity > 0)
                {
                    cartItem.Quantity = quantity;
                    db.Entry(cartItem).State = EntityState.Modified;
                }
                else
                {
                    db.CartItems.Remove(cartItem);
                }
                db.SaveChanges();
            }
        }
    }
}
