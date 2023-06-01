using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VivesShop.Core;
using VivesShop.Models;
using VivesShop.Services;

namespace VivesShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductCategoryService _ProductCategoryService;
        private readonly ProductService _ProductService;
        private readonly User _UserInfo;

        public HomeController(ProductCategoryService productCategoryService, ProductService productService, User userInfo)
        {
            _ProductCategoryService = productCategoryService;
            _ProductService = productService;
            _UserInfo = userInfo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var products = _ProductService.Find();
            return HomeView("Index", products);
        }

        [HttpGet]
        private IActionResult HomeView([AspMvcView] string viewName, IList<Product>? products = null)
        {
            var categories = _ProductCategoryService.Find();

            ViewBag.Categories = categories;
            ViewBag.UserInfo = _UserInfo;

            return View(viewName, products);
        }

        [HttpGet]
        public IActionResult Add(int id)
        {
            var product = _ProductService.Get(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            _UserInfo.ShoppingCart.Add(product);
            _UserInfo.GetTotal();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _UserInfo.ShoppingCart.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            _UserInfo.ShoppingCart.Remove(product);
            _UserInfo.GetTotal();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}