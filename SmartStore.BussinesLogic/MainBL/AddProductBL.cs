using SmartStore.BusinessLogic.Interfaces;
using SmartStore.BusinessLogic.Core;
using SmartStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartStore.BusinessLogic.DBModel;

namespace SmartStore.BusinessLogic.MainBL
{
    public class AddProductBL : AdminAPI, IAddProduct
    {
        private readonly ProductContext _productContext;

        public AddProductBL()
        {
        }
        public AddProductBL(ProductContext productContext)
        {
            _productContext = productContext;
        }
        public void CreareProduct(ProductData NewProduct)
        {
            CreateNewProduct(NewProduct);
        }
    }
}
