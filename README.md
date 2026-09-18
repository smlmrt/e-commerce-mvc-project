# ECommerceWeb – E-Ticaret ve Dinamik Sepet Sistemi

Ürünlerin listelendiği, yeni ürünlerin görselleriyle eklenebildiği ve Session tabanlı alışveriş sepeti bulunan bir ASP.NET Core MVC e-ticaret projesi.

## Özellikler

- Ürünleri kategorileriyle birlikte ana sayfada listeleme
- Ürün detay sayfası
- Görsel yüklemeli ürün ekleme formu (`wwwroot/images` altına benzersiz adla kaydedilir)
- Session tabanlı sepet: ürün ekleme, adet artırma, ürün çıkarma, toplam tutar
- Menüde sepetteki ürün sayısını gösteren **ViewComponent** (`CartSummary`)
- Session'a nesne yazıp okumak için JSON extension metotları
- İlk çalıştırmada varsayılan kategorilerin (Elektronik, Giyim, Kitap) otomatik eklenmesi

## Teknolojiler

- .NET 10 / ASP.NET Core MVC
- Entity Framework Core 10 (SQLite)
- ASP.NET Core Session
- Bootstrap

## Veri Modeli

- **Category:** `Id`, `Name`, `Products`
- **Product:** `Id`, `Title`, `Price`, `Description`, `ImageUrl`, `CategoryId`
- **CartItem** (Session'da tutulur): `ProductId`, `ProductTitle`, `Price`, `Quantity`, `ImageUrl`, `TotalPrice`

## Sayfalar

| Adres | Açıklama |
|-------|----------|
| `/` | Ürün listesi |
| `/Product/Details/{id}` | Ürün detayı |
| `/Product/Create` | Yeni ürün ekleme |
| `/Cart` | Sepet |
| `/Cart/AddToCart/{id}` | Ürünü sepete ekler |
| `/Cart/RemoveFromCart/{id}` | Ürünü sepetten çıkarır |

## Kurulum ve Çalıştırma

```bash
git clone https://github.com/smlmrt/e-commerce-mvc-project.git
cd e-commerce-mvc-project
dotnet restore
dotnet ef database update
dotnet run
```

Uygulama `http://localhost:5002` adresinde açılır.

## Proje Yapısı

```
ECommerceWeb/
├── Controllers/
│   ├── HomeController.cs
│   ├── ProductController.cs
│   └── CartController.cs
├── Data/AppDbContext.cs
├── Extensions/SessionExtensions.cs
├── Models/                  # Product, Category, CartItem
├── ViewComponents/CartSummaryViewComponent.cs
├── Views/
├── wwwroot/images/          # Yüklenen ürün görselleri
└── Program.cs
```
