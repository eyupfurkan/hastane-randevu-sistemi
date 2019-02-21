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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = tc;
            //Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();

            /*//Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where Hastaid="+tc,bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt; */

            //Branş Çekme

            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT Tbl_Randevular.Randevuid, Tbl_Doktorlar.DoktorAd, Tbl_Doktorlar.DoktorSoyad, Tbl_Randevular.RandevuTarih, Tbl_Randevular.RandevuSaat, Tbl_Randevular.RandevuDurum, Tbl_Randevular.Sikayet FROM Tbl_Randevular, Tbl_Doktorlar WHERE Hastaid IN (select Hastaid from Tbl_Hastalar where HastaTC = '" + tc + "' and RandevuDurum='True') AND Tbl_Randevular.Doktorid = Tbl_Doktorlar.Doktorid", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("SELECT DoktorAd,DoktorSoyad FROM Tbl_Doktorlar WHERE Bransid IN (select Bransid from tbl_Branslar where BransAd LIKE @p1)", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid,RandevuTarih,RandevuSaat from Tbl_Randevular where Bransid IN (select Bransid FROM Tbl_Branslar WHERE BransAd LIKE '" + CmbBrans.Text + "') and Doktorid IN (select Doktorid FROM Tbl_Doktorlar WHERE DoktorAd + ' ' + DoktorSoyad LIKE '" + CmbDoktor.Text + "' and RandevuDurum='False') ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = LblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        public string RandevuTarihi, RandevuSaati;

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Randevular SET RandevuDurum='False', Hastaid=null WHERE Randevuid='" + Txtid.Text + "' ", bgl.baglanti());
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            TabloGuncelle();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void TabloGuncelle()
        {
            //Tablo Güncelleme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT Tbl_Randevular.Randevuid, Tbl_Doktorlar.DoktorAd, Tbl_Doktorlar.DoktorSoyad, Tbl_Randevular.RandevuTarih, Tbl_Randevular.RandevuSaat, Tbl_Randevular.RandevuDurum, Tbl_Randevular.Sikayet FROM Tbl_Randevular, Tbl_Doktorlar WHERE Hastaid IN (select Hastaid from Tbl_Hastalar where HastaTC = '" + tc + "' and RandevuDurum='True') AND Tbl_Randevular.Doktorid = Tbl_Doktorlar.Doktorid", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid,RandevuTarih,RandevuSaat from Tbl_Randevular where Bransid IN (select Bransid FROM Tbl_Branslar WHERE BransAd LIKE '" + CmbBrans.Text + "') and Doktorid IN (select Doktorid FROM Tbl_Doktorlar WHERE DoktorAd + ' ' + DoktorSoyad LIKE '" + CmbDoktor.Text + "' and RandevuDurum='False') ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Randevular SET RandevuDurum='True',Hastaid=(SELECT Hastaid FROM Tbl_Hastalar WHERE HastaTC ='" + tc + "'),Sikayet=@r2 WHERE Randevuid='" + Txtid.Text + "' ", bgl.baglanti());
            komut.Parameters.AddWithValue("@r2", RchSikayet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            TabloGuncelle();
            


            /*SqlCommand komut = new SqlCommand("insert into Tbl_Randevular (RandevuDurum,Hastaid,Sikayet,Doktorid,Bransid) SELECT 1,(SELECT Hastaid FROM Tbl_Hastalar WHERE HastaTC ='"+ tc +"'), @r2, (SELECT Doktorid from Tbl_Doktorlar WHERE DoktorAd+' '+DoktorSoyad ='"+ CmbDoktor.Text + "'),(SELECT Bransid from Tbl_Branslar WHERE BransAd ='" + CmbBrans.Text + "')", bgl.baglanti());
            komut.Parameters.AddWithValue("@r2", RchSikayet.Text);
            komut.Parameters.AddWithValue("@r3", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            //dataGridView2_CellClick özelliği çalışmadığı için Randevuid gelmiyor ve aşağıdaki kodda veri gelirken hata veriyor.
            SqlCommand komut2 = new SqlCommand("select RandevuTarih,RandevuSaat from Tbl_Randevular where Randevuid=@r4", bgl.baglanti());
            komut2.Parameters.AddWithValue("@r4", Txtid.Text);
            SqlDataReader dr = komut2.ExecuteReader();
            RandevuTarihi = dr[1].ToString();
            RandevuSaati = dr[2].ToString();
            MessageBox.Show(CmbBrans.Text + " bölümünden " + CmbDoktor.Text + " doktordan \n" + RandevuTarihi + " tarihinde " + RandevuSaati + " ve saatinde randevunuz alınmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            bgl.baglanti().Close();*/
            //Buraya kadar!
        }
    }
}
