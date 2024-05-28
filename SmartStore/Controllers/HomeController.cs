using SmartStore.Atributes;
using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Controllers
{
    public class HomeController : BaseController
    {
        internal IProduct products;
        public ActionResult Index()
        {
            SessionStatus(); 
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Main");
            }
            //var ProductsList = products.GetProductsData();
            //return View(ProductsList);
            return View();
        }

        public ActionResult Cart()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Main");
            }
            return View();
        }

        public ActionResult Profile()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Main");
            }
            return View();
        }
    }
}