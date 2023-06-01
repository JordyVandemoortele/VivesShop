using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using VivesShop.Core;
using VivesShop.Models;
using VivesShop.Services;

namespace VivesShop.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly ProductCategoryService _ProductCategoryService;
        public ProductCategoryController(ProductCategoryService productCategoryService)
        {
            _ProductCategoryService = productCategoryService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _ProductCategoryService.Find();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _ProductCategoryService.Create(category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _ProductCategoryService.Get(id);

            if (category is null)
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _ProductCategoryService.Update(id, category);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var article = _ProductCategoryService.Get(id);

            if (article is null)
            {
                return RedirectToAction("Index");
            }

            return View(article);
        }

        [HttpPost("ProductCategory/Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _ProductCategoryService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
