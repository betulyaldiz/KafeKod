using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeKod.Data
{
   public enum SiparisDurum { Aktif,Odendi,Iptal}
    public class Siparis
    {
        public Siparis() //constructor
        {
            SiparisDetaylar = new List<SiparisDetay>();
        }
        public int MasaNo { get; set; }
        public DateTime?  AcilisZamani { get; set; } // ? nullable 
        public DateTime?  KapanisZamani { get; set; }
        public SiparisDurum Durum { get; set; }
        public List<SiparisDetay> SiparisDetaylar { get; set; } //Bir sipariş birden çok detay içerir ama .. içtiğimiz kola aynı olamaz. Bir takım birden çok taraftarı olur bir kişi bir takım tutabilir.
        public decimal OdenenTutar { get; set; }
        //onetomenu
        public string ToplamTutarTL => string.Format("{0:0.00}₺", ToplamTutar()); // bu bir metod değil property dir. Ödeme tutarı : 9.99₺ gösterimi için
        //public string ToplamTutarTL { get{ string.Format("{0:0.00}₺", ToplamTutar())}};
        public decimal ToplamTutar()
        {
            return SiparisDetaylar.Sum(x => x.Tutar()); //foreach ile liste üzerinde Siparistutar içindeki tutar miktarlarını toplayan kod kısa versiyon
        }
        //private decimal Fiyat(SiparisDetay x)
        //{
        //    return x.Tutar();
        //}
    }
}
