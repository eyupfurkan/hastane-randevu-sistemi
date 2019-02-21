using System;
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
    public partial class FrmYoneticiEkle : Form
    {
        public FrmYoneticiEkle()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();
        private void BtnYoneticiEkle_Click(object sender, EventArgs e)
        {
            if (FrmHastaKayit.TCKontrol(MskTC.Text)) {
                SqlCommand komut2 = new SqlCommand("insert into Tbl_Yoneticiler(YoneticiTC,YoneticiAd,YoneticiSoyad,YoneticiSifre) values (@p1, @p2, @p3, @p4)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p2", TxtAd.Text);
                komut2.Parameters.AddWithValue("@p3", TxtSoyad.Text);
                komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
                komut2.Parameters.AddWithValue("@p1", MskTC.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yönetici Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TabloGuncelle();
            }
        }

        private void TabloGuncelle()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Yoneticiid,YoneticiAd,YoneticiSoyad,YoneticiTC,YoneticiSifre from Tbl_Yoneticiler", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtYoneticiid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void btnYoneticiSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("delete from Tbl_Yoneticiler where Yoneticiid=@p1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", TxtYoneticiid.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yönetici Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloGuncelle();
        }

        private void FrmYoneticiEkle_Load(object sender, EventArgs e)
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select Yoneticiid,YoneticiAd,YoneticiSoyad,YoneticiTC,YoneticiSifre from Tbl_Yoneticiler", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView1.DataSource = dt2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Tbl_Yoneticiler set YoneticiAd=@p1, YoneticiSoyad=@p2, YoneticiTC=@p3, YoneticiSifre=@p4 where Yoneticiid=@p5", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", MskTC.Text);
            komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", TxtYoneticiid.Text);

            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yönetici Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloGuncelle();
           
        }
    }
}
