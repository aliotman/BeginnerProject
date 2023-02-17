using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SporSalonu
{
    public partial class SifremiUnuttum : KryptonForm
    {
        public SifremiUnuttum()
        {
            InitializeComponent();
        }

        SQLBaglanti baglan = new SQLBaglanti();
        private KryptonForm frmaktif;
        private void FormShow(KryptonForm frm)
        {
            ActiveFormClose();
            frmaktif = frm;
            frm.TopLevel = false;
            Pnl_SifremiUnuttum.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
        private void ActiveFormClose()
        {
            if (frmaktif != null)
            {
                frmaktif.Close();

            }
        }

      
        public void KullaniciIDGetir() 
        {
            SqlCommand komut = new SqlCommand(sifremiunuttumsorgu, baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", Txt_SifremiUnuttum_KullaniciAdi.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Lbl_SU_ID_Test.Text = dr[0].ToString(); 
               
            }
            
           
        }

        public void GuvenlikBilgi() 
        {
            SqlCommand komut2 = new SqlCommand("Select * from GuvenlikBilgi where ID=@p1", baglan.baglanti());
            komut2.Parameters.AddWithValue("@p1", Lbl_SU_ID_Test.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                Lbl_SU_GS_Test.Text = dr2[2].ToString(); 
                Lbl_SU_GS_Cvp_Test.Text = dr2[3].ToString();
                
            }
        }
        
       
       

        private void Btn_SU_GeriDon_Click(object sender, EventArgs e)
        {
            Txt_SifremiUnuttum_KullaniciAdi.Clear();
            Cmb_SU_Guvenlik_Sorusu.SelectedIndex = -1;
            Txt_SU_Guvenlik_Cevap.Clear();
            Grp_SU_Bilgiler.Enabled = false;
            Lbl_SifremiUnuttum_Uyarı.Visible = false;
            if (Pnl_YeniSifre_Bilgiler.Visible == true)
            {
                Pnl_YeniSifre_Bilgiler.Visible = false;
                Pnl_SifremiUnuttum_Bilgiler.Visible = true;
            }
            else
            {
                FormShow(new GirisEkrani());
            }
            
            
        }
        public string sifremiunuttumsorgu;
        private void Btn_SifremiUnuttum_Dogrula_Click_1(object sender, EventArgs e)
        {
            switch (Lbl_SU_GirisTipi.Text)
            {
                case "Admin":
                    sifremiunuttumsorgu = "Select AdminID from AdminBilgi where AdminKullaniciAdi=@p1";
                    KullaniciIDGetir();
                    GuvenlikBilgi();                    
                    break;


                case "Antrenör":
                    sifremiunuttumsorgu = "Select AntID from AntrenorBilgi where AntKullaniciAdi=@p1";
                    KullaniciIDGetir();
                    GuvenlikBilgi();                   
                    break;


                case "Özel Üye":
                    sifremiunuttumsorgu = "Select UyeID from UyeBilgi where UyeKullaniciAdi=@p1";
                    KullaniciIDGetir();
                    GuvenlikBilgi();                 
                    break;

                case "Üye":
                    sifremiunuttumsorgu = "Select UyeID from UyeBilgi where UyeKullaniciAdi=@p1";
                    KullaniciIDGetir();
                    GuvenlikBilgi();                  
                    break;
            }

            if (Cmb_SU_Guvenlik_Sorusu.Text == Lbl_SU_GS_Test.Text && Txt_SU_Guvenlik_Cevap.Text == Lbl_SU_GS_Cvp_Test.Text)
            {
                Pnl_SifremiUnuttum_Bilgiler.Visible = false;
                Pnl_YeniSifre_Bilgiler.Visible = true;
            }
            if (Cmb_SU_Guvenlik_Sorusu.Text != Lbl_SU_GS_Test.Text || Txt_SU_Guvenlik_Cevap.Text != Lbl_SU_GS_Cvp_Test.Text)
            {
                Lbl_SifremiUnuttum_Uyarı.Visible = true;
                Lbl_SifremiUnuttum_Uyarı.Text = "Hatalı giriş! Lütfen bilgilerinizi kontrol ediniz!";
            }

            baglan.baglanti().Close();
        }
        public string YeniSifreSorgu;

        public void YeniSifreGuncelle()
        {

            SqlCommand yenisifre = new SqlCommand(YeniSifreSorgu, baglan.baglanti());
            yenisifre.Parameters.AddWithValue("@p1", Lbl_YS_YeniSifre.Text);
            yenisifre.Parameters.AddWithValue("@p2", Lbl_SU_ID_Test.Text);
            yenisifre.ExecuteNonQuery();
            baglan.baglanti().Close();
        }
        private void Btn_YS_SifreniDegistir_Click(object sender, EventArgs e)
        {
            if (Txt_YS_YeniSifre.Text == Txt_YS_TekrarYeniSifre.Text)
            {
                Lbl_YS_YeniSifre.Text = Txt_YS_TekrarYeniSifre.Text;
                switch (Lbl_SU_GirisTipi.Text)
                {
                    case "Admin":
                        YeniSifreSorgu = "Update AdminBilgi set AdminSifre=@p1 where AdminID=@p2";
                        YeniSifreGuncelle();
                        break;

                    case "Antrenör":
                        YeniSifreSorgu = "Update AntrenorBilgi set AntSifre=@p1 where AntID=@p2";
                        YeniSifreGuncelle();
                        break;

                    case "Özel Üye":
                        YeniSifreSorgu = "Update UyeBilgi set UyeSifre=@p1 where UyeID=@p2";
                        YeniSifreGuncelle();
                        break;

                    case "Üye":
                        YeniSifreSorgu = "Update UyeBilgi set UyeSifre=@p1 where UyeID=@p2";
                        YeniSifreGuncelle();
                        break;
                }
                MessageBox.Show("Şifre Güncellendi!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormShow(new GirisEkrani());


            }
            else
            {
                Lbl_YS_Uyarı.Visible = true;
                Lbl_YS_Uyarı.Text = "Şifreler uyuşmuyor! Lütfen kontrol ediniz!";
            }
        }

        private void Txt_YS_YeniSifre_TextChanged(object sender, EventArgs e)
        {
            if (Txt_YS_YeniSifre.Text != "")
            {
                Pct_YS_Dogru1.Visible = true;
                Txt_YS_TekrarYeniSifre.Enabled = true;
                Pct_YS_Yanlis1.Visible = false;
                Pct_YS_Yanlis2.Visible = false;
            }
            if (Txt_YS_YeniSifre.Text == "")
            {
                Pct_YS_Dogru1.Visible = false;
                Lbl_YS_Uyarı.Visible = false;
                Txt_YS_TekrarYeniSifre.Enabled = false;
            }
        }

        private void Txt_YS_TekrarYeniSifre_TextChanged(object sender, EventArgs e)
        {
            if (Txt_YS_TekrarYeniSifre.Text != Txt_YS_YeniSifre.Text)
            {
                Pct_YS_Dogru1.Visible = false;
                Pct_YS_Yanlis1.Visible = true;
                Pct_YS_Dogru2.Visible = false;
                Pct_YS_Yanlis2.Visible = true;
                Lbl_YS_Uyarı.Visible = true;
                Lbl_YS_Uyarı.Text = "Şifreler uyuşmuyor!";
            }
            else
            {
                Pct_YS_Dogru1.Visible = true;
                Pct_YS_Yanlis1.Visible = false;
                Pct_YS_Dogru2.Visible = true; ;
                Pct_YS_Yanlis2.Visible = false;
                Lbl_YS_Uyarı.Visible = true;
                Lbl_YS_Uyarı.Text = "Şifreler uyuşuyor!";
                Btn_YS_SifreniDegistir.Enabled = true;
            }
        }

        private void Btn_SU_Admin_Click_1(object sender, EventArgs e)
        {
            Lbl_SU_GirisTipi.Text = "Admin";
            Lbl_HesapSifreniYenile.Text = "Admin Olarak Devam Et";
            Grp_SU_Bilgiler.Enabled = true;
        }

        private void Btn_SU_Ant_Click_1(object sender, EventArgs e)
        {
            Lbl_SU_GirisTipi.Text = "Antrenör";
            Lbl_HesapSifreniYenile.Text = "Antrenör Olarak Devam Et";
            Grp_SU_Bilgiler.Enabled = true;
        }

        private void Btn_SU_OzelUye_Click_1(object sender, EventArgs e)
        {

            Lbl_SU_GirisTipi.Text = "Özel Üye";
            Lbl_HesapSifreniYenile.Text = "Özel Üye Olarak Devam Et";
            Grp_SU_Bilgiler.Enabled = true;
        }

        private void Btn_SU_Uye_Click_1(object sender, EventArgs e)
        {
            Lbl_SU_GirisTipi.Text = "Üye";
            Lbl_HesapSifreniYenile.Text = "Üye Olarak Devam Et";
            Grp_SU_Bilgiler.Enabled = true;
        }

        private void Txt_SifremiUnuttum_KullaniciAdi_TextChanged_1(object sender, EventArgs e)
        {
            if (Txt_SifremiUnuttum_KullaniciAdi.Text == "")
            {
                Lbl_SU_KullaniciAdi_Yildiz1.Visible = true;
                Lbl_SifremiUnuttum_Uyarı.Text = "Lütfen belirtilen(*) tüm alanları doldurunuz!";
                Lbl_SifremiUnuttum_Uyarı.Visible = true;
            }
            else
            {
                Lbl_SU_KullaniciAdi_Yildiz1.Visible = false;

            }

            if (Txt_SifremiUnuttum_KullaniciAdi.Text != "" & Cmb_SU_Guvenlik_Sorusu.Text != "" & Txt_SU_Guvenlik_Cevap.Text != "")
            {
                Lbl_SifremiUnuttum_Uyarı.Visible = false;
                Btn_SifremiUnuttum_Dogrula.Enabled = true;
            }
        }

        private void Txt_SU_Guvenlik_Cevap_TextChanged_1(object sender, EventArgs e)
        {
            if (Txt_SU_Guvenlik_Cevap.Text == "")
            {
                Lbl_SU_KullaniciAdi_Yildiz3.Visible = true;
                Lbl_SifremiUnuttum_Uyarı.Text = "Lütfen belirtilen(*) tüm alanları doldurunuz!";
                Lbl_SifremiUnuttum_Uyarı.Visible = true;
            }
            else
            {
                Lbl_SU_KullaniciAdi_Yildiz3.Visible = false;
            }
            if (Txt_SifremiUnuttum_KullaniciAdi.Text != "" & Cmb_SU_Guvenlik_Sorusu.Text != "" & Txt_SU_Guvenlik_Cevap.Text != "")
            {
                Lbl_SifremiUnuttum_Uyarı.Visible = false;
                Btn_SifremiUnuttum_Dogrula.Enabled = true;
            }
        }

        private void Cmb_SU_Guvenlik_Sorusu_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (Cmb_SU_Sorusu.Text == "")
            {
                Lbl_SU_KullaniciAdi_Yildiz2.Visible = true;
                Lbl_SifremiUnuttum_Uyarı.Text = "Lütfen belirtilen(*) tüm alanları doldurunuz!";
                Lbl_SifremiUnuttum_Uyarı.Visible = true;
            }
            else
            {
                Lbl_SU_KullaniciAdi_Yildiz2.Visible = false;

            }
            if (Txt_SifremiUnuttum_KullaniciAdi.Text == "" | Txt_SU_Guvenlik_Cevap.Text == "")
            {
                Lbl_SifremiUnuttum_Uyarı.Text = "Lütfen belirtilen(*) tüm alanları doldurunuz!";
                Lbl_SifremiUnuttum_Uyarı.Visible = true;
            }
        }
    }
}
