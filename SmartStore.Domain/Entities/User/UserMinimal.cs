using SmartStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities.Responses
{
    public class UserMinimal
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LoginDateTime { get; set; }
        public string LoginIP { get; set; }
        public URole Level { get; set; }
    }
}
