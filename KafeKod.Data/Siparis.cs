using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeKod.Data
{
   public enum SiparisDurum { Aktif,Odendi,Iptal} //default
    [Table("Siparisler")]

    public class Siparis
    {
        public int Id { get; set; }
        public int MasaNo { get; set; }

        public DateTime?  AcilisZamani { get; set; } // ? nullable 

        public DateTime?  KapanisZamani { get; set; }

        public SiparisDurum Durum { get; set; }
        
        public decimal OdenenTutar { get; set; }
       
        public virtual List<SiparisDetay> SiparisDetaylar { get; set; } //Bir sipariş birden çok detay içerir ama .. içtiğimiz kola aynı olamaz. Bir takım birden çok taraftarı olur bir kişi bir takım tutabilir.
               
    }
}
