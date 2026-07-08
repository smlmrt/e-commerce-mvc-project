using ECommerceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Veritabanındaki tablolarımızı temsil eden DbSet'ler
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}