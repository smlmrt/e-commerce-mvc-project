namespace ECommerceWeb.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }

        // Toplam tutar (fiyat x adet)
        public decimal TotalPrice => Price * Quantity;
    }
}