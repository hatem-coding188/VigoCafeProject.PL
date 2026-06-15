using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            ViewBag.Categories = _categoryService.GetAll().Count();
            ViewBag.Products = _productService.GetAll().Count();
            ViewBag.Orders = _orderService.GetAll().Count();
            return View();
        }

        // Categories
        public IActionResult Categories() => View(_categoryService.GetAll());
        public IActionResult AddCategory() => View();

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Categories");
        }

        public IActionResult EditCategory(int id) => View(_categoryService.GetById(id));

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            _categoryService.Update(category);
            return RedirectToAction("Categories");
        }

        public IActionResult DeleteCategory(int id)
        {
            _categoryService.Delete(id);
            return RedirectToAction("Categories");
        }

        // Products
        public IActionResult Products() => View(_productService.GetAll());
        public IActionResult AddProduct()
        {
            ViewBag.Subcategories = _subcategoryService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productService.Add(product);
            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(int id)
        {
            ViewBag.Subcategories = _subcategoryService.GetAll();
            return View(_productService.GetById(id));
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            _productService.Update(product);
            return RedirectToAction("Products");
        }

        public IActionResult DeleteProduct(int id)
        {
            _productService.Delete(id);
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