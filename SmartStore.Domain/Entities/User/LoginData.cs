using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities.User
{
    public class LoginData
    {
        public string Credentials { get; set; }
        public string Password { get; set; }
        public string LoginIP { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}
