using SmartStore.BusinessLogic.DBModel;
using SmartStore.Domain.Entities.Products;
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
        public void CreateNewProduct(ProductData NewProduct)
        {

            var newProduct = new DBProduct
            {
                ProductName = NewProduct.ProductName,
                ProductDescription = NewProduct.ProductDescription,
                ProductCategory = NewProduct.ProductCategory,
                ProductPicture = NewProduct.ProductPicture,
                ProductPrice = NewProduct.ProductPrice,
            };

            using (var DbContext = new ProductContext())
            {
                DbContext.Products.Add(newProduct);
                DbContext.SaveChanges();

            }
        }
    }
}
