using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Domain.Entities.User;

namespace SmartStore.BussinesLogic.Core
{
    public class UserAPI
    {
        internal ResponseData UserValidateSession(LoginData data)
        {
            var testUser = new DBUser
            {
                name = "Admin",
                email = "admin@xenomaiarh.ru",
                pass = "test"
            };

            var status = (testUser.email == data.email && testUser.name == data.name && testUser.pass == data.Password) ? true : false;

            return new ResponseData
            {
                Status = status,
                User = testUser
            };
        }
    }
}
