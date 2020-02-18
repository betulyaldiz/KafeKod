using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeKod.Data
{
    public class Urun
    {
        //ürün özellikleri properties
        public string UrunAd { get; set; }
        public decimal BirimFiyat { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - {1:0.00}₺",UrunAd,BirimFiyat);
        }

    }
}
