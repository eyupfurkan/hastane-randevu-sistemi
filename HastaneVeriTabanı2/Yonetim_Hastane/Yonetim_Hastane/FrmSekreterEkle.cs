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
    public partial class FrmSekreterEkle : Form
    {
        public FrmSekreterEkle()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BtnYoneticiEkle_Click(object sender, EventArgs e)
        {
            if (FrmHastaKayit.TCKontrol(MskTC.Text)) {
                SqlCommand komut2 = new SqlCommand("insert into Tbl_Sekreterler(SekreterTC,SekreterAd,SekreterSoyad,SekreterSifre) values (@p1, @p2, @p3, @p4)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p2", TxtAd.Text);
                komut2.Parameters.AddWithValue("@p3", TxtSoyad.Text);
                komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
                komut2.Parameters.AddWithValue("@p1", MskTC.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Sekreter Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TabloGuncelle();
            }
        }

        private void FrmSekreterEkle_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Sekreterid, SekreterAd, SekreterSoyad, SekreterTC, SekreterSifre from Tbl_Sekreterler", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtSekreterid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void TabloGuncelle()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Sekreterid, SekreterAd, SekreterSoyad, SekreterTC, SekreterSifre from Tbl_Sekreterler", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void btnSekreterSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("delete from Tbl_Sekreterler where Sekreterid=@p1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtSekreterid.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Sekreter Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloGuncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Tbl_Sekreterler set SekreterAd=@p1, SekreterSoyad=@p2, SekreterTC=@p3, SekreterSifre=@p4 where Sekreterid=@p5", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", MskTC.Text);
            komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", txtSekreterid.Text);

            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Sekreter Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            TabloGuncelle();
        }
    }
}
