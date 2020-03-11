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
        private BindingList<Urun> blUrunler;

        public UrunlerForm(KafeContex kafeVeri)
        {
            db = kafeVeri;
            InitializeComponent();
            dgvUrunler.AutoGenerateColumns = false; //ürün olmasına ragmen hiç sutunu olmayan bir dgv
            blUrunler = new BindingList<Urun>(db.Urunler);
            dgvUrunler.DataSource = blUrunler;
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string urunAd = txtUrunAd.Text.Trim();

            if (urunAd == "")
            {
                MessageBox.Show("Lütfen bir ürün adı giriniz!");
                return;

            }
            blUrunler.Add(new Urun
            {
                UrunAd = urunAd,
                BirimFiyat = nudbirimFiyat.Value
            });
            db.Urunler.Sort();
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
                    dgvUrunler.Rows[e.RowIndex].ErrorText = "";
            }
        }
    }
}
