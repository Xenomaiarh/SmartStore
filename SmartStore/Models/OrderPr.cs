using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.User;
using SmartStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Models
{
    public class OrderPr 
    {
       
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public int ProductId { get; set; }
            public int QuantityOrder { get; set; }
            public decimal TotalAmount { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal Price { get; set; }
            public virtual DBProduct Product { get; set; } // Свойство для связи с продуктом
            /*        public string ImageUrl { get; set; }*/
            public virtual DBUser UDbTable { get; set; } // Свойство для связи с пользователем

            /*        public virtual RatingUdbTable RatingUdbTable { get; set; }*/
            public string ProductType { get; set; }
            public OrderStatus orderStatus { get; set; }
            /*        public string Feedback { get; set; }*/
            public int Rating { get; set; }
            public double AverageRating { get; set; }
        public decimal TotalPrice { get; internal set; }
    }
}