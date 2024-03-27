using SmartStore.BusinessLogic.DBModel;
using SmartStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.Core.ServiceAPI
{
    public class ProductsAPI
    {
        public List<ProductData> GetProductsData()
        {
            using (var db = new ProductContext())
            {
                var products = (from product in db.Products
                                select new ProductData
                                {
                                    ProductName = product.ProductName,
                                    ProductCategory = product.ProductCategory,
                                    ProductDescription = product.ProductDescription,
                                    ProductPrice = product.ProductPrice,
                                }).ToList();
                return products;
            }
        }
    }
}
