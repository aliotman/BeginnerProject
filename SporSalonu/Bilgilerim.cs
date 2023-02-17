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
using ComponentFactory.Krypton.Toolkit;
using System.Data.SqlClient;

namespace SporSalonu
{
    public partial class Bilgilerim : KryptonForm
    {
        public Bilgilerim()
        {
            InitializeComponent();
        }
        SQLBaglanti baglan = new SQLBaglanti();


        public string ad;
        public string id;
        public string sorgu;
        private void Bilgilerim_Load(object sender, EventArgs e)
        {

            Lbl_Bilgiler_Ad.Text = ad;
            Lbl_Bilgiler_ID.Text = id;

            SqlCommand blg = new SqlCommand("Select * from UyeBilgi where UyeID=@p1",baglan.baglanti());
            blg.Parameters.AddWithValue("@p1", Lbl_Bilgiler_ID.Text);
            SqlDataReader dr = blg.ExecuteReader();
            while (dr.Read())
            {
                Txt_ID.Text = Lbl_Bilgiler_ID.Text;
                Txt_Ad.Text= dr[1].ToString();
                Txt_Soyad.Text= dr[2].ToString();
                Txt_KullaniciAdi.Text= dr[3].ToString();
                Txt_Sifre.Text= dr[4].ToString();
                Msk_DogumTarihi.Text= dr[5].ToString();
                Cmb_Meslek.Text= dr[6].ToString();
                Msk_Iletisim.Text= dr[7].ToString();
                Cmb_Cinsiyet.Text= dr[8].ToString();
                Cmb_Hedefler.Text= dr[9].ToString();
                Txt_Uye_Yetkili.Text= dr[10].ToString();
                Lbl_UyelikTarihi2.Text= dr[11].ToString();
            }

            SqlCommand blg2 = new SqlCommand("Select * from GuvenlikBilgi where ID=@p1", baglan.baglanti());
            blg2.Parameters.AddWithValue("@p1", Lbl_Bilgiler_ID.Text);
            SqlDataReader dr2 = blg2.ExecuteReader();
            while (dr2.Read())
            {
               
                Cmb_Guvenlik_Sorusu.Text = dr2[2].ToString();
                Txt_Guvenlik_Cevap.Text = dr2[3].ToString();
            }

            SqlCommand blg3 = new SqlCommand("Select * from UyeOlculer where UyeID=@p1",baglan.baglanti());
            blg3.Parameters.AddWithValue("@p1", Lbl_Bilgiler_ID.Text);
            SqlDataReader dr3 = blg3.ExecuteReader();
            while (dr3.Read())
            {
                Msk_Omuz.Text = dr3[1].ToString();
                Msk_Gogus.Text = dr3[2].ToString();
                Msk_Bel.Text = dr3[3].ToString();
                Msk_Kalca.Text = dr3[4].ToString();
                Msk_Baldir.Text = dr3[5].ToString();
                Msk_Kol.Text = dr3[6].ToString();
                Msk_Boy.Text = dr3[7].ToString();
                Msk_Kilo.Text = dr3[8].ToString();
                Cmb_Soru1.Text = dr3[9].ToString(); 
                Rch_Soru1_Cevap.Text = dr3[10].ToString();
                Cmb_Soru2.Text = dr3[11].ToString();
                Rch_Soru2_Cevap.Text = dr3[12].ToString();
            }
            

        }
    }
}
