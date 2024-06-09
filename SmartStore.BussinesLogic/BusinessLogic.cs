using SmartStore.BusinessLogic.Interfaces;
using SmartStore.BusinessLogic.MainBL;
using SmartStore.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession getSessionBL()
        {
            return new SessionBL();
        }
        public IAddProduct AAddProductBL()
        {
            return new AddProductBL
                ();
        }
        public IProduct getProducts()
        {
            return new ProductBL();
        }
    }
}