namespace ECommerceWeb.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Bir kategorinin birden fazla ürünü olabilir. (Bire-Çok ilişki)
        public List<Product> Products { get; set; } = new List<Product>();
    }
}