using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeKod.Data
{
    public class SiparisDetay
    {
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }
        public int Adet { get; set; }

        //public decimal Tutar() => Adet * BirimFiyat; Aşağıdaki metodun tek satırlık lambda ile ifade edilme biçimi 

        public decimal Tutar()
        {
            return Adet * BirimFiyat;
        }
        

    }
}
