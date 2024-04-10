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

            var UserLoginData = new LoginData()
            {
                name = data.name,
                email = data.email,
                Password = data.pass,
                LoginIP = HttpContext.Request.UserHostAddress,
                LoginDateTime = DateTime.Now,
            };

            ResponseData response = session.UserLogin(UserLoginData);

            if (response != null && response.Status)
            {
                Session["UserName"] = response.User.name;
                return RedirectToAction("Index", "Home");
            }

            int y = 0;
            //return View();
            return RedirectToAction("Login", "Main");
        }
    }
}