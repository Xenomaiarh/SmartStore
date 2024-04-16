using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SmartStore.Domain.Entities.User;

namespace SmartStore.Domain.Entities.Responses
{
    public class ResponseData
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public DBUser User { get; set; }
        public bool AdminMod { get; set; }
        public bool ModerMod { get; set; }
    }
}
