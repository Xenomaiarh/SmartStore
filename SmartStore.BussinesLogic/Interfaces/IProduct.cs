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
        ResponseData GetProductsData();
        ResponseNewProduct PurchaseProduct(int userId, int productId, int quantity);
    }
}
