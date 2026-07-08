using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün başlığı zorunludur.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fiyat zorunludur.")]
        public decimal Price { get; set; }

        public string? Description { get; set; }
        
        // Kullanıcının yükleyeceği fotoğrafın sunucudaki (wwwroot) adını tutacak
        public string? ImageUrl { get; set; }

        // FK (ForeignKey) ürünün hangi kategoriye ait olduğunu belirtir.
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}