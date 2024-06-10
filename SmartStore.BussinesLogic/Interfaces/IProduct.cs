using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        Task<ResponseNewProduct> ConfirmPurchaseUserAction(int userId);
        Task<ResponseNewProduct> DeleteOrderAction(int userId, int productId);
        ResponseNewProduct EditQuntity(int userId, int productId, int quantityOrder);
        ResponseData GetProductsData();
        ResponseNewProduct PurchaseProduct(int userId, int productId, int quantity);
        ResponseNewProduct ViewOrdersAction(int userId);
        ResponseNewProduct ViewOrdersUserAction(int userId);
    }
}
