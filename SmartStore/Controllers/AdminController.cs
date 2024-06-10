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

namespace SmartStore.Controllers
{
    public class AdminController : BaseController
    {

        private readonly IAddProduct _addProduct;

        public AdminController()
        {
            var logicBl = new BusinessLogic.BusinessLogic();
            _addProduct = logicBl.AAddProductBL();
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

    }
}