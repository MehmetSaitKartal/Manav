using System.Data;



namespace Manav_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Manav manav = new Manav();
            manav.ManavUrunAl();
            manav.MusteriHizmet();
        }
    }

    class Toptanci
    {
        public List<Urun> MeyveListe { get; set; }
        public List<Urun> SebzeListe { get; set; }

        public Toptanci()
        {
            MeyveListe = new List<Urun>
            {
                new Urun("Elma", 10, 50),
                new Urun("Armut", 15, 40),
                new Urun("Çilek", 25, 30),
                new Urun("Muz", 30, 20),
                new Urun("Portakal", 15, 50)
            };
            SebzeListe = new List<Urun>
            {
                new Urun("Domates", 10, 100),
                new Urun("Patlıcan", 15, 80),
                new Urun("Kabak", 10, 60),
                new Urun("Biber", 15, 70),
                new Urun("Havuç", 15, 90)
            };
        }

        public Urun Satis(List<Urun> urunListesi, string urunAdi, int urunKilosu)
        {
            foreach (var urun in urunListesi)
            {
                if (urun.Ad == urunAdi && urun.Kilo >= urunKilosu)
                {
                    urun.Kilo -= urunKilosu;
                    return new Urun(urun.Ad, urun.Fiyat, urunKilosu);
                }
            }
            return null;
        }
    }

    class Manav
    {
        private Toptanci toptanci;
        public List<Urun> Stok { get; set; }

        public Manav()
        {
            toptanci = new Toptanci();
            Stok = new List<Urun>();
        }

        public void ManavUrunAl()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Meyve mi Sebze mi? (1 - Meyve, 2 - Sebze, 0 - Çıkış)");
                int secim = Convert.ToInt32(Console.ReadLine());

                if (secim == 1)
                {
                    ManavUrunAlma(toptanci.MeyveListe);
                }
                else if (secim == 2)
                {
                    ManavUrunAlma(toptanci.SebzeListe);
                }
                else if (secim == 0)
                {
                    Console.WriteLine("Toptancıdan alım tamamlandı.");
                    break;
                }
                else
                {
                    Console.WriteLine("Yanlış Tuş Girdiniz, Tekrar Deneyiniz");
                    Thread.Sleep(1000);
                }
            }
        }

        private void ManavUrunAlma(List<Urun> urunListesi)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mevcut Ürünler:");
                for (int i = 0; i < urunListesi.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {urunListesi[i].Ad} Fiyat: {urunListesi[i].Fiyat} TL/Kg, Kalan: {urunListesi[i].Kilo} kg");
                }
                Console.WriteLine("Hangi ürünü almak istersiniz?");
                int urunSecimi = Convert.ToInt32(Console.ReadLine()) - 1;

                if (urunSecimi < 0 || urunSecimi >= urunListesi.Count)
                {
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyiniz.");
                    Thread.Sleep(1000);
                    continue;
                }

                Urun secilenUrun = urunListesi[urunSecimi];

                Console.WriteLine("Kaç Kilo Almak İstersiniz?");
                int kilo = Convert.ToInt32(Console.ReadLine());

                if (kilo > secilenUrun.Kilo)
                {
                    Console.WriteLine("Yeterli stok yok. Mevcut stok: " + secilenUrun.Kilo + " kg");
                    Thread.Sleep(1000);
                    continue;
                }

                Urun alinanUrun = toptanci.Satis(urunListesi, secilenUrun.Ad, kilo);
                if (alinanUrun != null)
                {
                    var stoktakiUrun = Stok.Find(u => u.Ad == alinanUrun.Ad);
                    if (stoktakiUrun != null)
                    {
                        stoktakiUrun.Kilo += alinanUrun.Kilo;
                    }
                    else
                    {
                        Stok.Add(alinanUrun);
                    }
                    Console.WriteLine($"{kilo} Kilo {secilenUrun.Ad} Alındı");
                }

                Console.WriteLine("Başka Bir İsteğiniz Var Mı? (Evet/Hayır)");
                string cevap = Console.ReadLine().ToLower();
                if (cevap == "hayır")
                {
                    Console.WriteLine("İyi Günler");
                    break;
                }
            }
        }

        public void MusteriHizmet()
        {
            List<Urun> alinanUrunlar = new List<Urun>();

            while (true)
            {
                Console.WriteLine("Meyve mi Sebze mi? (1 - Meyve, 2 - Sebze, 0 - Çıkış)");
                int secim = Convert.ToInt32(Console.ReadLine());

                if (secim == 0)
                {
                    Console.WriteLine("Manavdan Çıkış Yapılıyor");
                    break;
                }

                Console.WriteLine("Mevcut Stok:");
                for (int i = 0; i < Stok.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {Stok[i].Ad} (Kalan: {Stok[i].Kilo} kg, Fiyat: {Stok[i].Fiyat} TL/kg)");
                }
                Console.WriteLine("Hangi Ürünü Almak İstersiniz?");
                int urunSecimi = Convert.ToInt32(Console.ReadLine()) - 1;

                if (urunSecimi < 0 || urunSecimi >= Stok.Count)
                {
                    Console.WriteLine("Geçersiz seçim. Tekrar deneyiniz.");
                    continue;
                }

                Urun secilenUrun = Stok[urunSecimi];

                Console.WriteLine("Kaç Kilo Almak İstersiniz?");
                int kilo = Convert.ToInt32(Console.ReadLine());

                if (kilo > secilenUrun.Kilo)
                {
                    Console.WriteLine("Yeterli stok yok. Mevcut stok: " + secilenUrun.Kilo + " kg");
                    continue;
                }

                secilenUrun.Kilo -= kilo;
                if (secilenUrun.Kilo == 0)
                {
                    Stok.Remove(secilenUrun);
                }

                alinanUrunlar.Add(new Urun(secilenUrun.Ad, secilenUrun.Fiyat, kilo));
                Console.WriteLine($"{kilo} Kilo {secilenUrun.Ad} Satıldı. Kalan Stok: {secilenUrun.Kilo} kg");

                Console.WriteLine("Başka Bir İsteğiniz Var Mı? (Evet/Hayır)");
                string cevap = Console.ReadLine().ToLower();

                if (cevap == "hayır")
                {
                    Console.WriteLine("Afiyet Olsun! Alınan Ürünler:");
                    foreach (var urun in alinanUrunlar)
                    {
                        Console.WriteLine($"{urun.Ad} - {urun.Kilo} kg, Toplam Fiyat: {urun.Kilo * urun.Fiyat} TL");
                    }
                    break;
                }
            }
        }
    }

    class Urun
    {
        public string Ad { get; private set; }
        public int Fiyat { get; private set; }
        public int Kilo { get; set; }

        public Urun(string ad, int fiyat, int kilo = 0)
        {
            Ad = ad;
            Fiyat = fiyat;
            Kilo = kilo;
        }
    }
}

