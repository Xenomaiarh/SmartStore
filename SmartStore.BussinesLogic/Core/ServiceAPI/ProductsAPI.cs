using SmartStore.BusinessLogic.DBModel;
using SmartStore.BusinessLogic.DBModel.Seed;
using SmartStore.BussinesLogic.DBModel;
using SmartStore.Domain.Entities.Orders;
using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using SmartStore.Domain.Enums;
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
        internal async Task<ResponseNewProduct> DeleteOrder(int userId, int productId)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    // Находим заказ пользователя с заданным userId и productId
                    var order = await db.Orders
                                        .FirstOrDefaultAsync(o => o.UserId == userId && o.ProductId == productId && o.orderStatus == OrderStatus.Pending);

                    if (order != null)
                    {
                        // Если заказ найден, удаляем его из контекста
                        db.Orders.Remove(order);

                        // Асинхронно сохраняем изменения в базе данных
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        // Заказ не найден
                        return new ResponseNewProduct { Status = false, Message = "Order not found" };
                    }

                    return new ResponseNewProduct { Status = true };
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                return new ResponseNewProduct { Status = false, Message = ex.Message };
            }
        }
        internal ResponseNewProduct EditOrderQuantity(int userId, int productId, int quantityOrder)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    // Находим заказ пользователя с заданным userId и productId
                    var order = db.Orders
                                   .FirstOrDefault(o => o.UserId == userId && o.ProductId == productId && o.orderStatus == OrderStatus.Pending);


                    if (order != null)
                    {
                        // Обновляем количество товара и вычисляем новую суммарную стоимость заказа
                        order.QuantityOrder = quantityOrder;
                        order.TotalPrice = order.Product.ProductPrice * quantityOrder;
                        order.TotalAmount = order.TotalPrice;

                        // Сохраняем изменения в базе данных
                        db.SaveChanges();
                        var orders = db.Orders.Where(o => o.orderStatus == OrderStatus.Pending && o.UserId == userId).ToList();
                        return new ResponseNewProduct { Status = true, Message = "Order quantity updated successfully.", OneOrder = order, Orders = orders };
                    }
                    else
                    {
                        return new ResponseNewProduct { Status = false, Message = "Order not found for the specified user and product." };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseNewProduct { Status = false, Message = ex.Message };
            }
        }
        internal async Task<ResponseNewProduct> ConfirmPurchaseAction(int userId)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    // Находим все заказы пользователя с заданным userId
                    var userOrders = await db.Orders
                                              .Where(o => o.UserId == userId && o.orderStatus == OrderStatus.Pending)
                                              .ToListAsync();

                    if (userOrders.Any())
                    {
                        decimal totalAmountOfOrders = userOrders.Sum(o => o.TotalAmount);

                   


                        foreach (var order in userOrders)
                        {
                            // Находим все товары, связанные с текущим заказом
                            var orderItems = await db.Orders.Where(oi => oi.OrderId == order.OrderId).ToListAsync();
                            order.orderStatus = OrderStatus.Successful;
                         



                            await db.SaveChangesAsync();
                        }

                        // Удаляем все заказы пользователя из контекста


                        // Сохраняем изменения в базе данных
                        await db.SaveChangesAsync();

                        return new ResponseNewProduct { Status = true };
                    }
                    else
                    {
                        // Заказы пользователя не найдены
                        return new ResponseNewProduct { Status = false, Message = "No orders found for the user" };
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений
                return new ResponseNewProduct { Status = false, Message = ex.Message };
            }
        }

        internal ResponseNewProduct GetUserOrders(int userId)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    // Получаем все заказы данного пользователя с указанным productId
                    var orders = db.Orders.Where(o => o.UserId == userId && o.orderStatus == OrderStatus.Pending).ToList();

                    // Создаем список для хранения обработанных заказов
                    var processedOrders = new List<Order>();

                    foreach (var order in orders)
                    {

                        // Добавляем заказ в список обработанных заказов
                        processedOrders.Add(new Order
                        {
                            ProductId = order.ProductId,
                            Product = order.Product,
                            QuantityOrder = order.QuantityOrder,
                            TotalAmount = order.TotalAmount,
                            ProductType = order.ProductType,
                            TotalPrice = order.TotalPrice,
                            // Другие необходимые свойства заказа, которые нужно сохранить
                        })
                            ; ;

                    }

                    // Возвращаем успешный результат с обработанными заказами
                    return new ResponseNewProduct
                    {
                        Status = true,
                        Orders = processedOrders
                    };
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем статус false и сообщение об ошибке
                return new ResponseNewProduct { Status = false, Message = ex.Message };
            }
        }
        internal ResponseNewProduct GetOrders(int userId)
        {
            try
            {
                using (var db = new OrderContext())
                {
                    var orders = db.Orders.Where(o => o.UserId == userId && o.orderStatus == OrderStatus.Successful).ToList();

                    // Проверяем, есть ли заказы
                    if (orders.Any())
                    {
                        return new ResponseNewProduct
                        {
                            Status = true,
                            Orders = orders.Select(p => new Order
                            {
                                OrderId = p.OrderId,
                                Product = p.Product,
                                ProductId = p.ProductId,
                                TotalPrice = p.TotalPrice,
                                OrderDate = p.OrderDate,
                                UDbTable = p.UDbTable,
                                QuantityOrder = p.QuantityOrder,
                                TotalAmount = p.TotalAmount,
                                UserId = p.UserId,
                                ProductType = p.ProductType,
                                Rating = p.Rating,


                            }).ToList()
                        };
                    }
                    else
                    {
                        return new ResponseNewProduct { Status = false, Message = "Вы ещё не совершили покупку товаров.Попробуйте купить что-нибудь в нашем магазине!" };
                    }
                }
            }
            catch (Exception ex)
            {
                // В случае ошибки возвращаем статус false и сообщение об ошибке
                return new ResponseNewProduct { Status = false, Message = ex.Message };
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
