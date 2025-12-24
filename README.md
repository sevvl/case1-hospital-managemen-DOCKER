# Patient Tracker - Hastane Yönetim Sistemi

Tam yýðýn saðlýk yönetim uygulamasý (.NET 8 Backend + Angular 18 Frontend)

## ??? Proje Yapýsý

```
case1/
??? case1/                    # .NET 8 Backend
?   ??? Controllers/          # API endpoints
?   ??? Services/             # Ýþ mantýðý
?   ??? Models/               # Entity modelleri
?   ??? DTOs/                 # Data Transfer Objects
?   ??? Data/                 # Veritabaný context
?   ??? Program.cs            # Uygulama yapýlandýrmasý
?
??? case1-frontend/           # Angular 18 Frontend
    ??? src/
    ?   ??? app/
    ?   ?   ??? core/         # Servisler, guards, interceptors
    ?   ?   ??? features/     # Bileþenler
    ?   ?   ??? app.routes.ts # Rotalar
    ?   ??? main.ts           # Giriþ noktasý
    ?   ??? index.html        # HTML
    ??? angular.json          # Angular CLI ayarlarý
    ??? package.json          # Baðýmlýlýklar
```

## ?? Backend Kurulumu (.NET 8)

### Gereksinimler
- .NET 8 SDK
- PostgreSQL 12+

### Adýmlar

1. **Veritabaný Baðlantýsýný Yapýlandýr**
   
   `case1/appsettings.json` dosyasýnda PostgreSQL baðlantý dizesini güncelle:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Port=5432;Database=PatientTrackerDB;Username=postgres;Password=YOUR_PASSWORD"
   }
   ```

2. **Migration Oluþtur ve Veritabanýný Baþlat**
   ```sh
   cd case1/case1
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

3. **Uygulamayý Çalýþtýr**
   ```sh
   dotnet run
   ```
   
   Backend `https://localhost:5001` adresinde çalýþacak
   Swagger UI: `https://localhost:5001/swagger`

## ?? Frontend Kurulumu (Angular 18)

### Gereksinimler
- Node.js 18+
- npm 9+

### Adýmlar

1. **Baðýmlýlýklarý Yükle**
   ```sh
   cd case1-frontend
   npm install
   ```

2. **Uygulamayý Çalýþtýr**
   ```sh
   npm start
   ```
   
   Frontend `http://localhost:4200` adresinde çalýþacak

3. **Derle**
   ```sh
   npm run build
   ```

## ?? Demo Giriþi

Backend otomatik olarak örnek veriler ekler:

**Doktor Hesaplarý:**
- Kullanýcý: `doctor1` | Þifre: `password123`
- Kullanýcý: `doctor2` | Þifre: `password123`

## ?? API Endpoints

### Kimlik Doðrulama
- `POST /api/auth/login` - Giriþ yap (JWT token döner)

### Hastalar
- `GET /api/patients` - Tüm hastalarý listele
- `GET /api/patients/{id}` - Hastayý detaylý göster
- `POST /api/patients` - Yeni hasta ekle
- `DELETE /api/patients/{id}` - Hastayý sil

### AI Tahminlemesi
- `GET /api/prediction/{patientId}` - Risk tahminlemesi al

## ?? Akýþ Diyagramý

```
1. Kullanýcý login sayfasýna eriþir
   ?
2. Kimlik bilgileri gönderir (doctor1 / password123)
   ?
3. Backend JWT token döner
   ?
4. Token localStorage'da saklanýr
   ?
5. Tüm API istekleri Authorization header'ý ile gönderilir
   ?
6. Hasta listesini görebilir
   ?
7. Hastayý seçerek detaylý bilgilerini ve týbbi kayýtlarýný görebilir
   ?
8. AI tahmini görüntülenir
   ?
9. Yeni hasta ekleyebilir veya hastayý silebilir
```

## ?? Dosya Hiyerarþisi

### Backend (.NET)
```
Controllers/
  ??? AuthController.cs       # Giriþ iþleri
  ??? PatientsController.cs   # Hasta yönetimi
  ??? PredictionController.cs # AI tahminleri

Services/
  ??? IAuthService.cs
  ??? AuthService.cs          # JWT token oluþturma
  ??? IPatientService.cs
  ??? PatientService.cs       # CRUD iþlemleri
  ??? IAiPredictionService.cs
  ??? MockAiPredictionService.cs

Models/
  ??? User.cs
  ??? Patient.cs
  ??? MedicalRecord.cs

DTOs/
  ??? LoginRequest/Response
  ??? PatientDto/DetailDto
  ??? CreatePatientRequest
  ??? AiPredictionResponse

Data/
  ??? AppDbContext.cs         # EF Core context
  ??? DbInitializer.cs        # Örnek veriler
```

