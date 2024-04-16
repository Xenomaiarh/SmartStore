using SmartStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BussinesLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=SmartStore")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
