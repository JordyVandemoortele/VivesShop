using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivesShop.Core;
using VivesShop.Models;
using VivesShop.Services;

namespace VivesShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductCategoryService _ProductCategoryService;
        private readonly ProductService _ProductService;

        public ProductController(ProductCategoryService productCategoryService, ProductService productService)
        {
            _ProductCategoryService = productCategoryService;
            _ProductService = productService;
        }

        public IActionResult Index()
        {
            var products = _ProductService.Find();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return CreateEditView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Create", product);
            }
            _ProductService.Create(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _ProductService.Get(id);

            if (product is null)
            {
                return RedirectToAction("Index");
            }

            return CreateEditView("Edit", product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return CreateEditView("Edit", product);
            }

            _ProductService.Update(id, product);

            return RedirectToAction("Index");
        }

        private IActionResult CreateEditView([AspMvcView] string viewName, Product? product = null)
        {
            var categories = _ProductCategoryService.Find();

            ViewBag.People = categories;

            return View(viewName, product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _ProductService.Get(id);

            if (product is null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [HttpPost("Product/Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ProductService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}