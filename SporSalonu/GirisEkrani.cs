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
using System.Data.SqlClient;
using System.Diagnostics;
using SporSalonu.Properties;

namespace SporSalonu
{
    public partial class GirisEkrani : KryptonForm
    {
        public GirisEkrani()
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
            Pnl_GirisEkrani.Controls.Add(frm);
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
        private void Btn_Uye_Giris_Click(object sender, EventArgs e)
        {
            Lbl_Giris_Tipi.Text = "Antrenör Olarak Giriş Yapılıyor";
            Lbl_GirisTipi_Kontrol.Text = "Antrenör";
            Grp_GirisBilgiler.Enabled = true;
    
        }

        public void Btn_Admin_Giris_Click(object sender, EventArgs e)
        {
            Lbl_Giris_Tipi.Text = "Admin Olarak Giriş Yapılıyor";
            Lbl_GirisTipi_Kontrol.Text = "Admin";
            Grp_GirisBilgiler.Enabled = true;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Lbl_Giris_Tipi.Text = "Özel Üye Olarak Giriş Yapılıyor";
            Lbl_GirisTipi_Kontrol.Text = "Özel Üye";
            Grp_GirisBilgiler.Enabled = true;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Lbl_Giris_Tipi.Text = "Üye Olarak Giriş Yapılıyor";
            Lbl_GirisTipi_Kontrol.Text = "Üye";
            Grp_GirisBilgiler.Enabled = true;
        }

        private void Btn_Giris_GirisYap_Click(object sender, EventArgs e)
        {
          
            if (Lbl_GirisTipi_Kontrol.Text=="Admin") // Admin Giriş 
            {

                if (Txt_Giris_KullaniciAdi.Text!="" && Txt_Giris_Sifre.Text!="")
                {
                    Lbl_Giris_Uyarı.Visible = false;
                    SqlCommand komut = new SqlCommand("Select * from AdminBilgi where AdminKullaniciAdi=@p1 and AdminSifre=@p2", baglan.baglanti());
                    komut.Parameters.AddWithValue("@p1", Txt_Giris_KullaniciAdi.Text);
                    komut.Parameters.AddWithValue("@p2", Txt_Giris_Sifre.Text);
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        AdminPaneli ad = new AdminPaneli();
                        ad.adkullaniciadi = Txt_Giris_KullaniciAdi.Text;
                        ad.Name = "AdminPaneli";
                        if (Application.OpenForms[ad.Name] == null)
                        {
                            Form.ActiveForm.Hide();
                            ad.Show();
                            //ad.TopMost = true;

                        }
                        else
                        {
                            DialogResult drs = new DialogResult();
                            drs = MessageBox.Show("Admin Paneli Zaten Kullanılıyor", "Açık Hesap!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (drs == DialogResult.OK)
                            {
                                Form.ActiveForm.Hide();
                            }

                        }
                    }
                    else
                    {
                        Lbl_Giris_Uyarı.Visible = true;
                        Lbl_Giris_Uyarı.Text = "Hatalı Kullanıcı Adı veya Şifre!";
                    }

                   

                }
                else if (Txt_Giris_KullaniciAdi.Text=="" || Txt_Giris_Sifre.Text=="")
                {
                    Lbl_Giris_Uyarı.Visible = true;
                    Lbl_Giris_Uyarı.Text = "Lütfen belirtilen (*) tüm alanları doldurunuz!";
                }
                return;

            }
            if (Lbl_GirisTipi_Kontrol.Text == "Antrenör") // Antrenör Giriş 
            {
               
                if (Txt_Giris_KullaniciAdi.Text != "" && Txt_Giris_Sifre.Text != "")
                {
                    Lbl_Giris_Uyarı.Visible = false;
                    SqlCommand komut = new SqlCommand("Select * from AntrenorBilgi where AntKullaniciAdi=@p1 and AntSifre=@p2", baglan.baglanti());
                    komut.Parameters.AddWithValue("@p1", Txt_Giris_KullaniciAdi.Text);
                    komut.Parameters.AddWithValue("@p2", Txt_Giris_Sifre.Text);
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        AntrenorPaneli ant = new AntrenorPaneli();
                        ant.Name = "AntrenorPaneli";
                        ant.antkullaniciadi = Txt_Giris_KullaniciAdi.Text;
                        if (Application.OpenForms[ant.Name] == null)
                        {
                            Form.ActiveForm.Hide();
                            ant.Show();
                            //ad.TopMost = true;

                        }
                        else
                        {
                            DialogResult drs = new DialogResult();
                            drs = MessageBox.Show("Antrenör Paneli Zaten Kullanılıyor", "Açık Hesap!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (drs == DialogResult.OK)
                            {
                                Form.ActiveForm.Hide();
                            }

                        }


                    }
                    else
                    {
                        Lbl_Giris_Uyarı.Visible = true;
                        Lbl_Giris_Uyarı.Text = "Hatalı Kullanıcı Adı veya Şifre!";
                    }
                   
                }
                else if (Txt_Giris_KullaniciAdi.Text == "" || Txt_Giris_Sifre.Text == "")
                {
                    Lbl_Giris_Uyarı.Visible = true;
                    Lbl_Giris_Uyarı.Text = "Lütfen belirtilen (*) tüm alanları doldurunuz!";
                }
                return;
            }

            if (Lbl_GirisTipi_Kontrol.Text == "Üye") // Üye Giriş 
            {

                if (Txt_Giris_KullaniciAdi.Text != "" && Txt_Giris_Sifre.Text != "")
                {
                    Lbl_Giris_Uyarı.Visible = false;
                    SqlCommand komut = new SqlCommand("Select * from UyeBilgi where UyeKullaniciAdi=@p1 and UyeSifre=@p2 and UyeYetkili=''", baglan.baglanti());
                    komut.Parameters.AddWithValue("@p1", Txt_Giris_KullaniciAdi.Text);
                    komut.Parameters.AddWithValue("@p2", Txt_Giris_Sifre.Text);
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        UyePaneli uye = new UyePaneli();
                        uye.Name = "UyePaneli";
                        uye.uyekullaniciadi = Txt_Giris_KullaniciAdi.Text;
                        if (Application.OpenForms[uye.Name] == null)
                        {
                            Form.ActiveForm.Hide();
                            uye.Show();
                            //ad.TopMost = true;

                        }
                        else
                        {
                            DialogResult drs = new DialogResult();
                            drs = MessageBox.Show("Üye Paneli Zaten Kullanılıyor", "Açık Hesap!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (drs == DialogResult.OK)
                            {
                                Form.ActiveForm.Hide();
                            }

                        }


                    }
                    else
                    {
                        Lbl_Giris_Uyarı.Visible = true;
                        Lbl_Giris_Uyarı.Text = "Hatalı Kullanıcı Adı veya Şifre!";
                    }
                   
                   
                }
                else if (Txt_Giris_KullaniciAdi.Text == "" || Txt_Giris_Sifre.Text == "")
                {
                    Lbl_Giris_Uyarı.Visible = true;
                    Lbl_Giris_Uyarı.Text = "Lütfen belirtilen (*) tüm alanları doldurunuz!";
                }
                return;
            }

            if (Lbl_GirisTipi_Kontrol.Text == "Özel Üye") // Özel Üye Giriş 
            {

                if (Txt_Giris_KullaniciAdi.Text != "" && Txt_Giris_Sifre.Text != "")
                {
                    Lbl_Giris_Uyarı.Visible = false;
                    SqlCommand komut = new SqlCommand("Select * from UyeBilgi where UyeKullaniciAdi=@p1 and UyeSifre=@p2 and UyeYetkili NOT LIKE ''", baglan.baglanti());
                    komut.Parameters.AddWithValue("@p1", Txt_Giris_KullaniciAdi.Text);
                    komut.Parameters.AddWithValue("@p2", Txt_Giris_Sifre.Text);
                    SqlDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        OzelUyePaneli ouye = new OzelUyePaneli();
                        ouye.Name = "OzelUyePaneli";
                        ouye.uyekullaniciadi = Txt_Giris_KullaniciAdi.Text;
                        if (Application.OpenForms[ouye.Name] == null)
                        {
                            Form.ActiveForm.Hide();
                            ouye.Show();
                            //ad.TopMost = true;

                        }
                        else
                        {
                            DialogResult drs = new DialogResult();
                            drs = MessageBox.Show("Özel Uye Paneli Zaten Kullanılıyor", "Açık Hesap!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (drs == DialogResult.OK)
                            {
                                Form.ActiveForm.Hide();
                            }

                        }


                    }
                    else
                    {
                        Lbl_Giris_Uyarı.Visible = true;
                        Lbl_Giris_Uyarı.Text = "Hatalı Kullanıcı Adı veya Şifre!";
                    }


                }
                else if (Txt_Giris_KullaniciAdi.Text == "" || Txt_Giris_Sifre.Text == "")
                {
                    Lbl_Giris_Uyarı.Visible = true;
                    Lbl_Giris_Uyarı.Text = "Lütfen belirtilen (*) tüm alanları doldurunuz!";
                }
                return;
            }






            baglan.baglanti().Close();
        }

