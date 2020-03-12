namespace KafeKod
{
    partial class UrunlerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudbirimFiyat = new System.Windows.Forms.NumericUpDown();
            this.btnEkle = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUrunler = new System.Windows.Forms.DataGridView();
            this.txtUrunAd = new System.Windows.Forms.TextBox();
            this.clmUrunAd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmBirimFiyat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nudbirimFiyat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).BeginInit();
            this.SuspendLayout();
            // 
            // nudbirimFiyat
            // 
            this.nudbirimFiyat.DecimalPlaces = 2;
            this.nudbirimFiyat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.nudbirimFiyat.Location = new System.Drawing.Point(174, 47);
            this.nudbirimFiyat.Name = "nudbirimFiyat";
            this.nudbirimFiyat.Size = new System.Drawing.Size(120, 21);
            this.nudbirimFiyat.TabIndex = 11;
            // 
            // btnEkle
            // 
            this.btnEkle.Font = new System.Drawing.Font("Microsoft YaHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnEkle.Location = new System.Drawing.Point(319, 47);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(75, 25);
            this.btnEkle.TabIndex = 9;
            this.btnEkle.Text = "EKLE";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(170, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Birim Fiyat (₺)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(27, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ürün Adı";
            // 
            // dgvUrunler
            // 
            this.dgvUrunler.AllowUserToAddRows = false;
            this.dgvUrunler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUrunler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUrunler.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmUrunAd,
            this.clmBirimFiyat,
            this.Column1});
            this.dgvUrunler.Location = new System.Drawing.Point(30, 91);
            this.dgvUrunler.Name = "dgvUrunler";
            this.dgvUrunler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUrunler.Size = new System.Drawing.Size(365, 350);
            this.dgvUrunler.TabIndex = 12;
            this.dgvUrunler.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvUrunler_CellValidating);
            this.dgvUrunler.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUrunler_CellValueChanged);
            this.dgvUrunler.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvUrunler_DataError);
            this.dgvUrunler.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvUrunler_UserDeletingRow);
            // 
            // txtUrunAd
            // 
            this.txtUrunAd.Location = new System.Drawing.Point(31, 49);
            this.txtUrunAd.Name = "txtUrunAd";
            this.txtUrunAd.Size = new System.Drawing.Size(119, 20);
            this.txtUrunAd.TabIndex = 13;
            // 
            // clmUrunAd
            // 
            this.clmUrunAd.DataPropertyName = "UrunAd";
            this.clmUrunAd.HeaderText = "Ürün Adı";
            this.clmUrunAd.Name = "clmUrunAd";
            this.clmUrunAd.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmBirimFiyat
            // 
            this.clmBirimFiyat.DataPropertyName = "BirimFiyat";
            this.clmBirimFiyat.HeaderText = "Birim Fiyat";
            this.clmBirimFiyat.Name = "clmBirimFiyat";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "StoktaYok";
            this.Column1.HeaderText = "Stokta Yok";
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // UrunlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 474);
            this.Controls.Add(this.txtUrunAd);
            this.Controls.Add(this.dgvUrunler);
            this.Controls.Add(this.nudbirimFiyat);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(446, 513);
            this.Name = "UrunlerForm";
            this.Text = "UrunlerForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UrunlerForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.nudbirimFiyat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudbirimFiyat;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvUrunler;
        private System.Windows.Forms.TextBox txtUrunAd;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUrunAd;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmBirimFiyat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
    }
}