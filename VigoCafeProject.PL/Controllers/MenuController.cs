using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MenuController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubcategoryService _subcategoryService;
        private readonly IProductService _productService;

        public MenuController(ICategoryService categoryService,
                              ISubcategoryService subcategoryService,
                              IProductService productService)
        {
            _categoryService = categoryService;
            _subcategoryService = subcategoryService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories);
        }

        public IActionResult Subcategories(int id)
        {
            var category = _categoryService.GetByIdWithSubcategories(id);
            if (category == null) return NotFound();
            return View(category);
        }

        public IActionResult Products(int id, string search)
        {
            var subcategory = _subcategoryService.GetByIdWithProducts(id);
            if (subcategory == null) return NotFound();

            if (!string.IsNullOrEmpty(search))
                ViewBag.Products = _productService.Search(search);
            else
                ViewBag.Products = subcategory.Products;

            return View(subcategory);
        }
    }
}