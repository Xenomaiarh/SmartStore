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
        public string name { get; set; }
        public string email { get; set; }
        public string pass { get; set; }
    }
}
