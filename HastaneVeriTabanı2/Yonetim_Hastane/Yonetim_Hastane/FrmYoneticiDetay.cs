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
    public partial class FrmYoneticiDetay : Form
    {
        public FrmYoneticiDetay()
        {
            InitializeComponent();
        }

        public String YoneticiTC;
        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmYoneticiDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = YoneticiTC;
            SqlCommand komut = new SqlCommand("select YoneticiAd,YoneticiSoyad from Tbl_Yoneticiler where YoneticiTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", YoneticiTC);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmYoneticiBilgiDuzenle fr = new FrmYoneticiBilgiDuzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
        }

        private void BtnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBransPaneli frb = new FrmBransPaneli();
            frb.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi rl = new FrmRandevuListesi();
            rl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd = new FrmDuyurular();
            frd.Show();
        }

        private void linkYoneticiEkle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmYoneticiEkle fr = new FrmYoneticiEkle();
            fr.Show();
        }

        private void linkSekreterEkle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmSekreterEkle fr = new FrmSekreterEkle();
            fr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
