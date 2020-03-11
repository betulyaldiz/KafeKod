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
        //event tanımlıyoruz
        public event EventHandler<MasaTasimaEventArgs> MasaTasiniyor; // tasınan masadaki verileri diger masaya aktarmak için


        //cift tıklayınca masaya masano çıkıyor
        private KafeContex db;
        private Siparis siparis;
        
        public SiparisForm(KafeContex kafeVeri,Siparis siparis )
        {
            db = kafeVeri;
            this.siparis = siparis;
            InitializeComponent();
            dgvSiparisDetaylari.AutoGenerateColumns = false;
            MasaNolariYükle();
            masanoGuncelle();
            TutarGuncelle();
            cboUrun.DataSource = db.Urunler.ToList(); //.OrderBy(x=> x.UrunAd).ToList(); //sıralama yapar
            //cboUrun.SelectedItem = null; boş gelmesini istiyorsak
            dgvSiparisDetaylari.DataSource = siparis.SiparisDetaylar.ToList();

        }

        private void MasaNolariYükle()
        {
            cboMasaNo.Items.Clear();
            for(int i = 1; i <= Properties.Settings.Default.MasaAdet; i++)
            {
                if (!db.Siparisler.Any(x=>x.MasaNo==i && x.Durum==SiparisDurum.Aktif)) //db deki aktif siparişler içerisinde masano su i olan var mı? (dolu olan)
                {
                    cboMasaNo.Items.Add(i);
                }
            }
        }

        private void TutarGuncelle()
        {
            lblTutar.Text =siparis.SiparisDetaylar.Sum(x => x.Adet*x.BirimFiyat).ToString("0.00")+"₺";
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
                UrunId = seciliUrun.Id,
                UrunAd = seciliUrun.UrunAd,
                BirimFiyat = seciliUrun.BirimFiyat,
                Adet = (int)nudAdet.Value
            };
            siparis.SiparisDetaylar.Add(sd); //blSiparisDetaylar= siparis.SiparisDetaylar
            //dgvSiparisDetaylari.DataSource = null;
            //dgvSiparisDetaylari.DataSource = siparis.SiparisDetaylar;
            //cboUrun.SelectedItem = null;
            db.SaveChanges();
            dgvSiparisDetaylari.DataSource = null;
            dgvSiparisDetaylari.DataSource = siparis.SiparisDetaylar;
            cboUrun.SelectedIndex = 0;
            nudAdet.Value = 1;
            TutarGuncelle();
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
                db.SaveChanges();
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
                siparis.OdenenTutar = siparis.SiparisDetaylar
                                                        .Sum(x=> x.Adet*x.BirimFiyat);
                db.SaveChanges();
                Close();
            }

        }

        private void dgvSiparisDetaylari_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //dgv üzerinde indexlere sağ tıkladığımızda seçmek için
            {
                int rowIndex = dgvSiparisDetaylari.HitTest(e.X, e.Y).RowIndex;

                if(rowIndex>-1)
                {
                    dgvSiparisDetaylari.ClearSelection(); //ilk  etapta mavi seçili alan olmasın diye
                    dgvSiparisDetaylari.Rows[rowIndex].Selected = true;//mouse un sadece tıklandığı yer selected yapılır
                    cmsSiparisDetay.Show(Cursor.Position); // sağ tıklayınca çıkan contextmenustrip çubuğunun pozisyonunu belirleme
                }
                
               // MessageBox.Show("Sağa tıklandı: "+rowIndex);
            }
        }

        private void tsmiSiparisDetaySil_Click(object sender, EventArgs e)
        {
            //secili elemanı sildir
            if(dgvSiparisDetaylari.SelectedRows.Count >0)
            {
                var seciliSatir = dgvSiparisDetaylari.SelectedRows[0];
                var sipDetay = (SiparisDetay)seciliSatir.DataBoundItem;
                siparis.SiparisDetaylar.Remove(sipDetay);
                db.SaveChanges();
            }
            TutarGuncelle();
        }

        private void btnMasaTasi_Click(object sender, EventArgs e)
        {
            if (cboMasaNo.SelectedItem == null)
            {
                MessageBox.Show("Lütfen hedef masa no yu seçiniz!");
            }

            int eskiMasaNo = siparis.MasaNo;
            int hedefMasaNo = (int)cboMasaNo.SelectedItem; //taşıma işlemi gerçekleştiriliyor
          

            if(MasaTasiniyor != null)
            {
                var args = new MasaTasimaEventArgs
                {
                    TasinanSiparis = siparis,
                    EskiMasaNo = eskiMasaNo,
                    YeniMasaNo = hedefMasaNo

                };
                MasaTasiniyor(this, args);
            }
            siparis.MasaNo = hedefMasaNo; //değişiklik yapıldı kaydet
            db.SaveChanges();
            masanoGuncelle();
            MasaNolariYükle();

        }

        private void SiparisForm_Load(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{ //butona bastığında tüm ögeleri gizler
        //    foreach (Control control in Controls)
        //    {
        //        if(control != sender)
        //        {
        //            control.Visible = !control.Visible;
        //        }

        //    }
        //}
    }

    public class MasaTasimaEventArgs : EventArgs
    {
        public Siparis TasinanSiparis { get; set; }
        public int EskiMasaNo { get; set; }
        public int YeniMasaNo { get; set; }
    }
}
