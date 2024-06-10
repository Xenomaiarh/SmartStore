using SmartStore.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities.Responses
{
    public class ResponseNewProduct
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public Order Order { get; set; }
    }
}
