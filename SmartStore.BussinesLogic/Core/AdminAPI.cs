using SmartStore.BusinessLogic.DBModel;
using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.Core
{
    public class AdminAPI
    {
        public AdminAPI() { }
        public ResponseNewProduct CreateNewProduct(ProductData products)
        {
            if (products == null)
            {
                string Error = "Error to add product";
                return new ResponseNewProduct { Status = false, Message = Error };
            }


            var product = new DBProduct()
            {
                ProductName = products.ProductName,
                ProductPicture = products.ProductPicture,
                ProductCategory = products.ProductCategory, // Используем цену с учетом скидки
                ProductDescription = products.ProductDescription,
                ProductPrice = products.ProductPrice,
                ID = products.ID,
                isAvailable = products.isAvailable,


            };

            using (var db = new ProductContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            return new ResponseNewProduct { Status = true };
        }
    }
}
