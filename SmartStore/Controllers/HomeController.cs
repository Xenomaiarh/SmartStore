using SmartStore.Atributes;
using SmartStore.BusinessLogic;
using SmartStore.BusinessLogic.Interfaces;
using SmartStore.BusinessLogic.MainBL;
using SmartStore.Domain.Entities.Products;
using SmartStore.Domain.Entities.Responses;
using SmartStore.Domain.Entities.User;
using SmartStore.Extentions;
using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartStore.Controllers
{
    public class HomeController : BaseController
    {
        internal IProduct _products;

        public HomeController()
        {
            var bl = new SmartStore.BusinessLogic.BusinessLogic();
            _products = bl.getProducts();

        }
        [HttpPost]
        public JsonResult UpdateQuantity(int productId, int quantityOrder)
        {
            int userId = (int)Convert.ToUInt32(Session["UserId"]);
            if (userId == 0)
            {
                GetUserId();
                userId = (int)Convert.ToUInt32(Session["UserId"]);
            }

            ResponseNewProduct response = _products.EditQuntity(userId, productId, quantityOrder);
            var resp = 0;
            var viewModelOrders = response.Orders.Select(p => new OrderPr
            {
                OrderId = p.OrderId,
                /*                Product = p.Product,*/
                ProductId = p.ProductId,
                TotalPrice = p.TotalPrice,
                OrderDate = p.OrderDate,
                /*                UDbTable = p.UDbTable,*/
                QuantityOrder = p.QuantityOrder,
                TotalAmount = p.TotalAmount,
                UserId = p.UserId,
                ProductType = p.ProductType,
                // ImageUrl = p.ImageUrl
            }).ToList();

            var updatedOrder = viewModelOrders.FirstOrDefault(o => o.ProductId == productId);
            var newTotalAmount = viewModelOrders.Sum(o => o.TotalPrice);

            return Json(new
            {
                status = response.Status,
                message = response.Message,
                orders = viewModelOrders, // Включаем список заказов в ответ
                updatedTotalPrice = updatedOrder?.TotalPrice.ToString("C"),
                newTotalAmount = newTotalAmount.ToString("C")
            });
        }
        public ActionResult OrderConfirm()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Main");
            }

            int userId = (int)Convert.ToUInt32(Session["UserId"]);
            if (userId == 0)
            {
                GetUserId();
                userId = (int)Convert.ToUInt32(Session["UserId"]);
            }

            // Вызов метода из бизнес-логики для получения всех продуктов
            ResponseNewProduct response = _products.ViewOrdersUserAction(userId);
            var viewModelOrders = response.Orders.Select(p => new OrderPr
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
                ProductType = p.ProductType

            }).ToList();
            if (response.Status)
            {
                // Если запрос прошёл успешно, отображаем список продуктов
                return View(viewModelOrders);
            }
            else
            {
                // Если при запросе возникла ошибка, отображаем сообщение об ошибке
                ViewBag.ErrorMessage = response.Message;
                return View("Error");
            }



        }
        [HttpPost]
        public async Task<JsonResult> DeleteOrder(int productId)
        {
            int userId = (int)Convert.ToUInt32(Session["UserId"]);
            if (userId == 0)
            {
                GetUserId();
                userId = (int)Convert.ToUInt32(Session["UserId"]);
            }
            ResponseNewProduct response = await _products.DeleteOrderAction(userId, productId);
            if (response.Status)
            {
                return Json(new { success = true });
            }
            else
                return Json(new { status = false }); // Ошибка при удалении
        }
        public async Task<ActionResult> ConfirmPurchase()
        {
            int userId = (int)Convert.ToUInt32(Session["UserId"]);
            if (userId == 0)
            {
                GetUserId();
                userId = (int)Convert.ToUInt32(Session["UserId"]);
            }

            // Вызов метода из бизнес-логики для получения всех продуктов
            ResponseNewProduct response = await _products.ConfirmPurchaseUserAction(userId);
   /*         var viewModelOrders = response.Orders.Select(p => new OrderPr
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
                ProductType = p.ProductType

            }).ToList();*/
            if (response.Status)
            {
                return Json(new { status = true });
            }
            else
            {
                return Json(new { status = false });
            }
        }
        public ActionResult Cart()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Account");
            }
            int userId = (int)Convert.ToUInt32(Session["UserId"]);
            if (userId == 0)
            {
                GetUserId();
                userId = (int)Convert.ToUInt32(Session["UserId"]);
            }

            // Вызов метода из бизнес-логики для получения всех продуктов
            ResponseNewProduct response = _products.ViewOrdersAction(userId);
            if (response == null)
            {
                TempData["ErrorMessage"] = "Error retrieving orders.";
                return View();
            }

            var viewModelOrders = response.Orders?.Select(p => new OrderPr
            {
                OrderId = p.OrderId,
                Product = p.Product,
                ProductId = p.ProductId,
                OrderDate = p.OrderDate,
                UDbTable = p.UDbTable,
                QuantityOrder = p.QuantityOrder,
                TotalAmount = p.TotalAmount,
                UserId = p.UserId,
                ProductType = p.ProductType,
                Rating = p.Rating,
                AverageRating = p.AverageRating,
                Price = p.Product.ProductPrice
            }).ToList();

            if (response.Status)
            {
                // Если запрос прошёл успешно, отображаем список продуктов
                return View(viewModelOrders);
            }
            else
            {
                // Если при запросе возникла ошибка, отображаем сообщение об ошибке
                TempData["ErrorMessage"] = response.Message;
                return View();
            }
        }
        public ActionResult Profile()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Main");
            }
            int userId = (int)Convert.ToUInt32(Session["UserId"]);
            if (userId == 0)
            {
                GetUserId();
                userId = (int)Convert.ToUInt32(Session["UserId"]);
            }

            // Вызов метода из бизнес-логики для получения всех продуктов
            ResponseNewProduct response = _products.ViewOrdersAction(userId);
            if (response == null)
            {
                TempData["ErrorMessage"] = "Error retrieving orders.";
                return View();
            }

            var viewModelOrders = response.Orders?.Select(p => new OrderPr
            {
                OrderId = p.OrderId,
                Product = p.Product,
                ProductId = p.ProductId,
                OrderDate = p.OrderDate,
                UDbTable = p.UDbTable,
                QuantityOrder = p.QuantityOrder,
                TotalAmount = p.TotalAmount,
                UserId = p.UserId,
                ProductType = p.ProductType,
                Rating = p.Rating,
                AverageRating = p.AverageRating,
                Price = p.Product.ProductPrice
            }).ToList();

            if (response.Status)
            {
                // Если запрос прошёл успешно, отображаем список продуктов
                return View(viewModelOrders);
            }
            else
            {
                // Если при запросе возникла ошибка, отображаем сообщение об ошибке
                TempData["ErrorMessage"] = response.Message;
                return View();
            }
        }
        public ActionResult Index()
        {
      /*      SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Account");
            }*/


            var user = System.Web.HttpContext.Current.GetMySessionObject();

            ResponseData response = _products.GetProductsData();
            if (response.Status)
            {
                var viewModelProducts = response.Products.Select(p => new Product
            {
                ProductName = p.ProductName,
                ProductPicture = p.ProductPicture,
                ProductCategory = p.ProductCategory, // Используем цену с учетом скидки
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                ID = p.ID,

            }).ToList();
/*
            var viewModel = new ProductHome
            {
                ProductsHomeIndex = viewModelProducts,

            };*/
         
                // Если запрос прошёл успешно, отображаем список продуктов
                return View(viewModelProducts);
            }
            else
            {
                // Если при запросе возникла ошибка, отображаем сообщение об ошибке
                ViewBag.ErrorMessage = response.Message;
                return View("Error");
            }
        }

         [HttpPost]
        public JsonResult AddProductToCart(int productId ,int quantity)
        {
            GetUserId();
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return Json(new { success = false , errorMessage = "Вы не авторизированы"});       
            }
            try
            {

                int UserId = (int)Convert.ToUInt32(Session["UserId"]);
                if (UserId == 0)
                {
                    GetUserId();
     
                    UserId = (int)Convert.ToUInt32(Session["UserId"]);
                }

                // Логика добавления товара в корзину
                ResponseNewProduct resp = _products.PurchaseProduct(UserId, productId, quantity);
                if (resp.Status)
                    // Переадресация обратно к списку товаров или на другую страницу
                    return Json(new { success = true });
                else
                    return Json(new { success = false });

            }
            catch (Exception ex)
            {
                // Обработка ошибки, если не удалось добавить товар в корзину
                ViewBag.ErrorMessage = "Произошла ошибка при добавлении товара в корзину.";
                ViewBag.ErrorMessage = ex.Message;
                return Json(new { success = false });
            }
        }
    }
}