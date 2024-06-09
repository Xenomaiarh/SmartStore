﻿using SmartStore.Atributes;
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
            return View(); // RedirectToAction("Login", "Main");
        }
    }
}