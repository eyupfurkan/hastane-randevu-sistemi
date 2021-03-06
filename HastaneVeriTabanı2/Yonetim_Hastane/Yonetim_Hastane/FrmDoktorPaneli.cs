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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //Doktorları Datagridview2'e aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select Tbl_Doktorlar.Doktorid, Tbl_Doktorlar.DoktorAd, Tbl_Doktorlar.DoktorSoyad, Tbl_Branslar.BransAd, Tbl_Doktorlar.DoktorTC, Tbl_Doktorlar.DoktorSifre from Tbl_Doktorlar, Tbl_Branslar WHERE Tbl_Doktorlar.Bransid = Tbl_Branslar.Bransid", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Branşları Combobox'a aktarma
            SqlCommand komut1 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                CmbBrans.Items.Add(dr1[0]);
            }
            bgl.baglanti().Close();
        }

        private void TabloGuncelle()
        {
            //Tablo güncelleme
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select Tbl_Doktorlar.Doktorid, Tbl_Doktorlar.DoktorAd, Tbl_Doktorlar.DoktorSoyad, Tbl_Branslar.BransAd, Tbl_Doktorlar.DoktorTC, Tbl_Doktorlar.DoktorSifre from Tbl_Doktorlar, Tbl_Branslar WHERE Tbl_Doktorlar.Bransid = Tbl_Branslar.Bransid", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (FrmHastaKayit.TCKontrol(MskTC.Text)) {
                SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar(DoktorAd,DoktorSoyad,Bransid,DoktorTC,DoktorSifre) values(@d1,@d2,(SELECT Bransid FROM Tbl_Branslar WHERE BransAd='" + CmbBrans.Text + "'),@d4,@d5)", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", TxtAd.Text);
                komut.Parameters.AddWithValue("@d2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@d4", MskTC.Text);
                komut.Parameters.AddWithValue("@d5", TxtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Doktor eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TabloGuncelle();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtDoktorid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("DELETE FROM Tbl_Randevular WHERE Doktorid='" + TxtDoktorid.Text + "'", bgl.baglanti());
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();

            SqlCommand komut = new SqlCommand("DELETE FROM Tbl_Doktorlar WHERE Doktorid='"+ TxtDoktorid.Text + "'", bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor kaydı silindi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            TabloGuncelle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (FrmHastaKayit.TCKontrol(MskTC.Text))
            {
                SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@d2, DoktorSoyad=@d3, Bransid=(SELECT Bransid FROM Tbl_Branslar WHERE BransAd=@d4), DoktorTC=@d5, DoktorSifre=@d6 where Doktorid=@d1", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", TxtDoktorid.Text);
                komut.Parameters.AddWithValue("@d2", TxtAd.Text);
                komut.Parameters.AddWithValue("@d3", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@d4", CmbBrans.Text);
                komut.Parameters.AddWithValue("@d5", MskTC.Text);
                komut.Parameters.AddWithValue("@d6", TxtSifre.Text);

                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Doktor bilgisi güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                TabloGuncelle();
            }
        }
    }
}
