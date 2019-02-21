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
    public partial class FrmHastaKayit : Form
    {
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BtnKayitYap_Click(object sender, EventArgs e)
        {
            if ( TCKontrol(MskTC.Text) ) {
                SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar(HastaAd,HastaSoyad,HastaTC,HastaTelefon,HastaSifre,HastaCinsiyet) values(@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komut.Parameters.AddWithValue("@p3", MskTC.Text);
                komut.Parameters.AddWithValue("@p4", MskTelefon.Text);
                komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
                komut.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kaydınız gerçekleşmiştir. Şifreniz: " + TxtSifre.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static Boolean TCKontrol(String TC)
        {
            string tcNo = TC;
            int toplam = 0; int toplam2 = 0; int toplam3 = 0;
            if (tcNo.Length == 11)
            {
                if (Convert.ToInt32(tcNo[0].ToString()) != 0) //tc kimlik numaranın ilk hanesi 0 değilse
                {
                    for (int i = 0; i < 10; i++)
                    {
                        toplam = toplam + Convert.ToInt32(tcNo[i].ToString());
                        if (i % 2 == 0)
                        {
                            if (i != 10)
                            {
                                toplam2 = toplam2 + Convert.ToInt32(tcNo[i].ToString()); // 7 ile çarpılacak sayıları topluyoruz
                            }

                        }
                        else
                        {
                            if (i != 9)
                            {
                                toplam3 = toplam3 + Convert.ToInt32(tcNo[i].ToString());
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Tc Kimlik Numaranızın ilk hanesi 0 olamaz.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Tc Kimlik Numarınız 11 haneli olmak zorunda.Eksik ya da fazla değer girdiniz.");
                return false;

            }
            if (((toplam2 * 7) - toplam3) % 10 == Convert.ToInt32(tcNo[9].ToString()) && toplam % 10 == Convert.ToInt32(tcNo[10].ToString()))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Tc Kimlik Numarası Yanlış!");
                return false;
            }
        }

    }
}
