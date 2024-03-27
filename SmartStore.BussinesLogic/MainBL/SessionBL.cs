using SmartStore.BusinessLogic.Interfaces;
using SmartStore.BusinessLogic.Core;
using SmartStore.Domain.Entities.User;
using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartStore.BusinessLogic.MainBL
{
    public class SessionBL : UserAPI, ISession
    {
        public ResponseData UserLogin(LoginData data)
        {
            return UserValidateSession(data);
        }

        public ResponseRegisterData UserRegister(RegisterData userRegisterData)
        {
            return UserRegisterAction(userRegisterData);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
