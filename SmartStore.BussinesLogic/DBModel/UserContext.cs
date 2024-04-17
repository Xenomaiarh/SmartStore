using SmartStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.DBModel.Seed
{
    public class UserContext : DbContext
    {
        public UserContext() : base(nameOrConnectionString: "name=SmartStore")
        {

        }

        public virtual DbSet<DBUser> Users { get; set; }
    }
}