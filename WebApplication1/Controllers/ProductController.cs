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
        ProductCrud product = new ProductCrud();
        // GET: Product
        public ActionResult Shop()
        {
            ViewBag.Products = product.findAll();
            return View();
        }
    }
}