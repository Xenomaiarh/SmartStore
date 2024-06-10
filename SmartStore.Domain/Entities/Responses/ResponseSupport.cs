using SmartStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities.Responses
{
    public  class ResponseSupport
    {
        public bool Status { get; set; }
        public string ResponseSupportMessage { get; set; }

        public List<DBUser> TotalUsers { get; set; } // Свойство для списка пользователей
    }
}
