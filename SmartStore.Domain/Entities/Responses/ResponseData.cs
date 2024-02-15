using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Domain.Entities.User;

namespace SmartStore.Domain.Entities.Responses
{
    public class ResponseData
    {
        public bool Status { get; set; }
        public DBUser User { get; set; }
    }
}
