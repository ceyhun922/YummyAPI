🍽️ YummyProject

Enterprise-grade .NET API + MVC Admin Platform

YummyProject, modern .NET teknolojileriyle geliştirilmiş, API + MVC UI mimarisine sahip, restoran ve kurumsal web siteleri için tasarlanmış ölçeklenebilir ve profesyonel bir yönetim platformudur.
Proje, güçlü bir backend altyapısı ile premium tasarıma sahip bir admin panelini birleştirir.

🚀 Genel Bakış

Backend: ASP.NET Core Web API (RESTful)

Frontend: ASP.NET Core MVC (Razor)

Mimari: Layered Architecture (API ↔ UI)

Tasarım: Premium Glass / Nebula UI

Odak: Yönetilebilirlik, genişletilebilirlik ve temiz kod

📚 İçindekiler

Mimari Yapı

Teknoloji Yığını

Proje Dizini

Kurulum

Uygulamayı Çalıştırma

Ortam Ayarları

API Modülleri

UI (Admin Panel) Modülleri

Dosya Yükleme Mekanizması

Örnek İş Akışları

Hata Ayıklama Rehberi

Gelecek Planları

Katkı

Lisans

🏗️ Mimari Yapı

YummyProject iki ana katmandan oluşur:

1️⃣ YummyAPI (Backend – Web API)

RESTful endpoint’ler

Entity / DTO ayrımı

EF Core ile veri erişimi

Görsel yükleme servisi (/api/FileImage)

Swagger UI ile test edilebilir yapı

2️⃣ YummyUI (Frontend – MVC / Razor)

Admin panel ve site arayüzü

HttpClientFactory ile API iletişimi

Canlı önizleme (Live Preview) destekli Create / Update sayfaları

Premium glassmorphism tasarım dili

🔄 İletişim Şeması
Client (Browser)
   ↓
YummyUI (ASP.NET MVC - Razor)
   ↓ HttpClient
YummyAPI (ASP.NET Web API)
   ↓
SQL Server + Static Files (/images)

🧰 Teknoloji Yığını
Katman	Teknoloji
Runtime	.NET 8.0
Backend	ASP.NET Core Web API
Frontend	ASP.NET Core MVC (Razor)
ORM	Entity Framework Core
JSON	Newtonsoft.Json & System.Text.Json
UI	Bootstrap + Custom CSS (Glass UI)
Icons	FontAwesome
Alerts	SweetAlert2 / Toast
Database	SQL Server
📂 Proje Dizini
YummyProject/
│
├── YummyAPI/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Entities/
│   ├── wwwroot/
│   │   └── images/        # Yüklenen görseller
│   └── Program.cs
│
├── YummyUI/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Views/
│   │   ├── Testimonial/
│   │   ├── Message/
│   │   └── ...
│   ├── wwwroot/
│   └── Program.cs
│
└── README.md

⚙️ Kurulum
Gereksinimler

.NET SDK 8.0+

SQL Server

Visual Studio / VS Code

Veritabanı

Connection string appsettings.json üzerinden ayarlanır

EF Core migration yapısı desteklenir