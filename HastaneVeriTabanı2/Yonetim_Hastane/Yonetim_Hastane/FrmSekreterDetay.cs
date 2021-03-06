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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        public string SekreterTC;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = SekreterTC;
            //Sekreter AdSoyad Çekme
            SqlCommand komut = new SqlCommand("select SekreterAd,SekreterSoyad from Tbl_Sekreterler where SekreterTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr1 = komut.ExecuteReader();
            while (dr1.Read())
            {
                LblAdSoyad.Text = dr1[0].ToString() + " " + dr1[1].ToString();
            }
            bgl.baglanti().Close();

            //Randevuları Datagridview1'e aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Tbl_Randevular.Randevuid, Tbl_Doktorlar.DoktorAd, Tbl_Doktorlar.DoktorSoyad, Tbl_Branslar.BransAd, Tbl_Randevular.RandevuTarih, Tbl_Randevular.RandevuSaat, Tbl_Randevular.RandevuDurum, Tbl_Randevular.Sikayet from Tbl_Randevular, Tbl_Doktorlar, Tbl_Branslar WHERE Tbl_Randevular.Doktorid = Tbl_Doktorlar.Doktorid AND Tbl_Randevular.Bransid = Tbl_Branslar.Bransid", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşı Combobox'a aktarma
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void TabloyuGuncelle()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Tbl_Randevular.Randevuid, Tbl_Doktorlar.DoktorAd, Tbl_Doktorlar.DoktorSoyad, Tbl_Branslar.BransAd, Tbl_Randevular.RandevuTarih, Tbl_Randevular.RandevuSaat, Tbl_Randevular.RandevuDurum, Tbl_Randevular.Sikayet from Tbl_Randevular, Tbl_Doktorlar, Tbl_Branslar WHERE Tbl_Randevular.Doktorid = Tbl_Doktorlar.Doktorid AND Tbl_Randevular.Bransid = Tbl_Branslar.Bransid", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular(RandevuTarih,RandevuSaat,Bransid,Doktorid,RandevuDurum) values(@p1,@p2,(SELECT Bransid FROM Tbl_Branslar WHERE BransAd='"+ CmbBrans.Text + "'),(SELECT Doktorid FROM Tbl_Doktorlar WHERE DoktorAd+' '+DoktorSoyad='" + CmbDoktor.Text + "'),'False')", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@p1", MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@p2", MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@p3",CmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@p4",CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu oluşturuldu.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            TabloyuGuncelle();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where Bransid = (SELECT Bransid FROM Tbl_Branslar WHERE BransAd='"+ CmbBrans.Text + "')", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular(Duyuru) values(@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru oluşturuldu.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            //SqlCommand komut2 = new SqlCommand("update Tbl_Randevular set RandevuTarih=@p1, RandevuSaat=@p2, Bransid=(SELECT Bransid FROM Tbl_Branslar WHERE BransAd=@p3), Doktorid=(SELECT Doktorid FROM Tbl_Doktorlar WHERE DoktorAd=@p4 and DoktorSoyad= @p5) where Randevuid=@p6", bgl.baglanti());

            SqlCommand komut2 = new SqlCommand("update Tbl_Randevular set RandevuTarih=@p1, RandevuSaat=@p2 where Randevuid=@p6", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", MskTarih.Text);
            komut2.Parameters.AddWithValue("@p2", MskSaat.Text);
            komut2.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@p4", DoktorAd);
            komut2.Parameters.AddWithValue("@p5", DoktorSoyad);
            komut2.Parameters.AddWithValue("@p6", Txtid.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloyuGuncelle();
        }

        String DoktorAd, DoktorSoyad;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            MskTarih.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            MskSaat.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            CmbDoktor.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            DoktorAd = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            DoktorSoyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmDoktorVeBranslar frd = new FrmDoktorVeBranslar();
            frd.Show();
        }

        private void RchDuyuru_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM Tbl_Randevular WHERE Randevuid='" + Txtid.Text + "'", bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloyuGuncelle();
        }
    }
}
