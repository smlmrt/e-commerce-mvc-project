using Microsoft.AspNetCore.Mvc;
using ECommerceWeb.Data;
using ECommerceWeb.Models;
using ECommerceWeb.Extensions;

namespace ECommerceWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            // Session'dan sepet listesini çek, eğer boşsa yeni bir liste oluştur
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            return View(cart);
        }


        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(c => c.ProductId == id);

            if (cartItem == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductTitle = product.Title,
                    Price = product.Price,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl
                });
            }
            else
            {
                cartItem.Quantity++;
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
        }


        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
    }
}