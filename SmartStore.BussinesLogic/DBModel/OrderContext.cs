using SmartStore.Domain.Entities.Orders;
using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BussinesLogic.DBModel
{
    public class OrderContext : DbContext
    {
        public OrderContext() : base("name=SmartStore")
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<DBUser> Users { get; set; } // DbSet для пользователей
        public virtual DbSet<DBProduct> Products { get; set; } // DbSet для продуктов
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Вызываем метод OnModelCreating из базового класса для применения общих настроек
            base.OnModelCreating(modelBuilder);

            // Дополнительные настройки для отношений, если необходимо
            // Например, убедитесь, что используются настройки для сущности Order из UserContext
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.UDbTable)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId);
        }
    }
}
