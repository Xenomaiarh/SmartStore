using SmartStore.Atributes;
using SmartStore.Domain.Entities.Products;
using SmartStore.BusinessLogic.Interfaces;
using SmartStore.Domain.Entities.Responses;
using SmartStore.Domain.Entities.User;
using SmartStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.UI.WebControls;
using AutoMapper;
using System.Threading.Tasks;
using SmartStore.Extentions;

namespace SmartStore.Controllers
{
    public class AdminController : BaseController
    {

        private readonly IAddProduct _addProduct;
        private readonly IProduct _products;

        public AdminController()
        {
            var logicBl = new BusinessLogic.BusinessLogic();
            _addProduct = logicBl.AAddProductBL();
            _products = logicBl.getProducts();
        }

        [AdminAndModer]
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Login", "Main");
            }
            return View();
        }

        public ActionResult Products()
        {
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
        public ActionResult AddProductForm(Product product, HttpPostedFileBase ImageFile)
        {

            // Проверка на наличие и тип файла
            if (ImageFile != null && ImageFile.ContentLength > 0 && ImageFile.ContentType.StartsWith("image"))
            {
                var uploadPath = Server.MapPath("~/img/product");
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);
                ImageFile.SaveAs(filePath);

                product.ProductPicture = Url.Content(Path.Combine("~/img/product", fileName));
            }
            else
            {
                ModelState.AddModelError("", "Please upload a valid image file.");
                return View("~/Views/Home/AddProduct.cshtml", product);
            }

            Mapper.Initialize(cfg => cfg.CreateMap<Models.Product, ProductData>());
            var AdminAddProduct = Mapper.Map<ProductData>(product);

            ResponseNewProduct response = _addProduct.CreateNewProduct(AdminAddProduct);
            if (response != null && response.Status)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Failed to add the product. Please try again.");
                return View("~/Views/Home/AddProduct.cshtml", product);
            }

   
        }
        [HttpPost]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            try
            {
                // Ваш код здесь для удаления пользователя
                // Например, вы можете использовать сервис или репозиторий для выполнения этой операции
                ResponseSupport response = await _addProduct.DeleteUserAction(userId);

                if (response.Status)
                {
                    return Json(new { success = true }); // Возвращаем успешный результат в формате JSON
                }
                else
                {
                    return Json(new { success = false, errorMessage = response.ResponseSupportMessage }); // Возвращаем ошибку в формате JSON
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если необходимо
                // Здесь можно добавить логику для записи информации об ошибке
                return Json(new { success = false, errorMessage = ex.Message }); // Возвращаем ошибку в формате JSON
            }
        }

 
        public async Task<ActionResult> UserModif()
        {
            SessionStatus();
            int currentUserId = GetUserId();
            ResponseSupport responseSupport = await _addProduct.GetAdminPanelUsers(currentUserId);
            if (responseSupport.Status)
            {
                return View(responseSupport.TotalUsers);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ChangeUserRoleAction(int userId, string newRole)
        {
            ResponseSupport response = await _addProduct.ChangeUserRole(userId, newRole);

            if (response.Status)
            {
                return Json(new { success = true }); // Возвращаем результат в формате JSON
            }
            else
            {
                return Json(new { success = false, errorMessage = response.ResponseSupportMessage });
            }
        }

    }
}