        private void Txt_Giris_KullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Giris_KullaniciAdi.Text == "")
            {
                Lbl_Giris_KullaniciAdi_Yildiz.Visible = true;
                if (Txt_Giris_KullaniciAdi.Text=="" && Txt_Giris_Sifre.Text=="")
                {
                    Lbl_Giris_Uyarı.Visible = true;
                    Lbl_Giris_Uyarı.Text = "Lütfen belirtilen (*) tüm alanları doldurunuz!";
                }
               
            }
            else
            {
                Lbl_Giris_KullaniciAdi_Yildiz.Visible = false;
                Lbl_Giris_Uyarı.Visible=false;
            }
        }

        private void Txt_Giris_Sifre_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Giris_Sifre.Text == "")
            {
                Lbl_Giris_Sifre_Yildiz.Visible = true;
                if (Txt_Giris_KullaniciAdi.Text=="" && Txt_Giris_Sifre.Text=="")
                {
                    Lbl_Giris_Uyarı.Visible = true;
                    Lbl_Giris_Uyarı.Text = "Lütfen belirtilen (*) tüm alanları doldurunuz!";
                }
               
            }
            else
            {
                Lbl_Giris_Sifre_Yildiz.Visible = false;
                Lbl_Giris_Uyarı.Visible = false;
            }
        }

        private void Btn_Giris_SifremiUnuttum_Click(object sender, EventArgs e)
        {
            FormShow(new SifremiUnuttum());
        }

    }
}
