using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Domain.Entities.Responses;
using SmartStore.Domain.Entities.User;
using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        private readonly ISession session;
        public AccountController()
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
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Authentification data)
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
        public ActionResult Logout()
        {
            ResponseData logout = session.UserLogout();

            if (Response.Cookies["X-KEY"] != null)
            {
                //var IsUserLoggedIn = false; // Статус аутентификации сохраняется между запросами   
                //var admin = false;
                //var moderator = false;
                //var userId = 0;
                //Session["UserId"] = userId;
                //Session["admin"] = admin.ToString(); // Сохраняем роль в сессии
                //Session["moderator"] = moderator.ToString(); // Сохраняем роль в сессии
                //["IsUserLoggedIn"] = IsUserLoggedIn.ToString(); // Сохраняем роль в сессии

                var cookie = new HttpCookie("X-KEY")
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(cookie);
            }

            // Перенаправляем пользователя на страницу входа
            return RedirectToAction("Login", "Account");
        }
    }
}