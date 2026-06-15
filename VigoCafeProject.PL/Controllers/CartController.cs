using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return RedirectToAction("Login", "Account");

            var cart = _cartService.GetCartByCustomerId(customerId.Value);
            return View(cart);
        }

        public IActionResult Add(int id)
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return RedirectToAction("Login", "Account");

            _cartService.AddToCart(customerId.Value, id, 1);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult Increase(int id)
        {
            _cartService.IncreaseQuantity(id);
            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int id)
        {
            _cartService.DecreaseQuantity(id);
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            if (customerId == null) return RedirectToAction("Login", "Account");

            _orderService.PlaceOrder(customerId.Value, "");
            return RedirectToAction("Index", "Home");
        }
    }
}