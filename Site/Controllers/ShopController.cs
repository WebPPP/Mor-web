using Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Site.BankService1;

namespace Site.Controllers
{
    [SessionAuthorize]
    public class ShopController : Controller
    {
        private ShoppingCart cart;
        private mordbEntities db = new mordbEntities();
        private Bank1SoapClient bank = new Bank1SoapClient();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var items = db.Items.SqlQuery("SELECT * FROM dbo.Items").ToList();
            return View(items.ToList());
        }

        [AllowAnonymous]
        public ActionResult Browse()
        {
            var items = db.Items.SqlQuery("SELECT * FROM dbo.Items").ToList();
            return View(items.ToList());
        }

        [Route("Cart")]
        public ActionResult ViewCart()
        {
            cart = Session["Cart"] as ShoppingCart;
            List<OrderLine> lines = cart.orderLines;
            if (lines != null)
            {
                return View(lines.ToList());
            }
            else
                return RedirectToAction("Index", "Shop");
        }

        [Route("Orders")]
        public ActionResult ViewOrders()
        {
            User user = Session["User"] as User;
            List<Order> orders = new List<Order>();
            foreach (var order in db.Orders)
            {
                if (order.UserID == user.UserID)
                    orders.Add(order);
            }
            return View(orders.ToList());
        }

        public ActionResult OrderDetails(int id)
        {
            ViewBag.OrderID = id;
            List<OrderLine> lines = new List<OrderLine>();
            foreach (var line in db.OrderLines)
            {
                if (line.OrderID == id)
                {
                    line.Item = db.Items.Find(line.ItemID);
                    lines.Add(line);
                }
            }
            return View(lines.ToList());
        }

        public ActionResult AddToCart(int id)
        {
            cart = Session["Cart"] as ShoppingCart;
            try
            {
                Item item = db.Items.SqlQuery("SELECT * FROM dbo.Items WHERE ItemID = '" + id + "'").FirstOrDefault<Item>();
                cart.addToCart(item, 1);
                Session["Cart"] = cart as ShoppingCart;
            }
            catch (NullReferenceException e)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = e.Message });
            }

            return RedirectToAction("Index", "Shop");
        }

        public ActionResult RemoveItem(int id)
        {
            cart = Session["Cart"] as ShoppingCart;
            cart.RemoveLine(id);
            return RedirectToAction("ViewCart", "Shop");
        }

        [AdminAuthorize]
        public ActionResult AdminOrders(int id)
        {
            List<Order> orders = new List<Order>();
            foreach (var order in db.Orders)
            {
                if (order.UserID == id)
                    orders.Add(order);
            }
            ViewBag.userName = db.Users.Find(id).UserName;
            return View(orders.ToList());
        }
        [AdminAuthorize]
        public ActionResult AdminDeleteOrder(int id)
        {
            db.Orders.Remove(db.Orders.Find(id));
            foreach (var line in db.OrderLines)
            {
                if (line.OrderID == id)
                {
                    db.OrderLines.Remove(line);
                }
            }
            db.SaveChanges();
            return RedirectToAction("AdminOrders", "Shop", new { id = Convert.ToInt32(Session["ViewedUser"]) });
        }

        [HttpGet, Route("Pay")]
        public ActionResult Payment()
        {
            return PartialView();
        }

        [HttpPost, Route("Pay")]
        public ActionResult FinishPayment()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            User user = Session["User"] as User;
            if (cart != null && cart.totalCash > 0)
            {
                bank.ChargeAccount(user.UserID, cart.totalCash);
                cart.sendOrder();
                Session["Cart"] = new ShoppingCart(user);
                return RedirectToAction("Index", "Shop");
            }
            else
                return RedirectToAction("Index", "Shop");
        }
    }
}