using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            ViewBag.FeaturedProducts = _productService.GetFeatured();
            ViewBag.LatestProducts = _productService.GetLatest();
            return View();
        }
    }
}