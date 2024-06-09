using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Domain.Entities.User;
using SmartStore.BusinessLogic.DBModel.Seed;
using System.Security.Cryptography.X509Certificates;
using SmartStore.Helpers;
using System.ComponentModel.DataAnnotations;
using SmartStore.BusinessLogic.DBModel;
using AutoMapper;
using System.Web;
using SmartStore.Domain.Enums;


namespace SmartStore.BusinessLogic.Core
{
    public class UserAPI
    {
        internal ResponseData UserValidateSession(LoginData data)
        {
            using (var db = new UserContext())
            {
                // Поиск пользователя в базе данных по имени пользователя
                try
                {
                    var user = db.Users.FirstOrDefault(u => u.UserName == data.Credentials);
                    if (user == null)
                    {
                        user = db.Users.FirstOrDefault(u => u.Email == data.Credentials);
                    }
                    string pass = LoginHelper.HashGen(data.Password);
                    if (user != null && user.Password == pass)
                    {
                        if (user.Level == URole.Admin)
                        {
                            return new ResponseData { Status = true, AdminMod = true };
                        }
                        if (user.Level == URole.Moder)
                        {
                            return new ResponseData { Status = true, ModerMod = true };
                        }
                        // Аутентификация успешна
                        return new ResponseData { Status = true };
                    }
                    // Если пользователь не найден или пароль не совпадает
                    else return new ResponseData { Status = false };
                }
                catch (Exception ex) { return new ResponseData { Status = false, Message = ex.Message }; }
            }
        }
            internal ResponseRegisterData UserRegisterAction(RegisterData data)
            {
            try
            {
                var User = new DBUser
                {
                    UserName = data.UserName,
                    Email = data.Email,
                    Password = data.Password,
                    ID = data.ID,
                    LoginIP = data.LoginIP,
                    RegisterDateTime = DateTime.Now,
                    Level = URole.User
                };

                User.Password = LoginHelper.HashGen(User.Password);
                using (var db = new UserContext())
                {
                    db.Users.Add(User);
                    db.SaveChanges();
                }
                return new ResponseRegisterData { Status = true };
            }
            catch (Exception ex) { return new ResponseRegisterData { Status = false, Message = ex.Message }; }
        }
        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Session curent;
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(loginCredential))
                {
                    curent = (from e in db.Sessions where e.Email == loginCredential select e).FirstOrDefault();
                }
                else
                {
                    curent = (from e in db.Sessions where e.Username == loginCredential select e).FirstOrDefault();
                }

                if (curent != null)
                {
                    curent.CookieString = apiCookie.Value;
                    curent.ExpireTime = DateTime.Now.AddMinutes(60);
                    using (var todo = new SessionContext())
                    {
                        todo.Entry(curent).State = EntityState.Modified;
                        todo.SaveChanges();
                    }
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                    db.SaveChanges();
                }
            }

            return apiCookie;
        }

        internal UserMinimal UserCookie(string cookie)
        {
            Session session;
            DBUser curentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;
            using (var db = new UserContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    curentUser = db.Users.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    curentUser = db.Users.FirstOrDefault(u => u.UserName == session.Username);
                }
            }

            if (curentUser == null) return null;
            Mapper.Initialize(cfg => cfg.CreateMap<DBUser, UserMinimal>());
            var userminimal = Mapper.Map<UserMinimal>(curentUser);
            
            return userminimal;
        }
        internal ResponseData UserLogoutAction()
        {
            var response = new ResponseData { Status = false };

            try
            {
                var httpContext = HttpContext.Current;
                if (httpContext != null && httpContext.Request.Cookies["X-KEY"] != null)
                {
                    var cookie = new HttpCookie("X-KEY")
                    {
                        Expires = DateTime.Now.AddDays(-1)
                    };
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    response.Status = true;
                    response.Message = "User successfully logged out.";
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
    }
}

