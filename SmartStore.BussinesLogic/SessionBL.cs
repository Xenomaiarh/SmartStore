using SmartStore.BusinessLogic.Interfaces;
using SmartStore.BussinesLogic.Core;
using SmartStore.Domain.Entities.User;
using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BussinesLogic
{
    public class SessionBL : UserAPI, ISession
    {
        public ResponseData UserLogin(LoginData data)
        {
            return UserValidateSession(data);
        }

    }
}
