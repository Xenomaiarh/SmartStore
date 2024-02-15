using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStore.Models;
using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Domain.Entities.Responses;
using SmartStore.Domain.Entities.User;

namespace SmartStore.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        internal ISession session;
        public MainController()
        {
            var BL = new BusinessLogic.BusinessLogic();
            session = BL.getSessionBL();

        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Form(Registration data)
        {

            var UserLoginData = new LoginData
            {
                Credentials = data.name,
                Password = data.pass,
                LoginIP = HttpContext.Request.UserHostAddress,
                LoginDateTime = DateTime.Now,
            };
            ResponseData response = session.UserLogin(UserLoginData);

            if (response != null && response.Status)
            {
                Session["UserName"] = response.User.name;
                return RedirectToAction("Index", "Main");
            }

            int y = 0;
            return View();
            //return RedirectToAction("Index", "Main");
        }
    }
}