using SmartStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities.User
{
    public class DBUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginIP { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public URole Level { get; set; }
    }
}
