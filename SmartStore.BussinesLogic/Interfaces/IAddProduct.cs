using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.Interfaces
{
    public interface IAddProduct
    {
        void CreateNewProduct(ProductData productData);
    }
}
