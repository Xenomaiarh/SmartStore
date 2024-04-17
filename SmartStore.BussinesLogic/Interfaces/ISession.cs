using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Domain.Entities.User;
using SmartStore.Domain.Entities.Responses;
using System.Web;

namespace SmartStore.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ResponseData UserLogin(LoginData data);
        ResponseRegisterData UserRegister(RegisterData userRegisterData);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
    }
}