using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace PL.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public AdminController(ICategoryService categoryService,
                               ISubcategoryService subcategoryService,
                               IProductService productService,
                               IOrderService orderService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _productService = productService;
            _orderService = orderService;
        }

        // Helper
        private string SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/images/" + fileName;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _categoryService.GetAll().Count();
            ViewBag.Subcategory = _subcategoryService.GetAll().Count();
            ViewBag.Products = _productService.GetAll().Count();
            ViewBag.Orders = _orderService.GetAll().Count();
            return View();
        }

        // Categories
        public IActionResult Categories() => View(_categoryService.GetAll());
        public IActionResult AddCategory() => View();

        [HttpPost]
        public IActionResult AddCategory(Category category, IFormFile ImageFile)
        {
            category.ImageUrl = SaveImage(ImageFile);
            _categoryService.Add(category);
            return RedirectToAction("Categories");
        }

        public IActionResult EditCategory(int id) => View(_categoryService.GetById(id));

        [HttpPost]
        public IActionResult EditCategory(Category category, IFormFile ImageFile)
        {
            if (ImageFile != null)
                category.ImageUrl = SaveImage(ImageFile);
            _categoryService.Update(category);
            return RedirectToAction("Categories");
        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Categories");
        }

        // Subcategories
        public IActionResult Subcategories()
            => View(_subcategoryService.GetAllWithCategory());

        public IActionResult AddSubcategory()
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult AddSubcategory(Subcategory subcategory, IFormFile ImageFile)
        {
            subcategory.ImageUrl = SaveImage(ImageFile);
            _subcategoryService.Add(subcategory);
            return RedirectToAction("Subcategories");
        }

        public IActionResult EditSubcategory(int id)
        {
            ViewBag.Categories = _categoryService.GetAll();
            return View(_subcategoryService.GetById(id));
        }

        [HttpPost]
        public IActionResult EditSubcategory(Subcategory subcategory, IFormFile ImageFile)
        {
            if (ImageFile != null)
                subcategory.ImageUrl = SaveImage(ImageFile);
            _subcategoryService.Update(subcategory);
            return RedirectToAction("Subcategories");
        }

        public IActionResult DeleteSubcategory(int id)
        {
            _subcategoryService.Delete(id);
            return RedirectToAction("Subcategories");
        }

        // Products
        public IActionResult Products() => View(_productService.GetAll());

        public IActionResult AddProduct()
        {
            ViewBag.Subcategories = _subcategoryService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product, IFormFile ImageFile)
        {
            product.ImageUrl = SaveImage(ImageFile);
            _productService.Add(product);
            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(int id)
        {
            ViewBag.Subcategories = _subcategoryService.GetAll();
            return View(_productService.GetById(id));
        }

        [HttpPost]
        public IActionResult EditProduct(Product product, IFormFile ImageFile)
        {
            if (ImageFile != null)
                product.ImageUrl = SaveImage(ImageFile);
            _productService.Update(product);
            return RedirectToAction("Products");
        }

        public IActionResult DeleteProduct(int id)
        {
            _productService.Delete(id);
            return RedirectToAction("Products");
        }

        // Featured
        public IActionResult ToggleFeatured(int id)
        {
            var product = _productService.GetById(id);
            if (product != null)
            {
                product.IsFeatured = !product.IsFeatured;
                _productService.Update(product);
            }
            return RedirectToAction("Products");
        }

        // Orders
        public IActionResult Orders() => View(_orderService.GetAll());

        public IActionResult UpdateOrderStatus(int id, OrderStatus status)
        {
            _orderService.UpdateStatus(id, status);
            return RedirectToAction("Orders");
        }
    }
}