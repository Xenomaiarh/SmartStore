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
        public ActionResult AddProductForm(Product data)
        {
            var newProductData = new ProductData
            {
                ProductName = data.ProductName,
                ProductDescription = data.ProductDescription,
                ProductPrice = data.ProductPrice,
                ProductCategory = data.ProductCategory,
                ProductPicture = data.ProductPicture,
            };
            _addProduct.CreateNewProduct(newProductData);


            return RedirectToAction("Index", "Admin");
        }

    }
}