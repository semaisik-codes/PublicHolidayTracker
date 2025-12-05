using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PublicHolidayTracker
{
    // Tatil bilgilerini tutacak kalıp (Class)
    public class Holiday
    {
        public string date { get; set; }
        public string localName { get; set; }
        public string name { get; set; }
        public string countryCode { get; set; }
        public bool @fixed { get; set; }
        public bool global { get; set; }
    }

    internal class Program
    {
        // Tüm tatilleri hafızada tutacağımız liste
        static List<Holiday> tumTatiller = new List<Holiday>();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Veriler API'den çekiliyor, lütfen bekleyiniz...");

            // 2023, 2024 ve 2025 yıllarını çekiyoruz
            await VeriGetir(2023);
            await VeriGetir(2024);
            await VeriGetir(2025);

            Console.Clear(); // Ekranı temizle
            Console.WriteLine("Veriler yüklendi! Toplam tatil sayısı: " + tumTatiller.Count);
            Console.WriteLine("--------------------------------------------------");

            bool devamEdelimMi = true;

            // Menü döngüsü
            while (devamEdelimMi)
            {
                Console.WriteLine("\n===== PublicHolidayTracker =====");
                Console.WriteLine("1. Tatil listesini göster (Yıl seçmeli)");
                Console.WriteLine("2. Tarihe göre tatil ara (gg-aa formatı)");
                Console.WriteLine("3. İsme göre tatil ara");
                Console.WriteLine("4. Tüm tatilleri 3 yıl boyunca göster (2023-2025)");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        YilaGoreListele();
                        break;
                    case "2":
                        TariheGoreAra();
                        break;
                    case "3":
                        IsmeGoreAra();
                        break;
                    case "4":
                        HepsiniListele();
                        break;
                    case "5":
                        devamEdelimMi = false;
                        Console.WriteLine("Çıkış yapılıyor...");
                        break;
                    default:
                        Console.WriteLine("Hatalı seçim, tekrar deneyin.");
                        break;
                }
            }
        }

        // İnternetten veri çeken fonksiyon
        static async Task VeriGetir(int yil)
        {
            string url = $"https://date.nager.at/api/v3/PublicHolidays/{yil}/TR";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonVerisi = await client.GetStringAsync(url);
                    List<Holiday> yilBazliListe = JsonConvert.DeserializeObject<List<Holiday>>(jsonVerisi);
                    tumTatiller.AddRange(yilBazliListe);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata oluştu ({yil}): " + ex.Message);
                }
            }
        }

        // 1. Seçenek: Yıla göre listeleme
        static void YilaGoreListele()
        {
            Console.Write("Hangi yılı listelemek istersiniz? (2023, 2024, 2025): ");
            string girilenYil = Console.ReadLine();

            Console.WriteLine($"\n--- {girilenYil} Yılı Tatilleri ---");

            foreach (Holiday tatil in tumTatiller)
            {
                if (tatil.date.StartsWith(girilenYil))
                {
                    Console.WriteLine($"{tatil.date} - {tatil.localName}");
                }
            }
        }

        // 2. Seçenek: Tarihe göre arama
        static void TariheGoreAra()
        {
            Console.Write("Aranacak tarih (gg-aa şeklinde girin, örn: 29-10): ");
            string arananTarih = Console.ReadLine();

            string[] parcalar = arananTarih.Split('-');

            if (parcalar.Length < 2)
            {
                Console.WriteLine("Hatalı format! Lütfen 29-10 gibi giriniz.");
                return;
            }

            string gun = parcalar[0];
            string ay = parcalar[1];

            // Veritabanındaki tarih formatına (YYYY-AA-GG) uyduruyoruz
            string aranacakFormat = $"-{ay}-{gun}";

            Console.WriteLine("\nBulunan Tatiller:");
            bool bulunduMu = false;

            foreach (Holiday tatil in tumTatiller)
            {
                if (tatil.date.EndsWith(aranacakFormat))
                {
                    Console.WriteLine($"{tatil.date} -> {tatil.localName}");
                    bulunduMu = true;
                }
            }

            if (bulunduMu == false)
            {
                Console.WriteLine("Bu tarihte bir tatil bulunamadı.");
            }
        }

        // 3. Seçenek: İsme göre arama
        static void IsmeGoreAra()
        {
            Console.Write("Tatil adı girin (örn: Cumhuriyet): ");
            string arananKelime = Console.ReadLine().ToLower();

            Console.WriteLine("\nBulunan Tatiller:");

            foreach (Holiday tatil in tumTatiller)
            {
                if (tatil.localName.ToLower().Contains(arananKelime))
                {
                    Console.WriteLine($"{tatil.date} - {tatil.localName}");
                }
            }
        }

        // 4. Seçenek: Hepsini listeleme
        static void HepsiniListele()
        {
            Console.WriteLine("\n--- Tüm Yılların Tatil Listesi (2023-2025) ---");
            foreach (Holiday tatil in tumTatiller)
            {
                Console.WriteLine($"Tarih: {tatil.date} | Tatil: {tatil.localName}");
            }
        }
    }
}