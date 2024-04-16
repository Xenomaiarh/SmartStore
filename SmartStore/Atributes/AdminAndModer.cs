using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Atributes
{
    public class AdminAndModerAttribute : ActionFilterAttribute
    {
        public readonly ISession _sessionBusinessLogic;
        public AdminAndModerAttribute()
        {
            var businessLogic = new SmartStore.BusinessLogic.BusinessLogic();
            _sessionBusinessLogic = businessLogic.getSessionBL();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _sessionBusinessLogic.GetUserByCookie(apiCookie.Value);

                if (profile != null && profile.Level == SmartStore.Domain.Enums.URole.Admin || profile.Level == SmartStore.Domain.Enums.URole.Moder)
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                }
            }
        }
    }
}