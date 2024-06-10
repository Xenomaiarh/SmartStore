using SmartStore.Domain.Entities.Orders;
using SmartStore.Domain.Entities.Products;
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
        public UserContext() : base("name=SmartStore") // connection string name defined in your web.config
        {

        }

        public virtual DbSet<DBUser> Users { get; set; }

        public virtual DbSet<DBProduct> Products { get; set; }

        public virtual DbSet<Order> Orders { get; set; } // DbSet для заказов

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Определение отношений между сущностями
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.UDbTable) // Каждый заказ связан с одним пользователем
                .WithMany(u => u.Orders) // У каждого пользователя много заказов
                .HasForeignKey(o => o.UserId); // Внешний ключ UserId в таблице Order

            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Product) // Каждый заказ связан с одним продуктом
                .WithMany() // У каждого продукта много заказов
                .HasForeignKey(o => o.ProductId); // Внешний ключ ProductId в таблице Order

            base.OnModelCreating(modelBuilder);
        }

    }
}