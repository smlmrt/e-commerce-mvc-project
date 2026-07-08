using ECommerceWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor(); // Session'a her yerden erişebilmek için
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // 20 dakika işlem yapılmazsa sepet silinsin
    options.Cookie.HttpOnly = true; // Güvenlik için cookie'yi dış müdahalelere kapat
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


// --- Otomatik Kategori Ekleme Başlangıcı ---
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ECommerceWeb.Data.AppDbContext>();
    if (!context.Categories.Any()) // Eğer hiç kategori yoksa
    {
        context.Categories.AddRange(
            new ECommerceWeb.Models.Category { Name = "Elektronik" },
            new ECommerceWeb.Models.Category { Name = "Giyim" },
            new ECommerceWeb.Models.Category { Name = "Kitap" }
        );
        context.SaveChanges();
    }
}
// --- Otomatik Kategori Ekleme Bitişi ---


app.Run();
