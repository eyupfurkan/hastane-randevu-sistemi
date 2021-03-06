﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Yonetim_Hastane
{
    public partial class FrmRandevuListesi : Form
    {
        public FrmRandevuListesi()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmRandevuListesi_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Tbl_Randevular.Randevuid, Tbl_Hastalar.HastaAd, Tbl_Hastalar.HastaSoyad, Tbl_Branslar.BransAd, Tbl_Doktorlar.DoktorAd, Tbl_Doktorlar.DoktorSoyad, Tbl_Randevular.RandevuTarih, Tbl_Randevular.RandevuSaat, Tbl_Randevular.RandevuDurum, Tbl_Randevular.Sikayet   FROM Tbl_Randevular, Tbl_Branslar, Tbl_Doktorlar, Tbl_Hastalar WHERE Tbl_Randevular.Bransid = Tbl_Branslar.Bransid AND Tbl_Randevular.Doktorid = Tbl_Doktorlar.Doktorid AND Tbl_Randevular.Hastaid = Tbl_Hastalar.Hastaid ", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public int secilen;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            secilen = dataGridView1.SelectedCells[0].RowIndex;
        }
    }
}
