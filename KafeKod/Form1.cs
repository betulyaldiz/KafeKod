using KafeKod.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeKod
{ //Kafekod referances sağ tık ile Kafekod.Data yı referans ekliyoruz iki projeyi biribirine bağlıyoruz.
    public partial class Form1 : Form
    {
        KafeVeri db;
        int masaAdet = 20;
        
        public Form1()
        {
            db = new KafeVeri(); //içinde ürün listesi,aktif sipariş listesi,geçmiş sipariş listesi var
            OrnekVerileriYukle();
            InitializeComponent();
            MasalariOlustur();
        }

        private void OrnekVerileriYukle()
        {
            db.Urunler = new List<Urun>
            {
                new Urun{ UrunAd="Kola", BirimFiyat=6.99m},
                new Urun{ UrunAd="Çay", BirimFiyat=3.99m},
            };
        }

        private void MasalariOlustur()
        {
            #region ListWiew İmajlarının Hazırlanması
            ImageList il = new ImageList();
            il.Images.Add("bos", Properties.Resources.bos);
            il.Images.Add("dolu", Properties.Resources.dolu);
            il.ImageSize = new Size(64,64);
            lvwMasalar.LargeImageList = il;
            #endregion
            ListViewItem lvi;
            for (int i = 1; i <= masaAdet ; i++)
            {
                lvi = new ListViewItem("Masa" + i);
                lvi.Tag = i; //tag int varsa kapalı yoksa masa doludur tag de siparis saklıdır
                lvi.ImageKey = "bos"; //bos olan image key çağırır onu yazar
                lvwMasalar.Items.Add(lvi);

            }
        }

        private void lvwMasalar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var lvi = lvwMasalar.SelectedItems[0];
                lvi.ImageKey = "dolu";
                Siparis sip;
                //masa dolu ise olanı al, boş ise sipariş oluştur
                if(!(lvi.Tag is SiparisDetay)) //is tür kontrolü 
                {
                    sip = new Siparis();
                    if(lvi.Tag is Siparis)
                    {
                        sip = (Siparis)lvi.Tag;
                    }
                    else
                    {
                        sip = new Siparis();
                        sip.MasaNo = (int)lvi.Tag; //masa noyu koyduk
                        sip.AcilisZamani = DateTime.Now; //acilis zamanini koyduk
                        lvi.Tag = sip; //tag de istediğimiz nesneyi saklayabiliriz
                        db.AktifSiparisler.Add(sip);

                    }
                    //bu noktadan sonra if e de girse else de girse sip dolu

                    SiparisForm frmSiparis = new SiparisForm(db,sip); //yeni forma bağlıyorum içine iki deger veriyorum kafe veri ve siparis  (FORMLAR ARASI VERİ GEÇİŞİ)
                    frmSiparis.ShowDialog();


                    //masadan siparis alınmış aktif değilse masano tag ni int e ceviriyoruz ki boş olduğu görünsün diye
                    if (sip.Durum == SiparisDurum.Odendi || sip.Durum == SiparisDurum.Iptal)
                    {
                        lvi.Tag = sip.MasaNo;
                        lvi.ImageKey = "bos";
                        db.AktifSiparisler.Remove(sip);
                        db.GecmisSiparisler.Add(sip);

                    }
                }
            }
        }

        private void tsmiGecmisSiparisler_Click(object sender, EventArgs e)
        {
            var frm = new GecmisSiparislerForm(db); //gecmis sipariasler sekmesine basınca diger forma bagladık
            frm.ShowDialog();
        }
    }
}
