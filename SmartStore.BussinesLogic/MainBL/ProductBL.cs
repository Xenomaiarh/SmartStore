using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.BusinessLogic.Core.ServiceAPI;
using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Domain.Entities.Responses;

namespace SmartStore.BusinessLogic.MainBL
{
    public class ProductBL : ProductsAPI, IProduct
    {
        public ResponseData GetProductsData()
        {
            return GetAllProducts();
        }

        public ResponseNewProduct PurchaseProduct(int userId, int productId, int quantity)
        {
            return PurchaseProductAction( userId, productId,  quantity);
        }
    }
}
