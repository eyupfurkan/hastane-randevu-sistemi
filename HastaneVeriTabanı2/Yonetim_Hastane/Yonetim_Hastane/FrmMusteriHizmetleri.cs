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
    public partial class FrmMusteriHizmetleri : Form
    {
        public FrmMusteriHizmetleri()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        public string MusteriHizmetleriTC;
        private void FrmMusteriHizmetleri_Load(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();

            lblMusteriHizmetleriTC.Text = MusteriHizmetleriTC;

            //Musteri Hizmetleri AdSoyad Çekme
            SqlCommand komut = new SqlCommand("select MusteriHizmetleriAd, MusteriHizmetleriSoyad from Tbl_Musteri_Hizmetleri where MusteriHizmetleriTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MusteriHizmetleriTC);
            SqlDataReader dr1 = komut.ExecuteReader();
            while (dr1.Read())
            {
                lblAdiSoyadi.Text = dr1[0].ToString() + " " + dr1[1].ToString();
            }
            bgl.baglanti().Close();
        }

        private void TabloGuncelle()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid,RandevuTarih,RandevuSaat,Sikayet from Tbl_Randevular where Bransid IN (select Bransid FROM Tbl_Branslar WHERE BransAd LIKE '" + CmbBrans.Text + "') and Doktorid IN (select Doktorid FROM Tbl_Doktorlar WHERE DoktorAd + ' ' + DoktorSoyad LIKE '" + CmbDoktor.Text + "' and RandevuDurum='False') ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void CmbBrans_SelectedIndexChanged_1(object sender, EventArgs e)
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
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid,RandevuTarih,RandevuSaat,Sikayet from Tbl_Randevular where Bransid IN (select Bransid FROM Tbl_Branslar WHERE BransAd LIKE '" + CmbBrans.Text + "') and Doktorid IN (select Doktorid FROM Tbl_Doktorlar WHERE DoktorAd + ' ' + DoktorSoyad LIKE '" + CmbDoktor.Text + "' and RandevuDurum='False') ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Randevular SET RandevuDurum='True',Hastaid=(SELECT Hastaid FROM Tbl_Hastalar WHERE HastaTC ='"+ txtHastaTC.Text +"'),Sikayet=@r2 WHERE Randevuid='" + Txtid.Text + "' ", bgl.baglanti());
            komut.Parameters.AddWithValue("@r2", RchSikayet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloGuncelle();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString(); 
        }

        private void btnRandevularıGetir_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Randevuid,RandevuTarih,RandevuSaat,Sikayet from Tbl_Randevular where Hastaid=(SELECT Hastaid FROM Tbl_Hastalar WHERE HastaTC='"+ txtHastaTC.Text +"') ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void btnRandevuSil_Click(object sender, EventArgs e)
        {
            //SqlCommand komut = new SqlCommand("UPDATE Tbl_Randevular SET RandevuDurum='False' where Randevuid=@p1", bgl.baglanti());
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Randevular SET RandevuDurum='False', Hastaid=null WHERE Randevuid='" + Txtid.Text + "' ", bgl.baglanti());
            //komut.Parameters.AddWithValue("@p1", Txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            TabloGuncelle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
