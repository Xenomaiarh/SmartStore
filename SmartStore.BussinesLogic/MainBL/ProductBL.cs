using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.BusinessLogic.Core.ServiceAPI;
using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Domain.Entities.Products;
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

       public  ResponseNewProduct ViewOrdersUserAction(int userId)
        {
            return GetUserOrders(userId);
        }
        public ResponseNewProduct ViewOrdersAction(int userId)
        {
            return GetOrders(userId);
        }
        public async Task<ResponseNewProduct> DeleteOrderAction(int userId, int productId)
        {
            return await DeleteOrder(userId, productId);
        }
        public async Task<ResponseNewProduct> ConfirmPurchaseUserAction(int userId)
        {
            return await ConfirmPurchaseAction(userId);
        }
        public ResponseNewProduct EditQuntity(int userId, int productId, int quantityOrder)
        {
            return EditOrderQuantity(userId, productId, quantityOrder);
        }


    }
}
