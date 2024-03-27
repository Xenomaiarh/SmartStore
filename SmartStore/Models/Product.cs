using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPicture { get; set; }
    }
}