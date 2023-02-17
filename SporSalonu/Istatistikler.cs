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

namespace SporSalonu
{
    public partial class Istatistikler : KryptonForm
    {
        public Istatistikler()
        {
            InitializeComponent();
        }

        SQLBaglanti baglan = new SQLBaglanti();

        private void Istatistikler_Load(object sender, EventArgs e)
        {
    
            SqlCommand tuye = new SqlCommand("Select count(*) from UyeBilgi",baglan.baglanti()); //Toplam Üye
            SqlDataReader dr1 = tuye.ExecuteReader();
            while (dr1.Read())
            {
                Txt_Toplam_Uye.Text = dr1[0].ToString();
            }

            SqlCommand uye = new SqlCommand("Select count(*) from UyeBilgi where UyeYetkili=''", baglan.baglanti());//Üye
            SqlDataReader dr2 = uye.ExecuteReader();
            while (dr2.Read())
            {
                Txt_Uye.Text = dr2[0].ToString();
            }

            SqlCommand kuye = new SqlCommand("Select count(*) from UyeBilgi where UyeYetkili='' and UyeCinsiyet='Kadın'", baglan.baglanti());//Kadın Üye
            SqlDataReader dr3 = kuye.ExecuteReader();
            while (dr3.Read())
            {
                Txt_Kadin_Uye.Text = dr3[0].ToString();
            }

            SqlCommand kouye = new SqlCommand("Select count(*) from UyeBilgi where UyeYetkili NOT LIKE '' and UyeCinsiyet='Kadın'", baglan.baglanti());//Kadın Özel Üye
            SqlDataReader dr4 = kouye.ExecuteReader();
            while (dr4.Read())
            {
                Txt_Kadin_Ozel_Uye.Text = dr4[0].ToString();
            }

            SqlCommand euye = new SqlCommand("Select count(*) from UyeBilgi where UyeYetkili='' and UyeCinsiyet='Erkek'", baglan.baglanti());//Erkek Üye
            SqlDataReader dr5 = euye.ExecuteReader();
            while (dr5.Read())
            {
                Txt_Erkek_Uye.Text = dr5[0].ToString();
            }

            SqlCommand eouye = new SqlCommand("Select count(*) from UyeBilgi where UyeYetkili NOT LIKE '' and UyeCinsiyet='Erkek'", baglan.baglanti());//Erkek Özel Üye
            SqlDataReader dr6 = eouye.ExecuteReader();
            while (dr6.Read())
            {
                Txt_Erkek_Ozel_Uye.Text = dr6[0].ToString();
            }

            SqlCommand ouye = new SqlCommand("Select count(*) from UyeBilgi where UyeYetkili NOT LIKE ''", baglan.baglanti());//Özel Üye
            SqlDataReader dr7 = ouye.ExecuteReader();
            while (dr7.Read())
            {
                Txt_Ozel_Uye.Text = dr7[0].ToString();
            }

            SqlCommand ant = new SqlCommand("Select count(*) from AntrenorBilgi", baglan.baglanti());//Antrenör 
            SqlDataReader dr8 = ant.ExecuteReader();
            while (dr8.Read())
            {
                Txt_Antrenor.Text = dr8[0].ToString();
            }

            SqlCommand kiloa = new SqlCommand("Select count(*) from UyeBilgi where UyeHedef='Kilo Alma'", baglan.baglanti());//Kilo Almak İsteyenler
            SqlDataReader dr9 = kiloa.ExecuteReader();
            while (dr9.Read())
            {
                Txt_Kilo_Alma.Text = dr9[0].ToString();
            }

            SqlCommand kilov = new SqlCommand("Select count(*) from UyeBilgi where UyeHedef='Kilo Verme'", baglan.baglanti());//Kilo Vermek İsteyenler
            SqlDataReader dr10 = kilov.ExecuteReader();
            while (dr10.Read())
            {
                Txt_Kilo_Verme.Text = dr10[0].ToString();
            }

            SqlCommand sikilasma = new SqlCommand("Select count(*) from UyeBilgi where UyeHedef='Sıkılaşma'", baglan.baglanti());//Sıkılaşmak İsteyenler
            SqlDataReader dr11 = sikilasma.ExecuteReader();
            while (dr11.Read())
            {
                Txt_Sikilasma.Text = dr11[0].ToString();
            }

            SqlCommand guckon = new SqlCommand("Select count(*) from UyeBilgi where UyeHedef='Güç&Kondisyon'", baglan.baglanti());//Güç&Kondisyon İsteyenler
            SqlDataReader dr12 = guckon.ExecuteReader();
            while (dr12.Read())
            {
                Txt_Guc_Kondisyon.Text = dr12[0].ToString();
            }

            SqlCommand tku = new SqlCommand("Select count(*) from UyeBilgi where UyeCinsiyet='Kadın'", baglan.baglanti());//Toplam Kadın Üye
            SqlDataReader dr13 = tku.ExecuteReader();
            while (dr13.Read())
            {
                Txt_Toplam_Kadin_Uye.Text = dr13[0].ToString();
            }

            SqlCommand teu = new SqlCommand("Select count(*) from UyeBilgi where UyeCinsiyet='Erkek'", baglan.baglanti());//Toplam Erkek Üye
            SqlDataReader dr14 = teu.ExecuteReader();
            while (dr14.Read())
            {
                Txt_Toplam_Erkek_Uye.Text = dr14[0].ToString();
            }


        }
    }
}
