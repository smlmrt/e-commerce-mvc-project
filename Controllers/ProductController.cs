using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceWeb.Data;
using ECommerceWeb.Models;
using System.IO; // Dosya işlemleri için bu kütüphaneyi ekledik

namespace ECommerceWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Ürün Ekleme Sayfasını Getir
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }


        // GET: /Product/Detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // url'de id gönderilmediyse hata verir
            }

            // Tıklanan id'ye sahip ürün kategori bilgisiyle birlikte veritabanından buluyoruz
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            

            if (product == null)
            {
                return NotFound(); // Eğer veritabanında böyle bir ürün yoksa hata dön
            }

            return View(product); // Ürün bulunduysa detay sayfa görünümüne ürünü gönder
        }


        // POST: Form Gönderildiğinde Çalışacak Metot
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                product.ImageUrl = uniqueFileName;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        
    }
}