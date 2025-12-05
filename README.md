# PublicHolidayTracker
Projenin Amacı kullanıcının statik veri girmesine gerek kalmadan internet üzerinden güncel tatil verilerine ulaşmasını sağlamak ve bu veriler üzerinde yıl, tarih veya isim bazlı arama yapabilmektir.

--Özellikler--
Uygulama açıldığında verileri hafızaya yükler ve aşağıdaki işlemleri yapabilir:

1-Yıla Göre Listeleme: Kullanıcı 2023-2024-2025 yıllarından birini seçer ve o yılın tüm tatillerini görebilir.

2- Tarihe Göre Arama: Kullanıcı 'gg-aa' (Örn: 29-10) formatında giriş yaparak o günün tatil olup olmadığını sorgulayabilir.

3- İsme Göre Arama: 3 yıllık (2023-2025) tüm tatil verileri tek liste halinde görüntüleyebilir.
4-JSON Veri İşleme: API'den gelen veriler 'Newtonsoft.Json' kütüphanesi ile 'Holiday' sınıfına dönüştürülür.

Veriler Nager.Date API servisinden çekilmektedir.
https://date.nager.at/api/v3/PublicHolidays/2023/TR
https://date.nager.at/api/v3/PublicHolidays/2024/TR
https://date.nager.at/api/v3/PublicHolidays/2025/TR
