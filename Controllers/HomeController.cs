using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceWeb.Models;
using ECommerceWeb.Data;

namespace ECommerceWeb.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    // Veritabanı bağlantımızı (AppDbContext) içeri alıyoruz
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Ürünleri, ait oldukları kategori bilgisiyle birlikte veritabanından çekiyoruz
        var products = await _context.Products.Include(p => p.Category).ToListAsync();
        
        // Çektiğimiz ürün listesini View'a gönderiyoruz
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}