### Frontend (Angular)
```
core/
  ??? guards/
  ?   ??? auth.guard.ts
  ??? interceptors/
  ?   ??? jwt.interceptor.ts
  ??? services/
  ?   ??? auth.service.ts
  ?   ??? patient.service.ts
  ?   ??? ai.service.ts
  ??? models/
      ??? user.model.ts
      ??? patient.model.ts
      ??? prediction.model.ts

features/
  ??? auth/
  ?   ??? login/
  ?       ??? login.component.ts/html/css
  ??? patients/
      ??? patient-list/
      ??? patient-detail/
      ??? patient-create/
```

## ?? Güvenlik Özellikleri

1. **JWT Kimlik Doðrulamasý**
   - Token 24 saat geçerlidir
   - Tüm API endpoints korunur (AuthController/login hariç)

2. **Þifre Karma**
   - HMACSHA256 kullanarak þifreler saklanýr

3. **CORS Ayarlarý**
   - Angular (localhost:4200) için izin verilir

4. **JWT Interceptor**
   - Otomatik olarak Authorization header eklenir

5. **Auth Guard**
   - Korunan rotalar giriþ olmadan eriþilemiyor

## ?? Test Akýþý

### 1. Giriþ Testi
```
1. http://localhost:4200 ziyaret et
2. doctor1 / password123 gir
3. Token localStorage'da saklanmalý
```

### 2. Hasta Listesi
```
1. Giriþ baþarýlý olduktan sonra /patients sayfasý görünmelidir
2. Örnek hastalar listelenir (Ahmet Yýldýz, Fatma Kaya, Mehmet Demir)
```

### 3. Hasta Detaylarý
```
1. Bir hastaya týkla
2. Týbbi kayýtlarý görüntüle
3. AI tahminlemesini gör
```

### 4. Yeni Hasta Ekle
```
1. "+ Yeni Hasta" butonuna týkla
2. Formu doldur (ad, soyadý, doðum tarihi)
3. Kaydet
4. Yeni hasta listeye eklenir
```

### 5. Hasta Sil
```
1. Hastanýn yanýndaki "Sil" butonuna týkla
2. Onay ver
3. Hasta listeden çýkar
```

## ??? Swagger/Postman Testi

Backend çalýþýrken Swagger UI'yi açýn:
```
https://localhost:5001/swagger
```

### 1. Giriþ
```
POST /api/auth/login
Body:
{
  "username": "doctor1",
  "password": "password123"
}

Response:
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "username": "doctor1",
  "role": "Doctor"
}
```

### 2. Hasta Listesi (Þu token'ý Authorization header'ýnda gönder)
```
GET /api/patients
Authorization: Bearer {token}
```

## ?? Notlar

- AI tahminlemesi þu anda mock veriler döndürür (sahte)
- Veritabaný otomatik migrate edilir (Startup'ta)
- Örnek veriler DbInitializer tarafýndan eklenir

## ?? Sorun Giderme

### Backend Hatasý: "EF Core Migration hata"
```sh
cd case1/case1
dotnet ef migrations remove
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Frontend Hatasý: "API baðlantý hatasý"
- Backend çalýþýyor mu? (`dotnet run`)
- API URL'i `src/app/core/services/` dosyalarýnda doðru mu?
- CORS ayarý `Program.cs`'de yapýlmýþ mý?

### Port Çakýþmasý
```
Backend: appsettings.json ? "Urls"
Frontend: angular.json ? "serve"
```

## ?? Kaynaklar

- [.NET 8 Docs](https://learn.microsoft.com/en-us/dotnet/)
- [Angular 18 Docs](https://angular.io/docs)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [JWT Tokens](https://jwt.io/)

## ????? Geliþtirici Notlarý

Bu proje 2-3 günlük bir case study olarak tasarlandý. Aþaðýdakiler ek olarak eklenebilir:

- [ ] Birim testleri
- [ ] E2E testleri
- [ ] Docker containerization
- [ ] GitHub Actions CI/CD
- [ ] Gerçek AI/ML modeli integrasyon
- [ ] Dosya yükleme (hasta resimleri)
- [ ] Pagination ve arama
- [ ] Geliþmiþ UI/Material Design

## ?? Lisans

MIT
