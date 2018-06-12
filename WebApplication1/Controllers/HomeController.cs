﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private UserCrud Model = new UserCrud();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "HELLOW AND WELCOM TO SPORTDB store, of sports clothing and footwear " +
                "my name is mor dabi and im here to give you the shippest price in the market.";

            return View("About");
        }

        public ActionResult Shop()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult logIn()
        {
            ViewBag.Message = "Your contact page.";
            

            return View();
        }
        public ActionResult Register()
        {
            ViewBag.Message = "Your contact page.";
            MvcApplication.ViewCheck = "";

            return View();
        }
        [System.Web.Mvc.HttpPost]
        public ActionResult InDatabase(User UserPN)
        {

            var RealUser = Model.findByName(UserPN.name);
            ModelState.Remove("Email");
            if (RealUser == null)
                ModelState.AddModelError(string.Empty, "User Does not Exist");
            else if (RealUser.password != UserPN.password)
                ModelState.AddModelError(string.Empty, "Wrong password,please try again");
            else if (RealUser.name == "admin")
                MvcApplication.ViewCheck = "admin";
                else MvcApplication.ViewCheck = "LogedIn";
            if (!ModelState.IsValid)
            {
                return View("logIn");
            }
            return View("index");
        }
        // GET: User
        public ActionResult AdminPlace()
        {
            ViewBag.Users = Model.findAll();
            return View("AdminPlace");
        }
        [HttpPost]
        public ActionResult Add(User NewUser)
        {
            if (Model.findByName(NewUser.name) != null)
            {
                ModelState.AddModelError(string.Empty, "User name allready Exist, Please try again");
                return View("Register");
            }
            if (ModelState.IsValid)
            {
                MvcApplication.ViewCheck = "LogedIn";
                Model.Create(NewUser);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Update(User NewUser, string id)
        {
            NewUser.id = new ObjectId(id);
            Model.Update(NewUser);
            return RedirectToAction("AdminPlace");
        }
        public ActionResult Delete(string id)
        {
            Model.Delete(id);
            return RedirectToAction("AdminPlace");
        }
        [HttpGet]
        public ActionResult Update(string id)
        {
            var model = Model.find(id);
            return View(model);
        }

    }
}