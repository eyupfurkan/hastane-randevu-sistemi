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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        public string DoktorTCsi;
        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = DoktorTCsi;
            //Doktor Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad,Doktorid from Tbl_Doktorlar where DoktorTC=@d1", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();

            //Doktor Ad, Soyad ve Randevu Listesi
            while (dr.Read())
            {
                String DoktorAd = dr[0].ToString();
                String DoktorSoyad = dr[1].ToString();
                LblAdSoyad.Text = DoktorAd + " " + DoktorSoyad;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select Tbl_Randevular.Randevuid, Tbl_Hastalar.HastaAd, Tbl_Hastalar.HastaSoyad, Tbl_Randevular.RandevuTarih, Tbl_Randevular.RandevuSaat, Tbl_Randevular.RandevuDurum, Tbl_Randevular.Sikayet from Tbl_Randevular, Tbl_Hastalar where Doktorid='" + dr[2].ToString() + "' and RandevuDurum='True' AND Tbl_Randevular.Hastaid = Tbl_Hastalar.Hastaid", bgl.baglanti());
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            bgl.baglanti().Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle dbd = new FrmDoktorBilgiDuzenle();
            dbd.DoktorTCsidir = LblTC.Text;
            dbd.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular d = new FrmDuyurular();
            d.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtRandevuid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnTamamla_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("delete from Tbl_Randevular where Randevuid=@p1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtRandevuid.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Tamamlandı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloGuncelle();
        }

        private void TabloGuncelle()
        {
            LblTC.Text = DoktorTCsi;
            //Doktor Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad,Doktorid from Tbl_Doktorlar where DoktorTC=@d1", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                String DoktorAd = dr[0].ToString();
                String DoktorSoyad = dr[1].ToString();
                LblAdSoyad.Text = DoktorAd + " " + DoktorSoyad;
                DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter("select Randevuid,RandevuTarih, RandevuSaat, RandevuDurum, Sikayet from Tbl_Randevular where Doktorid='" + dr[2].ToString() + "' and RandevuDurum='True'", bgl.baglanti());
                SqlDataAdapter da = new SqlDataAdapter("select Tbl_Randevular.Randevuid, Tbl_Hastalar.HastaAd, Tbl_Hastalar.HastaSoyad, Tbl_Randevular.RandevuTarih, Tbl_Randevular.RandevuSaat, Tbl_Randevular.RandevuDurum, Tbl_Randevular.Sikayet from Tbl_Randevular, Tbl_Hastalar where Doktorid='" + dr[2].ToString() + "' and RandevuDurum='True' AND Tbl_Randevular.Hastaid = Tbl_Hastalar.Hastaid", bgl.baglanti());
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            bgl.baglanti().Close();
        }
    }
}
