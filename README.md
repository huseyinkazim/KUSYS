# KUSYS - Öğrenci ve Kurs Yönetim Sistemi

KUSYS, Clean Architecture prensiplerine uygun olarak geliştirilen bir **Öğrenci ve Kurs Yönetim Sistemi**dir.

## 📋 Proje Açıklaması

Bu proje, öğrencilerin kurslara kayıt olmasını, yönetici ve eğitmenlerin kurs ve öğrenci yönetimini yapabildiği modern bir web uygulamasıdır. 

Proje, **N-Layer (Katmanlı Mimari)** ve **Clean Architecture** prensipleriyle tasarlanmıştır. Bu sayede kodun bakımını kolaylaştırmak, test edilebilirliği artırmak ve katmanlar arası bağımlılıkları minimize etmek hedeflenmiştir.

## 🛠 Kullanılan Teknolojiler

- **.NET 8**
- **ASP.NET Core Web API**
- **ASP.NET Core MVC** (WebApplication)
- **Entity Framework Core**
- **SQL Server** (veritabanı)
- **AutoMapper**
- **FluentValidation**
- **MediatR** (isteğe bağlı eklenebilir)
- **Repository Pattern**
- **Dependency Injection**

## 🏗 Proje Katmanları

| Katman                  | Açıklama                                      |
|-------------------------|-----------------------------------------------|
| **KUSYS.Model**         | Entity, DTO, ViewModel ve Enum'lar            |
| **KUSYS.Repository**    | Veri erişim katmanı (EF Core DbContext vb.)   |
| **KUSYS.Business**      | İş kuralları ve Domain Logic                  |
| **KUSYS.Service**       | Application Service katmanı                   |
| **KUSYS.Api**           | Web API katmanı                               |
| **KUSYS.WebApplication**| MVC / Razor Pages tabanlı Web Arayüzü        |

## 🚀 Projeyi Çalıştırma

1. Projeyi klonlayın:
   ```bash
   git clone https://github.com/huseyinkazim/KUSYS.git
Visual Studio'da solution'ı açın.
Multiple Startup Projects olarak ayarlayın:
KUSYS.WebApplication ve KUSYS.Api projelerini aynı anda çalıştırın.

KUSYS.WebApplication ve KUSYS.Api içerisindeki appsettings.json dosyalarında ConnectionString'i kendi SQL Server veritabanınıza göre düzenleyin.
Migration'ları uygulayın:BashUpdate-Database(Package Manager Console'da ilgili projeyi seçerek çalıştırın)
Uygulamayı çalıştırın (F5).

📁 Klasör Yapısı
textKUSYS/
├── KUSYS.WebApplication/
├── KUSYS.Api/
├── KUSYS.Service/
├── KUSYS.Business/
├── KUSYS.Repository/
├── KUSYS.Model/
└── KUSYS.sln
✨ Mevcut Özellikler (Geliştirme Aşamasında)

Öğrenci CRUD işlemleri
Kurs CRUD işlemleri
Öğrenci - Kurs Kayıt İşlemleri
Temiz ve Ölçeklenebilir Mimari

📌 Yapılacaklar Listesi

 ASP.NET Identity & Rol Yönetimi
 FluentValidation entegrasyonu
 Global Exception Handling
 Swagger Dokümantasyonu
 Unit ve Integration Testler
 Docker desteği


Geliştirici: Hüseyin Kazım
Başlangıç Tarihi: Nisan 2026
Her türlü öneri ve katkıya açığım! ⭐
