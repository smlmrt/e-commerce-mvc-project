using Microsoft.AspNetCore.Mvc;
using ECommerceWeb.Models;
using ECommerceWeb.Extensions;

namespace ECommerceWeb.ViewComponents
{
    public class CartSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Session'daki sepet listesini çekiyoruz
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
            
            // Sepetteki toplam ürün adetini hesaplıyoruz (Örn: 2 adet X ürünü, 1 adet Y ürünü = Toplam 3 ürün)
            var totalCount = cart.Sum(item => item.Quantity);
            
            // Bu adeti küçük tasarlayacağımız arayüze (View'a) gönderiyoruz
            return View(totalCount);
        }
    }
}