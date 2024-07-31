using BookFixx.database;
using System.Linq;
using System.Web.Mvc;

namespace BookFixx.Controllers
{
    public class BaseController : Controller
    {
        private readonly DataBaseLayer db = new DataBaseLayer();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user != null)
            {
                var cart = db.Carts.FirstOrDefault(c => c.UserID == user.UserID);
                if (cart != null)
                {
                    ViewBag.BasketCount = cart.CartItems.Sum(ci => ci.Quantity);
                }
                else
                {
                    ViewBag.BasketCount = 0;
                }
            }
            else
            {
                ViewBag.BasketCount = 0;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
