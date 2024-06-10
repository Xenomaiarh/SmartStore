using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Domain.Entities.Products
{
    public class ProductData
    {
        public int ID { get; set; }
        public bool isAvailable { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPicture { get; set; }
    }
}
