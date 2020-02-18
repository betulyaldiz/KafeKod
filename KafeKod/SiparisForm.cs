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
{
    public partial class SiparisForm : Form
    {
        //cift tıklayınca masaya masano çıkıyor
        private KafeVeri db;
        private Siparis siparis;
        BindingList<SiparisDetay> blSiparisDetaylar;
        public SiparisForm(KafeVeri kafeVeri,Siparis siparis )
        {
            db = kafeVeri;
            this.siparis = siparis;
            blSiparisDetaylar = new BindingList<SiparisDetay>(siparis.SiparisDetaylar);
            InitializeComponent();
            masanoGuncelle();
            tutarGuncelle();
            cboUrun.DataSource = db.Urunler;
            //cboUrun.SelectedItem = null; boş gelmesini istiyorsak
            dgvSiparisDetaylari.DataSource = blSiparisDetaylar;

        }

        private void tutarGuncelle()
        {
            lblTutar.Text = siparis.ToplamTutarTL;
        }

        private void masanoGuncelle()
        {
            Text = "Masa " + siparis.MasaNo;
            lblMasaNo.Text = siparis.MasaNo.ToString("00"); //format verdik
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if(cboUrun.SelectedItem==null)
            {
                MessageBox.Show("Lütfen bir ürün seciniz");
                return;
            }

            Urun seciliUrun = (Urun)cboUrun.SelectedItem;
            var sd = new SiparisDetay
            {
                UrunAd = seciliUrun.UrunAd,
                BirimFiyat = seciliUrun.BirimFiyat,
                Adet = (int)nudAdet.Value
            };
            blSiparisDetaylar.Add(sd); //blSiparisDetaylar= siparis.SiparisDetaylar
            //dgvSiparisDetaylari.DataSource = null;
            //dgvSiparisDetaylari.DataSource = siparis.SiparisDetaylar;
            //cboUrun.SelectedItem = null;
            nudAdet.Value = 1;
            tutarGuncelle();
        }

        private void btnAnasayfa_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSiparisIptal_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("Sipariş iptal edilecektir. Onaylıyor musunuz?","Sipariş İptal Onayı", //1.mesaj 2.başlık
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, //kullanılan ikon
                MessageBoxDefaultButton.Button2); //default secili buton konumu (hayır)
            if (dr == DialogResult.Yes)
            {
                siparis.Durum = SiparisDurum.Iptal;
                siparis.KapanisZamani = DateTime.Now;
                Close();
            }
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("Ödeme alındıysa masanın hesabı kapatılacaktır. Onaylıyor musunuz?", "Masa Hesabı Kapatma Onayı", //1.mesaj 2.başlık
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Warning, //kullanılan ikon
           MessageBoxDefaultButton.Button2); //default secili buton konumu (hayır)
            if (dr == DialogResult.Yes)
            {
                siparis.Durum = SiparisDurum.Odendi;
                siparis.KapanisZamani = DateTime.Now;
                siparis.OdenenTutar = siparis.ToplamTutar();
                Close();
            }

        }
    }
}
