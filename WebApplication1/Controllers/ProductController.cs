using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        OrderCrude Orders = new OrderCrude();
        ProductCrud product = new ProductCrud();
        // GET: Product
        public ActionResult Shop()
        {
            ViewBag.Products = product.findAll();
            return View();
        }
        [HttpPost]
        public ActionResult AddToCart(ObjectId ProductId )
        {
            return View();
        }


    }
}