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
    
    public partial class UrunlerForm : Form
    {
        KafeContex db;
        

        public UrunlerForm(KafeContex kafeVeri)
        {
            db = kafeVeri;
            InitializeComponent();
            dgvUrunler.AutoGenerateColumns = false; //ürün olmasına ragmen hiç sutunu olmayan bir dgv
            //dgvUrunler.DataSource = db.Urunler.OrderBy(x => x.UrunAd).ToList(); //binding list kaldırdık sıralı gelmesi için başlangıçtada Order by sıralaması yapıyoruz
            dgvUrunler.DataSource = new BindingSource(db.Urunler.OrderBy(x => x.UrunAd).ToList(), null);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAd.Text.Trim();

            if (urunAd == "")
            {
                MessageBox.Show("Lütfen bir ürün adı giriniz!");
                return;

            }
            db.Urunler.Add(new Urun
            {
                UrunAd = urunAd,
                BirimFiyat = nudbirimFiyat.Value
            });
            db.SaveChanges();

            dgvUrunler.DataSource = new BindingSource(db.Urunler.OrderBy(x => x.UrunAd).ToList(), null);
        }

        private void dgvUrunler_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Hatalı giriş!");
        }

        private void dgvUrunler_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //UrunAd düzenliyorsa
            if (e.ColumnIndex == 0)
            {
                if (e.FormattedValue.ToString().Trim() == "") //ürün adı boş ise hata mesaj belirtme
                {
                    dgvUrunler.Rows[e.RowIndex].ErrorText = "Ürün adı boş geçilmez";
                    e.Cancel = true;

                }
                else
                {
                    dgvUrunler.Rows[e.RowIndex].ErrorText = "";
                }
            }
            db.SaveChanges();
        }

        private void dgvUrunler_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Urun urun = (Urun)e.Row.DataBoundItem;

            if (urun.SiparisDetaylar.Count>0)
            {
                MessageBox.Show("Ürün geçmiş sipariş ile bağlantılı olduğu için silinemez!");
                e.Cancel = true;
                return;
            }
            db.Urunler.Remove(urun);
            db.SaveChanges();
        }

        private void UrunlerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtUrunAd.Focus(); // dgv deki son değişikliğin kaydedilmesini tetiklemek için
        }

        private void dgvUrunler_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
