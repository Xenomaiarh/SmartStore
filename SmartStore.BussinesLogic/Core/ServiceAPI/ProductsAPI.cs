using SmartStore.BusinessLogic.DBModel;
using SmartStore.BusinessLogic.DBModel.Seed;
using SmartStore.Domain.Entities.Orders;
using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.BusinessLogic.Core.ServiceAPI
{
    public class ProductsAPI
    {
        internal ResponseData GetAllProducts()
        {
            try
            {
                using (var db = new ProductContext())
                {
                    var productSellers = db.Products.OrderByDescending(p => p.ProductPrice).ToList();


                    return new ResponseData
                    {
                        Status = true,
                        Products = productSellers.Select(p => new ProductData
                        {
                            ProductName = p.ProductName,
                            ProductPicture = p.ProductPicture,
                            ProductCategory = p.ProductCategory, // Используем цену с учетом скидки
                            ProductDescription = p.ProductDescription,
                            ProductPrice = p.ProductPrice,
                            ID = p.ID,
                            isAvailable = p.isAvailable,
                        }).ToList(),

                       
                    };
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем статус false и сообщение об ошибке
                return new ResponseData { Status = false, Message = ex.Message };
            }
        }
        internal ResponseNewProduct PurchaseProductAction(int userId, int productId, int quantity)
        {
            try
            {
                using (var db = new UserContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.ID == userId);
                    var product = db.Products.FirstOrDefault(p => p.ID == productId);
                    var existingOrder = db.Orders.FirstOrDefault(o => o.UserId == userId && o.ProductId == productId && o.orderStatus !=  Domain.Enums.OrderStatus.Successful);

                    if (user == null || product == null)
                    {
                        return new ResponseNewProduct { Status = false, Message = "User or product not found" };
                    }

                    if (existingOrder != null)
                    {
                        // Update existing order quantity
                    /*    existingOrder.Product.Quantity++;*/
                        existingOrder.QuantityOrder += quantity;
                        existingOrder.TotalPrice = product.ProductPrice;
                        db.Entry(existingOrder).State = EntityState.Modified;
                        existingOrder.orderStatus = Domain.Enums.OrderStatus.Pending;
                        db.SaveChanges();

                        return new ResponseNewProduct { Status = true, Order = existingOrder };
                    }
                    else
                    {
                        // Create a new order
                        decimal totalPrice = product.ProductPrice * quantity;
                        var order = new Order
                        {
                            UserId = userId,
                            ProductId = productId,
                            QuantityOrder = quantity,
                            TotalPrice = totalPrice,
                            OrderDate = DateTime.UtcNow,
                            ProductType = product.ProductCategory,
                            TotalAmount = totalPrice,
                            orderStatus = Domain.Enums.OrderStatus.Pending,

                        };

                        db.Orders.Add(order);
                        db.SaveChanges();

                        return new  ResponseNewProduct { Status = true, Order = order };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseNewProduct { Status   = false, Message = ex.Message };
            }
        }
    }
    
 }
