using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.DBModel
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("name=SmartStore")
        {
        }
        public virtual DbSet<DBProduct> Products { get; set; }
    }
}
