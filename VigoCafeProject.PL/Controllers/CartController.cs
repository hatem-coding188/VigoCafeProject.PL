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

        [Route("Cart/Increase/{cartId}/{productId}")]
        public IActionResult Increase(int cartId, int productId)
        {
            _cartService.IncreaseQuantity(cartId, productId);
            return RedirectToAction("Index");
        }

        [Route("Cart/Decrease/{cartId}/{productId}")]
        public IActionResult Decrease(int cartId, int productId)
        {
            _cartService.DecreaseQuantity(cartId, productId);
            return RedirectToAction("Index");
        }

        [Route("Cart/Remove/{cartId}/{productId}")]
        public IActionResult Remove(int cartId, int productId)
        {
            _cartService.RemoveFromCart(cartId, productId);
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