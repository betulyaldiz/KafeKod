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
    public partial class GecmisSiparislerForm : Form
    {
        KafeContex db;
        public GecmisSiparislerForm(KafeContex kafeVeri)
        {
            db = kafeVeri;
            InitializeComponent();

            dgvSiparisler.DataSource = db.GecmisSiparisler; // verileri getirdik
        }

        private void dgvSiparisler_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvSiparisler.SelectedRows.Count >0) //gecmis siparislerden secili itemin detaylarını siparis detaya aldık
            {
                DataGridViewRow satir = dgvSiparisler.SelectedRows[0];
                Siparis siparis = (Siparis)satir.DataBoundItem;
                dgvSiparisDetaylari.DataSource = siparis.SiparisDetaylar;
            }
        }
    }
}
