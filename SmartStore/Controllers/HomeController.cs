using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Homee
        public ActionResult Index()
        {
            Peg tmp = new Peg();
            tmp.name = "Hi Guys";
            return View(tmp);
        }
    }
}