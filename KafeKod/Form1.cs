using KafeKod.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KafeKod
{ //Kafekod referances sağ tık ile Kafekod.Data yı referans ekliyoruz iki projeyi biribirine bağlıyoruz.
    public partial class Form1 : Form
    {
        KafeContex db=new KafeContex();
        
        //int masaAdet = 20; -> properties.settings den çektik

        public Form1()
        {           
            //db = new KafeVeri(); //içinde ürün listesi,aktif sipariş listesi,geçmiş sipariş listesi var
            //OrnekVerileriYukle();
            InitializeComponent();
            MasalariOlustur();
            //MasaBul(5).Text = "seni buldum";
            //MasaBul(12).Text = "hello";
        }

           
        private void MasalariOlustur()
        {
            #region ListWiew İmajlarının Hazırlanması
            ImageList il = new ImageList();
            il.Images.Add("bos", Properties.Resources.bos);
            il.Images.Add("dolu", Properties.Resources.dolu);
            il.ImageSize = new Size(64, 64);
            lvwMasalar.LargeImageList = il;
            #endregion

            lvwMasalar.Items.Clear();
            ListViewItem lvi;

            for (int i = 1; i <= Properties.Settings.Default.MasaAdet; i++)
            {
                lvi = new ListViewItem("Masa" + i);
                Siparis sip = db.Siparisler.FirstOrDefault(x => x.MasaNo == i && x.Durum == SiparisDurum.Aktif);

                if (sip == null)
                {
                    lvi.Tag = i; //tag int varsa kapalı yoksa masa doludur tag de siparis saklıdır
                    lvi.ImageKey = "bos"; //bos olan image key çağırır onu yazar
                }
                else
                {
                    lvi.Tag = sip;
                    lvi.ImageKey = "dolu";
                }
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
                if (!(lvi.Tag is SiparisDetay)) //is tür kontrolü 
                {
                    sip = new Siparis();
                    if (lvi.Tag is Siparis)
                    {
                        sip = (Siparis)lvi.Tag;
                    }
                    else
                    {
                        sip = new Siparis();
                        sip.Durum = SiparisDurum.Aktif;
                        sip.MasaNo = (int)lvi.Tag; //masa noyu koyduk
                        sip.AcilisZamani = DateTime.Now; //acilis zamanini koyduk
                        lvi.Tag = sip; //tag de istediğimiz nesneyi saklayabiliriz
                        db.Siparisler.Add(sip);
                        db.SaveChanges(); //yapılan her değişiklikte kaydetmemiz lazım

                    }
                    //bu noktadan sonra if e de girse else de girse sip dolu

                    //siparis formun oluşma anı
                    SiparisForm frmSiparis = new SiparisForm(db, sip); //yeni forma bağlıyorum içine iki deger veriyorum kafe veri ve siparis  (FORMLAR ARASI VERİ GEÇİŞİ)
                    frmSiparis.MasaTasiniyor += FrmSiparis_MasaTasindi;
                    frmSiparis.ShowDialog();


                    //masadan siparis alınmış aktif değilse masano tag ni int e ceviriyoruz ki boş olduğu görünsün diye sipariş varsa kalsın
                    //masa kapandıysa boşalt
                    if (sip.Durum == SiparisDurum.Odendi || sip.Durum == SiparisDurum.Iptal)
                    {
                        lvi.Tag = sip.MasaNo;
                        lvi.ImageKey = "bos";                       
                    }
                }
            }
        }

        private void FrmSiparis_MasaTasindi(object sender, MasaTasimaEventArgs e)
        {
            //adım 1: eski masayı boşalt
            ListViewItem lviEskiMasa = MasaBul(e.EskiMasaNo);
            lviEskiMasa.Tag=e.EskiMasaNo;
            lviEskiMasa.ImageKey = "bos";

            //adım 2: yeni masaya sipariş koy 
            ListViewItem lviYeniMasa = MasaBul(e.YeniMasaNo);
            lviYeniMasa.Tag = e.TasinanSiparis;
            lviYeniMasa.ImageKey = "dolu";
        }

        private void tsmiGecmisSiparisler_Click(object sender, EventArgs e)
        {
            var frm = new GecmisSiparislerForm(db); //gecmis sipariasler sekmesine basınca diger forma bagladık
            frm.ShowDialog();
        }

        private void tsmiUrunler_Click(object sender, EventArgs e)
        {
            var frm = new UrunlerForm(db);
            frm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Dispose();
        }

        private ListViewItem MasaBul (int masaNo)
        {
            foreach (ListViewItem item in lvwMasalar.Items) 
            {
                if(item.Tag is int && (int) item.Tag ==masaNo) //listview içindeki masaNo yu (Tag içinde saklıyorduk) bulup return ile döndürüyoruz
                {
                    return item;
                }
                else if(item.Tag is Siparis && ((Siparis)item.Tag).MasaNo == masaNo)
                {
                    return item;
                }

            }
            return null;
        }

  

        private void tsmiAyarlar_Click(object sender, EventArgs e)
        {
            var frm = new AyarlarForm();
            DialogResult dr = frm.ShowDialog();

            if (dr==DialogResult.OK)
            {
                MasalariOlustur();
            }
        }
    }
}
