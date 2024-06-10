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

        public ActionResult Cart()
        {
      /*      SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Main");
            }*/
            return View();
        }

        public ActionResult Profile()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}