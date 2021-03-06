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
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void button1_Click(object sender, EventArgs e)
        {
            String secim = comboBox1.Text;

            if( secim.Equals("Hasta") )
            {
                SqlCommand komut = new SqlCommand("select * from Tbl_Hastalar where HastaTC=@p1 and HastaSifre=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmHastaDetay fr = new FrmHastaDetay();
                    fr.tc = textBox1.Text;
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC Kimlik No ya da şifre hatalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            } else if( secim.Equals("Doktor") )
            {
                SqlCommand komut = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@d1 and DoktorSifre=@d2", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", textBox1.Text);
                komut.Parameters.AddWithValue("@d2", textBox2.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmDoktorDetay fr = new FrmDoktorDetay();
                    fr.DoktorTCsi = textBox1.Text;
                    fr.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC Kimlik No ya da şifre hatalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            } else if( secim.Equals("Sekreter") )
            {
                SqlCommand komut = new SqlCommand("select * from Tbl_Sekreterler where SekreterTC=@p1 and SekreterSifre=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmSekreterDetay frs = new FrmSekreterDetay();
                    frs.SekreterTC = textBox1.Text;
                    frs.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC Kimlik No ya da şifre hatalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            } else if( secim.Equals("Yönetici") )
            {
                SqlCommand komut = new SqlCommand("select * from Tbl_Yoneticiler where YoneticiTC=@p1 and YoneticiSifre=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmYoneticiDetay frs = new FrmYoneticiDetay();
                    frs.YoneticiTC = textBox1.Text;
                    frs.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC Kimlik No ya da şifre hatalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            } else if( secim.Equals("Müşteri Hizmetleri") )
            {
                SqlCommand komut = new SqlCommand("select * from Tbl_Musteri_Hizmetleri where MusteriHizmetleriTC=@p1 and MusteriHizmetleriSifre=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", textBox2.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmMusteriHizmetleri frs = new FrmMusteriHizmetleri();
                    frs.MusteriHizmetleriTC = textBox1.Text;
                    frs.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("TC Kimlik No ya da şifre hatalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                bgl.baglanti().Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmHastaKayit fr = new FrmHastaKayit();
            fr.Show();
        }
    }
}
