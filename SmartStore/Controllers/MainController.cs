using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartStore.Models;
using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Domain.Entities.Responses;
using SmartStore.Domain.Entities.User;
using System.Web.UI.WebControls;

namespace SmartStore.Controllers
{
    public class MainController : BaseController
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
        public ActionResult Registration(Registration data)
        {
            var UserRegisterData = new RegisterData
            {
                UserName = data.name,
                Password = data.pass,
                LoginIP = HttpContext.Request.UserHostAddress,
                RegisterDateTime = DateTime.Now,
                Email = data.email,
            };
            ResponseRegisterData response = session.UserRegister(UserRegisterData);

            if (response != null && response.Status)
            {
                return RedirectToAction("Login", "Main");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Form(Authentification data)
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
                HttpCookie cookie = session.GenCookie(data.name);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}