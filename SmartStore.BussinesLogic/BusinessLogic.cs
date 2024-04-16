using SmartStore.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession getSessionBL()
        {
            return new BussinesLogic.SessionBL();
        }
    }
}