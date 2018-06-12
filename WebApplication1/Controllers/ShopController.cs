using DabisBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Dabis;
using WebApplication1.Models;

namespace Site.Controllers
{
    [SessionAuthorize]
    public class ShopController : Controller
    {
        OrderCrude Orders = new OrderCrude();
        ProductCrud product = new ProductCrud();
        private ShoppingCart cart;
        OrderCrude db;
        private Bank1SoapClient bank = new Bank1SoapClient();

        [AllowAnonymous]
        public ActionResult Shop()
        {
            ViewBag.Products = product.findAll();
            db = new OrderCrude();
            return View();
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
            foreach (var order in db.findAll())
            {
                if (order.UserId== user.id)
                    orders.Add(order);
            }
            return View(orders.ToList());
        }

        public ActionResult OrderDetails(string id)
        {
            ViewBag.OrderID = id;
            Order OrdeVar = Orders.find(id);
            List<OrderLine> lines = new List<OrderLine>();
            foreach (var line in OrdeVar.Lines)
            {
                if (line.OrderId.ToString() == id)
                {
                    line.Product = product.find(line.ProductId.ToString());
                    lines.Add(line);
                }
            }
            return View(lines.ToList());
        }

        public ActionResult AddToCart(string id)
        {
            cart = Session["Cart"] as ShoppingCart;
            try
            {
                Product item =product.find(id);
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
                bank.ChargeAccount(user.id.ToString(), cart.totalCash);
                cart.sendOrder();
                Session["Cart"] = new ShoppingCart(user);
                return RedirectToAction("Index", "Shop");
            }
            else
                return RedirectToAction("Index", "Shop");
        }
    }
}