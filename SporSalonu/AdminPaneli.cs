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
using System.IO;
using System.Data.SqlClient;
using System.Globalization;

namespace SporSalonu
{
    public partial class AdminPaneli : KryptonForm
    {
        public AdminPaneli()
        {
            InitializeComponent();
          
            

        }
        
            static string RandomID(ref Random rnd)// ID ÜRETİLEN YER
            {
                string ID = "";
                for (int i = 0; i < 10; i++)
                {
                    if (i % 2 == 0)
                    {
                        ID += Convert.ToChar(rnd.Next(65, 91));
                    }
                    else
                    {
                        ID += Convert.ToChar(rnd.Next(48, 58));
                    }
                }
                return ID;
            }


        
       
        

        public void Temizle() //TEMİZLEME METODU
        {         
            Txt_ID.Clear();
            Txt_Ad.Clear();
            Txt_Soyad.Clear();
            Msk_DogumTarihi.Clear();
            Msk_Iletisim.Clear();
            Cmb_Cinsiyet.SelectedIndex = -1;
            Cmb_Meslek.SelectedIndex = -1;
            Cmb_Guvenlik_Sorusu.SelectedIndex = -1;
            Txt_Guvenlik_Cevap.Clear();
            Txt_KullaniciAdi.Clear();
            Txt_Sifre.Clear();
            Cmb_Hedefler.SelectedIndex = -1;
            Cmb_Yetkili.SelectedIndex = -1;
            Msk_Omuz.Clear();
            Msk_Baldir.Clear();
            Msk_Bel.Clear();
            Msk_Boy.Clear();
            Msk_Gogus.Clear();
            Msk_Kol.Clear();
            Msk_Kalca.Clear();
            Msk_Kilo.Clear();
            Cmb_Soru1.SelectedIndex = -1;
            Rch_Soru1_Cevap.Clear();
            Cmb_Soru2.SelectedIndex = -1;
            Rch_Soru2_Cevap.Clear();
            Cmb_Kayit_Tipi.Enabled = true;
           
        }
        public void Varsayilan()//İLK AÇILDIĞI HALE DÖNME
        {   //Kişisel Bilgiler
            Cmb_Kayit_Tipi.SelectedIndex = -1;
            Cmb_Kayit_Tipi.Enabled = true;
            Txt_ID.Clear();
            Txt_ID.Enabled = false;
            Txt_Ad.Clear();
            Txt_Ad.Enabled = false;
            Txt_Soyad.Clear();
            Txt_Soyad.Enabled = false;
            Msk_DogumTarihi.Clear();
            Msk_DogumTarihi.Enabled = false;
            Msk_Iletisim.Clear();
            Msk_Iletisim.Enabled = false;
            Cmb_Cinsiyet.SelectedIndex = -1;
            Cmb_Cinsiyet.Enabled = false;
            Cmb_Meslek.SelectedIndex = -1;
            Cmb_Meslek.Enabled = false;
            Cmb_Guvenlik_Sorusu.SelectedIndex = -1;
            Cmb_Guvenlik_Sorusu.Enabled = false;
            Btn_Listele.Enabled = true;

            Txt_Guvenlik_Cevap.Clear();
            Txt_Guvenlik_Cevap.Enabled = false;

            Txt_KullaniciAdi.Clear();
            Txt_Sifre.Clear();
           

            Cmb_Hedefler.SelectedIndex = -1;
            Grp_Hedefler.Enabled = false;

            Cmb_Yetkili.SelectedIndex = -1;
            Grp_Yetkili.Enabled = false;

            //Ölçüler  
            Msk_Omuz.Clear();
            Msk_Baldir.Clear();
            Msk_Bel.Clear();
            Msk_Boy.Clear();
            Msk_Gogus.Clear();
            Msk_Kol.Clear();
            Msk_Kalca.Clear();
            Msk_Kilo.Clear();
            Cmb_Soru1.SelectedIndex = -1;
            Rch_Soru1_Cevap.Clear();
            Cmb_Soru2.SelectedIndex = -1;
            Rch_Soru2_Cevap.Clear();
            Grp_Olculer.Enabled = false;

            //Butonlar
            Btn_KayıtOl.Enabled = false;
            Btn_Guncelle.Enabled = false;
            Btn_Sil.Enabled = false;

            //DataGridView
            Dt_Kayitlar.DataSource = "";

            //Tarih          
            Date_Tarih.Value = Convert.ToDateTime(DateTime.Now);            
        }

        public void OrtakAlanlar()
        {
            Txt_Ad.Enabled = true;
            Txt_Soyad.Enabled = true;
            Msk_Iletisim.Enabled = true;
            Cmb_Guvenlik_Sorusu.Enabled = true;
            Txt_Guvenlik_Cevap.Enabled = true;
        }

        SQLBaglanti baglan = new SQLBaglanti();
       
      
       

        public string adkullaniciadi;
        private void AdminPaneli_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Lbl_Giris_Yapanın_Adı_Admin.Text = adkullaniciadi;
            SqlCommand komut = new SqlCommand("Select (AdminAdi+' '+AdminSoyadi) as Ad_Soyad from AdminBilgi where AdminKullaniciAdi=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", adkullaniciadi);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Lbl_Giris_Yapanın_Adı_Admin.Text = dr[0].ToString();
            }
        }
        public bool BtnAnaMenuClick = false;
        private void Btn_Admin_AnaMenu_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
            BtnAnaMenuClick = true;
            AnaForm fr = new AnaForm();
            fr.ShowDialog();
            this.Close();
            //AnaForm fr = new AnaForm();
           
            //fr.ShowDialog();
            //*// AnaForm fr = new AnaForm();   
            //fr.Show();
            //**//this.Close();
        }

        public void IDKontrol()
        {
            Random rnd = new Random();
            tekrar:
            string idcheck = (RandomID(ref rnd));
            SqlCommand idkontrol = new SqlCommand("Select ID from GuvenlikBilgi where ID=@p1",baglan.baglanti());
            idkontrol.Parameters.AddWithValue("@p1", idcheck);
            SqlDataReader dr = idkontrol.ExecuteReader();
            if (dr.Read())
            {

                goto tekrar;
               
            }
            else
            {
                
                Txt_ID.Text = idcheck;//ID FARKLI İSE
            }

        }
        public string kacheck;
        public string kasorgu;
       
       

        private void Btn_KayıtOl_Click_1(object sender, EventArgs e) //KAYIT OL BUTONU
        {
            
       
            
            if (Cmb_Kayit_Tipi.Text == "" || Txt_Ad.Text == "" || Txt_Soyad.Text == "" || Txt_KullaniciAdi.Text == "" || Txt_Sifre.Text == "" || Cmb_Guvenlik_Sorusu.Text == "" || Txt_Guvenlik_Cevap.Text == "")
            {
                MessageBox.Show("Lütfen belirtilen (*) bütün boş alanları doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Cmb_Kayit_Tipi.Text == "Admin")
                {
                    SqlCommand kakontrol = new SqlCommand("Select AdminKullaniciAdi from AdminBilgi where AdminKullaniciAdi=@p1", baglan.baglanti());
                    kakontrol.Parameters.AddWithValue("@p1", Txt_KullaniciAdi.Text);
                    SqlDataReader dr = kakontrol.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        IDKontrol();
                        //---------------------------------------------------------ADMİN KAYIT BİLGİLERİ---------------------------------------------------------
                        SqlCommand kayit4 = new SqlCommand("Insert into AdminBilgi (AdminID,AdminAdi,AdminSoyadi,AdminKullaniciAdi,AdminSifre,AdminIletisim) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglan.baglanti());
                        kayit4.Parameters.AddWithValue("@p1", Txt_ID.Text);
                        kayit4.Parameters.AddWithValue("@p2", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Txt_Ad.Text));
                        kayit4.Parameters.AddWithValue("@p3", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Txt_Soyad.Text));
                        kayit4.Parameters.AddWithValue("@p4", Txt_KullaniciAdi.Text.ToLower());
                        kayit4.Parameters.AddWithValue("@p5", Txt_Sifre.Text);
                        kayit4.Parameters.AddWithValue("@p6", Msk_Iletisim.Text);
                        kayit4.ExecuteNonQuery();
                        SqlCommand kayit5 = new SqlCommand("Insert into GuvenlikBilgi (ID,KayitTipi,GuvenlikSoru,GuvenlikCevap) values (@p1,@p2,@p3,@p4)", baglan.baglanti());
                        kayit5.Parameters.AddWithValue("@p1", Txt_ID.Text);
                        kayit5.Parameters.AddWithValue("@p2", Cmb_Kayit_Tipi.Text);
                        kayit5.Parameters.AddWithValue("@p3", Cmb_Guvenlik_Sorusu.Text);
                        kayit5.Parameters.AddWithValue("@p4", Txt_Guvenlik_Cevap.Text);
                        kayit5.ExecuteNonQuery();
                        baglan.baglanti().Close();
                        MessageBox.Show("Kayıt başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);                     
                        Varsayilan();
                        baglan.baglanti().Close();
                    }
                }
                else if (Cmb_Kayit_Tipi.Text=="Üye" || Cmb_Kayit_Tipi.Text =="Özel Üye")
                {
                    SqlCommand kakontrol = new SqlCommand("Select UyeKullaniciAdi from UyeBilgi where UyeKullaniciAdi=@p1", baglan.baglanti());
                    kakontrol.Parameters.AddWithValue("@p1", Txt_KullaniciAdi.Text);
                    SqlDataReader dr = kakontrol.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (Cmb_Kayit_Tipi.Text=="Özel Üye" & Cmb_Yetkili.SelectedIndex<0)
                        {
                            MessageBox.Show("Özel üye kaydederken antrenör seçmelisiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            IDKontrol(); // ID ÜRETİLEN YER
                            SqlCommand kayit = new SqlCommand("Insert into UyeBilgi (UyeID,UyeAdi,UyeSoyadi,UyeKullaniciAdi,UyeSifre,UyeDogumTarihi,UyeMeslek,UyeIletisim,UyeCinsiyet,UyeHedef,UyeYetkili,UyeKayitTarihi,UyeBeslenmeProgrami) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13)", baglan.baglanti());
                            //---------------------------------------------------------ÜYE KAYIT BİLGİLERİ---------------------------------------------------------                   
                            kayit.Parameters.AddWithValue("@p1", Txt_ID.Text);
                            kayit.Parameters.AddWithValue("@p2", Txt_Ad.Text);
                            kayit.Parameters.AddWithValue("@p3", Txt_Soyad.Text);
                            kayit.Parameters.AddWithValue("@p4", Txt_KullaniciAdi.Text);
                            kayit.Parameters.AddWithValue("@p5", Txt_Sifre.Text);
                            kayit.Parameters.AddWithValue("@p6", Msk_DogumTarihi.Text);
                            kayit.Parameters.AddWithValue("@p7", Cmb_Meslek.Text);
                            kayit.Parameters.AddWithValue("@p8", Msk_Iletisim.Text);
                            kayit.Parameters.AddWithValue("@p9", Cmb_Cinsiyet.Text);
                            kayit.Parameters.AddWithValue("@p10", Cmb_Hedefler.Text);
                            kayit.Parameters.AddWithValue("@p11", Cmb_Yetkili.Text);
                            kayit.Parameters.AddWithValue("@p12", Date_Tarih.Value.ToString("dd.MM.yyyy"));
                            kayit.Parameters.AddWithValue("@p13", "");
                            kayit.ExecuteNonQuery();
                            SqlCommand kayit2 = new SqlCommand("Insert into UyeOlculer (UyeID,Omuz,Gogus,Bel,Kalca,Baldir,Kol,Boy,Kilo,Soru1,Soru1Cevap,Soru2,Soru2Cevap) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13)", baglan.baglanti());
                            kayit2.Parameters.AddWithValue("@p1", Txt_ID.Text);
                            kayit2.Parameters.AddWithValue("@p2", Msk_Omuz.Text);
                            kayit2.Parameters.AddWithValue("@p3", Msk_Gogus.Text);
                            kayit2.Parameters.AddWithValue("@p4", Msk_Bel.Text);
                            kayit2.Parameters.AddWithValue("@p5", Msk_Kalca.Text);
                            kayit2.Parameters.AddWithValue("@p6", Msk_Baldir.Text);
                            kayit2.Parameters.AddWithValue("@p7", Msk_Kol.Text);
                            kayit2.Parameters.AddWithValue("@p8", Msk_Boy.Text);
                            kayit2.Parameters.AddWithValue("@p9", Msk_Kilo.Text);
                            kayit2.Parameters.AddWithValue("@p10", Cmb_Soru1.Text);
                            kayit2.Parameters.AddWithValue("@p11", Rch_Soru1_Cevap.Text);
                            kayit2.Parameters.AddWithValue("@p12", Cmb_Soru2.Text);
                            kayit2.Parameters.AddWithValue("@p13", Rch_Soru2_Cevap.Text);
                            kayit2.ExecuteNonQuery();
                            SqlCommand kayit3 = new SqlCommand("Insert into GuvenlikBilgi (ID,KayitTipi,GuvenlikSoru,GuvenlikCevap) values (@p1,@p2,@p3,@p4)", baglan.baglanti());
                            kayit3.Parameters.AddWithValue("@p1", Txt_ID.Text);
                            kayit3.Parameters.AddWithValue("@p2", Cmb_Kayit_Tipi.Text);
                            kayit3.Parameters.AddWithValue("@p3", Cmb_Guvenlik_Sorusu.Text);
                            kayit3.Parameters.AddWithValue("@p4", Txt_Guvenlik_Cevap.Text);
                            kayit3.ExecuteNonQuery();

                            string odenmedi = "Yapılmadı";

                            SqlCommand odemekayit = new SqlCommand("Insert into OdemePlani (UyeID,UyeAdi,UyeSoyadi,[1.Ay],[2.Ay],[3.Ay],[4.Ay],[5.Ay],[6.Ay],[7.Ay],[8.Ay],[9.Ay],[10.Ay],[11.Ay],[12.Ay],UyeYetkili) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16)", baglan.baglanti());
                            odemekayit.Parameters.AddWithValue("@p1", Txt_ID.Text);
                            odemekayit.Parameters.AddWithValue("@p2", Txt_Ad.Text);
                            odemekayit.Parameters.AddWithValue("@p3", Txt_Soyad.Text);
                            odemekayit.Parameters.AddWithValue("@p4", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p5", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p6", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p7", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p8", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p9", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p10", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p11", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p12", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p13", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p14", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p15", odenmedi);
                            odemekayit.Parameters.AddWithValue("@p16", Cmb_Yetkili.Text);
                            odemekayit.ExecuteNonQuery();

                            MessageBox.Show("Kayıt başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Varsayilan();
                            baglan.baglanti().Close();
                        }
                        
                    }
                    

                }
                else if (Cmb_Kayit_Tipi.Text=="Antrenör")
                {
                    SqlCommand kakontrol = new SqlCommand("Select AntKullaniciAdi from AntrenorBilgi where AntKullaniciAdi=@p1", baglan.baglanti());
                    kakontrol.Parameters.AddWithValue("@p1", Txt_KullaniciAdi.Text);
                    SqlDataReader dr = kakontrol.ExecuteReader();
                    if (dr.Read())
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        IDKontrol(); // ID ÜRETİLEN YER
                        SqlCommand kayit6 = new SqlCommand("Insert into AntrenorBilgi (AntID,AntAdi,AntSoyadi,AntKullaniciAdi,AntSifre,AntIletisim,AntKayitTarihi) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", baglan.baglanti());
                        kayit6.Parameters.AddWithValue("@p1", Txt_ID.Text);
                        kayit6.Parameters.AddWithValue("@p2", Txt_Ad.Text);
                        kayit6.Parameters.AddWithValue("@p3", Txt_Soyad.Text);
                        kayit6.Parameters.AddWithValue("@p4", Txt_KullaniciAdi.Text);
                        kayit6.Parameters.AddWithValue("@p5", Txt_Sifre.Text);
                        kayit6.Parameters.AddWithValue("@p6", Msk_Iletisim.Text);
                        kayit6.Parameters.AddWithValue("@p7", Date_Tarih.Value.ToString("dd.MM.yyyy"));
                        kayit6.ExecuteNonQuery();
                        SqlCommand kayit7 = new SqlCommand("Insert into GuvenlikBilgi (ID,KayitTipi,GuvenlikSoru,GuvenlikCevap) values (@p1,@p2,@p3,@p4)", baglan.baglanti());
                        kayit7.Parameters.AddWithValue("@p1", Txt_ID.Text);
                        kayit7.Parameters.AddWithValue("@p2", Cmb_Kayit_Tipi.Text);
                        kayit7.Parameters.AddWithValue("@p3", Cmb_Guvenlik_Sorusu.Text);
                        kayit7.Parameters.AddWithValue("@p4", Txt_Guvenlik_Cevap.Text);
                        kayit7.ExecuteNonQuery();
                        MessageBox.Show("Kayıt başarılı", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Varsayilan();
                        baglan.baglanti().Close();
                    }
                    

                }
               
            }
        }

        public void OrtakDisabled() //ANTRENÖR VEYA ADMİN ÇİFT CLİCK EDİLMİŞ İSE ENABLED ÖZELLİĞİ FALSE OLACAK YERLER VE KAYIT OL BUTONU AKTİF
        {
            Msk_DogumTarihi.Enabled = false;
            Cmb_Cinsiyet.Enabled = false;
            Cmb_Meslek.Enabled = false;
            Grp_Olculer.Enabled = false;
            Grp_Hedefler.Enabled = false;
            Grp_Yetkili.Enabled = false;
        }
        public void UyeDisabled()
        {
            Msk_DogumTarihi.Enabled = true;
            Cmb_Cinsiyet.Enabled = true;
            Cmb_Meslek.Enabled = true;
            Grp_Hedefler.Enabled = true;
            Grp_Olculer.Enabled = true;
            Btn_KayıtOl.Enabled = true;
            Btn_Ara.Enabled = true;
        }

        private void Cmb_Kayit_Tipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Temizle();
            Txt_ID.ReadOnly = false;
            Btn_Ara.Enabled = true;
            Txt_ID.Enabled = true;
            Cmb_Yetkili.SelectedIndex = -1;
            Lbl_Listele_Uyari.Visible = false;
            if (Cmb_Kayit_Tipi.Text == "Admin") //ADMİN SEÇİLİYKEN AÇILAN YERLER
            {
                OrtakAlanlar();
                OrtakDisabled();   
                Btn_Guncelle.Enabled = false;
                Btn_Sil.Enabled = false;
                Btn_KayıtOl.Enabled = true;
                Date_Tarih.Enabled = false;
                Lbl_Antrenor.Text = "Antrenör";
                Lbl_Hedefler.Text = "Hedefler";
            }
            else if (Cmb_Kayit_Tipi.Text == "Üye" || Cmb_Kayit_Tipi.Text == "Özel Üye")//ÜYE VEYA ÖZEL ÜYE SEÇİLİYKEN AÇILAN YERLER
            {
                Cmb_Yetkili.Items.Clear();
                SqlCommand komut = new SqlCommand("Select (AntAdi+' '+AntSoyadi) as Ad_Soyad from AntrenorBilgi", baglan.baglanti());
                SqlDataReader dr;
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    Cmb_Yetkili.Items.Add(dr["Ad_Soyad"]);
                }               
                OrtakAlanlar();
                switch (Cmb_Kayit_Tipi.Text) //YETKİLİ ENABLED
                {
                    case "Üye":
                        Grp_Yetkili.Enabled = false;
                        Lbl_Antrenor.Text = "Antrenör";
                        Lbl_Hedefler.Text = "Hedefler*";
                        break;
                    case "Özel Üye":
                        Grp_Yetkili.Enabled = true;
                        Lbl_Antrenor.Text = "Antrenör*";
                        Lbl_Hedefler.Text = "Hedefler*";
                        break;
                }
                UyeDisabled();
                Btn_Guncelle.Enabled = false;
                Btn_Sil.Enabled = false;
                Btn_KayıtOl.Enabled = true;
                Date_Tarih.Enabled = true;

            }
            else if (Cmb_Kayit_Tipi.Text=="Antrenör")//ANTRENÖR SEÇİLİYKEN AÇILAN YERLER
            {
                OrtakAlanlar();
                OrtakDisabled();
                Btn_Guncelle.Enabled = false;
                Btn_Sil.Enabled = false;
                Btn_KayıtOl.Enabled = true;
                Date_Tarih.Enabled = true;
                Lbl_Antrenor.Text = "Antrenör";
                Lbl_Hedefler.Text = "Hedefler";
            }
        }
        public string sorgu;
        public void DataGrid()
        {
            
            SqlCommand Listele = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglan.baglanti());
            da.Fill(dt);
            Dt_Kayitlar.DataSource = dt;
        }


        private void Btn_Listele_Click_1(object sender, EventArgs e)
        {
            if (Cmb_Kayit_Tipi.SelectedIndex==-1)
            {
                Lbl_Listele_Uyari.Visible = true;
            }
            else
            {
                Cmb_Kayit_Tipi.Enabled = false;
                Lbl_Listele_Uyari.Visible = false;
                if (Cmb_Kayit_Tipi.Text == "Üye")
                {
                    Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                    sorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeKullaniciAdi) as Kullanıcı_Adı,(ub.UyeSifre) as Şifre,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(gb.GuvenlikSoru) as Güvenlik_Sorusu,(gb.GuvenlikCevap) as Güvenlik_Cevap,(ub.UyeYetkili) as Yetkili,(ub.UyeKayitTarihi) as Kayıt_Tarihi,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join GuvenlikBilgi gb on ub.UyeID=gb.ID full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where UyeYetkili=''";
                    DataGrid();

                }


                else if (Cmb_Kayit_Tipi.Text == "Özel Üye")
                {
                    Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                    sorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeKullaniciAdi) as Kullanıcı_Adı,(ub.UyeSifre) as Şifre,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(gb.GuvenlikSoru) as Güvenlik_Sorusu,(gb.GuvenlikCevap) as Güvenlik_Cevap,(ub.UyeYetkili) as Yetkili,(ub.UyeKayitTarihi) as Kayıt_Tarihi,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join GuvenlikBilgi gb on ub.UyeID=gb.ID full outer join UyeOlculer uo on ub.UyeID=uo.UyeID  where UyeYetkili LIKE '%_%'";
                    DataGrid();

                }
                if (Cmb_Kayit_Tipi.Text == "Özel Üye" & Cmb_Yetkili.SelectedIndex > -1)
                {
                    Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                    sorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeKullaniciAdi) as Kullanıcı_Adı,(ub.UyeSifre) as Şifre,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(gb.GuvenlikSoru) as Güvenlik_Sorusu,(gb.GuvenlikCevap) as Güvenlik_Cevap,(ub.UyeYetkili) as Yetkili,(ub.UyeKayitTarihi) as Kayıt_Tarihi,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join GuvenlikBilgi gb on ub.UyeID=gb.ID full outer join UyeOlculer uo on ub.UyeID=uo.UyeID  where UyeYetkili='" + Cmb_Yetkili.Text + "'";
                    DataGrid();
                }
                if (Cmb_Kayit_Tipi.Text == "Admin")
                {
                    Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                    sorgu = "Select AdminID as ID, AdminAdi as Adı, AdminSoyadi as Soyadı,AdminKullaniciAdi as Kullanıcı_Adı,AdminSifre as Şifre,AdminIletisim as İletişim,gb.GuvenlikSoru as Güvenlik_Soru,gb.GuvenlikCevap as Güvenlik_Cevap from AdminBilgi ab full outer join GuvenlikBilgi gb on ab.AdminID=gb.ID where gb.KayitTipi='Admin'";
                    DataGrid();
                }
                if (Cmb_Kayit_Tipi.Text == "Antrenör")
                {
                    Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                    sorgu = "Select AntID as ID, AntAdi as Adı, AntSoyadi as Soyadı, AntKullaniciAdi as Kullanıcı_Adı, AntSifre as Şifre, AntIletisim as İletişim,AntKayitTarihi as Kayıt_Tarihi,gb.GuvenlikSoru as Güvenlik_Soru,gb.GuvenlikCevap as Güvenlik_Cevap  from  AntrenorBilgi ant full outer join GuvenlikBilgi gb on ant.AntID=gb.ID where gb.KayitTipi='Antrenör' ";
                    DataGrid();
                }
                baglan.baglanti().Close();
            }
        }

        

        private void Btn_Ara_Click(object sender, EventArgs e)
        {
            if (Cmb_Kayit_Tipi.Text == "Üye")
            {
                Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                sorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeKullaniciAdi) as Kullanıcı_Adı,(ub.UyeSifre) as Şifre,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(gb.GuvenlikSoru) as Güvenlik_Sorusu,(gb.GuvenlikCevap) as Güvenlik_Cevap,(ub.UyeYetkili) as Yetkili,(ub.UyeKayitTarihi) as Kayıt_Tarihi,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join GuvenlikBilgi gb on ub.UyeID=gb.ID full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where ub.UyeID='" + Txt_ID.Text + "'";
                DataGrid();

            }


            else if (Cmb_Kayit_Tipi.Text == "Özel Üye")
            {
                Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                sorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeKullaniciAdi) as Kullanıcı_Adı,(ub.UyeSifre) as Şifre,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(gb.GuvenlikSoru) as Güvenlik_Sorusu,(gb.GuvenlikCevap) as Güvenlik_Cevap,(ub.UyeYetkili) as Yetkili,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join GuvenlikBilgi gb on ub.UyeID=gb.ID full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where ub.UyeID='" + Txt_ID.Text + "'";
                DataGrid();

            }
            
            if (Cmb_Kayit_Tipi.Text == "Admin")
            {
                Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                sorgu = "Select AdminID as ID, AdminAdi as Adı, AdminSoyadi as Soyadı,AdminKullaniciAdi as Kullanıcı_Adı,AdminSifre as Şifre,AdminIletisim as İletişim,gb.GuvenlikSoru as Güvenlik_Soru,gb.GuvenlikCevap as Güvenlik_Cevap from AdminBilgi ab full outer join GuvenlikBilgi gb on ab.AdminID=gb.ID where AdminID='" + Txt_ID.Text + "'";
                DataGrid();
            }
            if (Cmb_Kayit_Tipi.Text == "Antrenör")
            {
                Dt_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
                sorgu = "Select AntID as ID, AntAdi as Adı, AntSoyadi as Soyadı, AntKullaniciAdi as Kullanıcı_Adı, AntSifre as Şifre, AntIletisim as İletişim,AntKayitTarihi as Kayıt_Tarihi,gb.GuvenlikSoru as Güvenlik_Soru,gb.GuvenlikCevap as Güvenlik_Cevap  from  AntrenorBilgi ant full outer join GuvenlikBilgi gb on ant.AntID=gb.ID where AntID='" + Txt_ID.Text + "'";
                DataGrid();
            }
            baglan.baglanti().Close();
        }

     

        private void Btn_Temizle_Click(object sender, EventArgs e)
        {
            Varsayilan();
        }
        public int secilen;

        public void UyeCagir() // ÜYE BİLGİLERİNİ GETİRME
        {
            secilen= Dt_Kayitlar.SelectedCells[0].RowIndex;
            Txt_ID.Text = Dt_Kayitlar.Rows[secilen].Cells[0].Value.ToString();
            Txt_Ad.Text = Dt_Kayitlar.Rows[secilen].Cells[1].Value.ToString();
            Txt_Soyad.Text = Dt_Kayitlar.Rows[secilen].Cells[2].Value.ToString();
            Txt_KullaniciAdi.Text = Dt_Kayitlar.Rows[secilen].Cells[3].Value.ToString();
            Txt_Sifre.Text = Dt_Kayitlar.Rows[secilen].Cells[4].Value.ToString();
            Msk_DogumTarihi.Text = Dt_Kayitlar.Rows[secilen].Cells[5].Value.ToString();
            Cmb_Meslek.Text = Dt_Kayitlar.Rows[secilen].Cells[6].Value.ToString();
            Msk_Iletisim.Text = Dt_Kayitlar.Rows[secilen].Cells[7].Value.ToString();
            Cmb_Cinsiyet.Text = Dt_Kayitlar.Rows[secilen].Cells[8].Value.ToString();
            Cmb_Guvenlik_Sorusu.Text = Dt_Kayitlar.Rows[secilen].Cells[9].Value.ToString();
            Txt_Guvenlik_Cevap.Text = Dt_Kayitlar.Rows[secilen].Cells[10].Value.ToString();
            //Cmb_Yetkili.Text= Dt_Kayitlar.Rows[secilen].Cells[11].Value.ToString();
            //Date_Tarih.Value = Convert.ToDateTime(Dt_Kayitlar.Rows[secilen].Cells[12].Value);
            Cmb_Hedefler.Text = Dt_Kayitlar.Rows[secilen].Cells[13].Value.ToString();
            Msk_Omuz.Text = Dt_Kayitlar.Rows[secilen].Cells[14].Value.ToString();
            Msk_Gogus.Text = Dt_Kayitlar.Rows[secilen].Cells[15].Value.ToString();
            Msk_Bel.Text = Dt_Kayitlar.Rows[secilen].Cells[16].Value.ToString();
            Msk_Kalca.Text = Dt_Kayitlar.Rows[secilen].Cells[17].Value.ToString();
            Msk_Baldir.Text = Dt_Kayitlar.Rows[secilen].Cells[18].Value.ToString();
            Msk_Kol.Text = Dt_Kayitlar.Rows[secilen].Cells[19].Value.ToString();
            Msk_Boy.Text = Dt_Kayitlar.Rows[secilen].Cells[20].Value.ToString();
            Msk_Kilo.Text = Dt_Kayitlar.Rows[secilen].Cells[21].Value.ToString();
            Cmb_Soru1.Text = Dt_Kayitlar.Rows[secilen].Cells[22].Value.ToString();
            Rch_Soru1_Cevap.Text = Dt_Kayitlar.Rows[secilen].Cells[23].Value.ToString();
            Cmb_Soru2.Text = Dt_Kayitlar.Rows[secilen].Cells[24].Value.ToString();
            Rch_Soru2_Cevap.Text = Dt_Kayitlar.Rows[secilen].Cells[25].Value.ToString();
        }

        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            switch (Cmb_Kayit_Tipi.Text)
            {
                case "Üye":  //ÜYE GÜNCELLEME YERİ 
                    SqlCommand guncelle1 = new SqlCommand("Update UyeBilgi set UyeAdi=@p1,UyeSoyadi=@p2,UyeKullaniciAdi=@p3,UyeSifre=@p4,UyeDogumTarihi=@p5,UyeMeslek=@p6,UyeIletisim=@p7,UyeCinsiyet=@p8,UyeHedef=@p9,UyeKayitTarihi=@p10 where UyeID=@p11", baglan.baglanti());                                
                    guncelle1.Parameters.AddWithValue("@p1", Txt_Ad.Text);
                    guncelle1.Parameters.AddWithValue("@p2", Txt_Soyad.Text);
                    guncelle1.Parameters.AddWithValue("@p3", Txt_KullaniciAdi.Text);
                    guncelle1.Parameters.AddWithValue("@p4", Txt_Sifre.Text);
                    guncelle1.Parameters.AddWithValue("@p5", Msk_DogumTarihi.Text);
                    guncelle1.Parameters.AddWithValue("@p6", Cmb_Meslek.Text);
                    guncelle1.Parameters.AddWithValue("@p7", Msk_Iletisim.Text);
                    guncelle1.Parameters.AddWithValue("@p8", Cmb_Cinsiyet.Text);
                    guncelle1.Parameters.AddWithValue("@p9", Cmb_Hedefler.Text);
                    guncelle1.Parameters.AddWithValue("@p10", Date_Tarih.Value.ToString("dd.MM.yyyy"));
                    guncelle1.Parameters.AddWithValue("@p11",Txt_ID.Text);
                    guncelle1.ExecuteNonQuery();
                    SqlCommand guncelle2 = new SqlCommand("Update UyeOlculer set Omuz=@p1,Gogus=@p2,Bel=@p3,Kalca=@p4,Baldir=@p5,Kol=@p6,Boy=@p7,Kilo=@p8,Soru1=@p9,Soru1Cevap=@p10,Soru2=@p11,Soru2Cevap=@p12 where UyeID=@p13", baglan.baglanti());                 
                    guncelle2.Parameters.AddWithValue("@p1", Msk_Omuz.Text);
                    guncelle2.Parameters.AddWithValue("@p2", Msk_Gogus.Text);
                    guncelle2.Parameters.AddWithValue("@p3", Msk_Bel.Text);
                    guncelle2.Parameters.AddWithValue("@p4", Msk_Kalca.Text);
                    guncelle2.Parameters.AddWithValue("@p5", Msk_Baldir.Text);
                    guncelle2.Parameters.AddWithValue("@p6", Msk_Kol.Text);
                    guncelle2.Parameters.AddWithValue("@p7", Msk_Boy.Text);
                    guncelle2.Parameters.AddWithValue("@p8", Msk_Kilo.Text);
                    guncelle2.Parameters.AddWithValue("@p9", Cmb_Soru1.Text);
                    guncelle2.Parameters.AddWithValue("@p10", Rch_Soru1_Cevap.Text);
                    guncelle2.Parameters.AddWithValue("@p11", Cmb_Soru2.Text);
                    guncelle2.Parameters.AddWithValue("@p12", Rch_Soru2_Cevap.Text);
                    guncelle2.Parameters.AddWithValue("@p13", Txt_ID.Text);
                    guncelle2.ExecuteNonQuery();
                    SqlCommand guncelle3 = new SqlCommand("Update GuvenlikBilgi set GuvenlikSoru=@p1,GuvenlikCevap=@p2 where ID=@p3 ", baglan.baglanti()); 
                    guncelle3.Parameters.AddWithValue("@p1", Cmb_Guvenlik_Sorusu.Text);
                    guncelle3.Parameters.AddWithValue("@p2", Txt_Guvenlik_Cevap.Text);
                    guncelle3.Parameters.AddWithValue("@p3", Txt_ID.Text);
                    guncelle3.ExecuteNonQuery();                   
                    break;

                case "Özel Üye":  //ÖZEL ÜYE GÜNCELLEME YERİ
                    SqlCommand guncelle4 = new SqlCommand("Update UyeBilgi set UyeAdi=@p1,UyeSoyadi=@p2,UyeKullaniciAdi=@p3,UyeSifre=@p4,UyeDogumTarihi=@p5,UyeMeslek=@p6,UyeIletisim=@p7,UyeCinsiyet=@p8,UyeHedef=@p9,UyeYetkili=@p10,UyeKayitTarihi=@p11 where UyeID=@p12", baglan.baglanti());
                    guncelle4.Parameters.AddWithValue("@p1", Txt_Ad.Text);
                    guncelle4.Parameters.AddWithValue("@p2", Txt_Soyad.Text);
                    guncelle4.Parameters.AddWithValue("@p3", Txt_KullaniciAdi.Text);
                    guncelle4.Parameters.AddWithValue("@p4", Txt_Sifre.Text);
                    guncelle4.Parameters.AddWithValue("@p5", Msk_DogumTarihi.Text);
                    guncelle4.Parameters.AddWithValue("@p6", Cmb_Meslek.Text);
                    guncelle4.Parameters.AddWithValue("@p7", Msk_Iletisim.Text);
                    guncelle4.Parameters.AddWithValue("@p8", Cmb_Cinsiyet.Text);
                    guncelle4.Parameters.AddWithValue("@p9", Cmb_Hedefler.Text);
                    guncelle4.Parameters.AddWithValue("@p10", Cmb_Yetkili.Text);
                    guncelle4.Parameters.AddWithValue("@p11", Date_Tarih.Value.ToString("dd.MM.yyyy"));
                    guncelle4.Parameters.AddWithValue("@p12", Txt_ID.Text);
                    guncelle4.ExecuteNonQuery();
                    SqlCommand guncelle5 = new SqlCommand("Update UyeOlculer set Omuz=@p1,Gogus=@p2,Bel=@p3,Kalca=@p4,Baldir=@p5,Kol=@p6,Boy=@p7,Kilo=@p8,Soru1=@p9,Soru1Cevap=@p10,Soru2=@p11,Soru2Cevap=@p12 where UyeID=@p13", baglan.baglanti());
                    guncelle5.Parameters.AddWithValue("@p1", Msk_Omuz.Text);
                    guncelle5.Parameters.AddWithValue("@p2", Msk_Gogus.Text);
                    guncelle5.Parameters.AddWithValue("@p3", Msk_Bel.Text);
                    guncelle5.Parameters.AddWithValue("@p4", Msk_Kalca.Text);
                    guncelle5.Parameters.AddWithValue("@p5", Msk_Baldir.Text);
                    guncelle5.Parameters.AddWithValue("@p6", Msk_Kol.Text);
                    guncelle5.Parameters.AddWithValue("@p7", Msk_Boy.Text);
                    guncelle5.Parameters.AddWithValue("@p8", Msk_Kilo.Text);
                    guncelle5.Parameters.AddWithValue("@p9", Cmb_Soru1.Text);
                    guncelle5.Parameters.AddWithValue("@p10", Rch_Soru1_Cevap.Text);
                    guncelle5.Parameters.AddWithValue("@p11", Cmb_Soru2.Text);
                    guncelle5.Parameters.AddWithValue("@p12", Rch_Soru2_Cevap.Text);
                    guncelle5.Parameters.AddWithValue("@p13", Txt_ID.Text);
                    guncelle5.ExecuteNonQuery();
                    SqlCommand guncelle6 = new SqlCommand("Update GuvenlikBilgi set GuvenlikSoru=@p1,GuvenlikCevap=@p2 where ID=@p3", baglan.baglanti());
                    guncelle6.Parameters.AddWithValue("@p1", Cmb_Guvenlik_Sorusu.Text);
                    guncelle6.Parameters.AddWithValue("@p2", Txt_Guvenlik_Cevap.Text);
                    guncelle6.Parameters.AddWithValue("@p3", Txt_ID.Text);
                    guncelle6.ExecuteNonQuery();                   
                    break;

                case "Admin": //ADMİN GÜNCELLEME YERİ
                    SqlCommand guncelle7 = new SqlCommand("Update AdminBilgi set AdminAdi=@p1,AdminSoyadi=@p2,AdminKullaniciAdi=@p3,AdminSifre=@p4,AdminIletisim=@p5 where AdminID=@p6",baglan.baglanti());
                    guncelle7.Parameters.AddWithValue("@p1", Txt_Ad.Text);
                    guncelle7.Parameters.AddWithValue("@p2", Txt_Soyad.Text);
                    guncelle7.Parameters.AddWithValue("@p3", Txt_KullaniciAdi.Text);
                    guncelle7.Parameters.AddWithValue("@p4",Txt_Sifre.Text);    
                    guncelle7.Parameters.AddWithValue("@p5", Msk_Iletisim.Text);
                    guncelle7.Parameters.AddWithValue("@p6", Txt_ID.Text);
                    guncelle7.ExecuteNonQuery();
                    SqlCommand guncelle8 = new SqlCommand("Update GuvenlikBilgi set GuvenlikSoru=@p1,GuvenlikCevap=@p2 where ID=@p3", baglan.baglanti());
                    guncelle8.Parameters.AddWithValue("@p1",Cmb_Guvenlik_Sorusu.Text);
                    guncelle8.Parameters.AddWithValue("@p2", Txt_Guvenlik_Cevap.Text);
                    guncelle8.Parameters.AddWithValue("@p3", Txt_ID.Text);
                    guncelle8.ExecuteNonQuery();
                    break;

                case "Antrenör": //ANTRENÖR GÜNCELLEME YERİ
                    SqlCommand guncelle9 = new SqlCommand("Update AntrenorBilgi set AntAdi=@p1,AntSoyadi=@p2,AntKullaniciAdi=@p3,AntSifre=@p4,AntIletisim=@p5,AntKayitTarihi=@p6 where AntID=@p7 ",baglan.baglanti());
                    guncelle9.Parameters.AddWithValue("@p1", Txt_Ad.Text);
                    guncelle9.Parameters.AddWithValue("@p2", Txt_Soyad.Text);
                    guncelle9.Parameters.AddWithValue("@p3", Txt_KullaniciAdi.Text);
                    guncelle9.Parameters.AddWithValue("@p4", Txt_Sifre.Text);
                    guncelle9.Parameters.AddWithValue("@p5", Msk_Iletisim.Text);
                    guncelle9.Parameters.AddWithValue("@p6", Date_Tarih.Value.ToString("dd.MM.yyyy"));
                    guncelle9.Parameters.AddWithValue("@p7", Txt_ID.Text);
                    guncelle9.ExecuteNonQuery();
                    SqlCommand guncelle10 = new SqlCommand("Update GuvenlikBilgi set GuvenlikSoru=@p1,GuvenlikCevap=@p2 where ID=@p3", baglan.baglanti());
                    guncelle10.Parameters.AddWithValue("@p1", Cmb_Guvenlik_Sorusu.Text);
                    guncelle10.Parameters.AddWithValue("@p2", Txt_Guvenlik_Cevap.Text);
                    guncelle10.Parameters.AddWithValue("@p3", Txt_ID.Text);
                    guncelle10.ExecuteNonQuery();
                    break;
            }
            baglan.baglanti().Close();
            Varsayilan();
            MessageBox.Show("Kayıt Güncellendi!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);  //GÜNCELLEME BAŞARILI DİALOĞU

        }   

        private void Dt_Kayitlar_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Btn_KayıtOl.Enabled = false;
            Txt_ID.ReadOnly = true;
            switch (Cmb_Kayit_Tipi.Text)
            {
                case "Üye":
                    UyeCagir();
                    UyeDisabled();
                    Btn_Guncelle.Enabled = true;
                    Btn_Sil.Enabled = true;
                    Grp_Yetkili.Enabled = false;
                    Btn_Listele.Enabled = false;
                    Btn_KayıtOl.Enabled = false;
                    Date_Tarih.Value = Convert.ToDateTime(Dt_Kayitlar.Rows[secilen].Cells[12].Value);
                    break;
                case "Özel Üye":
                    UyeCagir();
                    UyeDisabled();
                    Btn_Guncelle.Enabled = true;
                    Btn_Sil.Enabled = true;
                    Grp_Yetkili.Enabled = true;
                    Cmb_Yetkili.Text = Dt_Kayitlar.Rows[secilen].Cells[11].Value.ToString();
                    Btn_KayıtOl.Enabled = false;
                    Btn_Listele.Enabled = false;
                    Date_Tarih.Value = Convert.ToDateTime(Dt_Kayitlar.Rows[secilen].Cells[12].Value);
                    break;
                case "Admin":
                    //OrtakCagir();
                    secilen = Dt_Kayitlar.SelectedCells[0].RowIndex;
                    Txt_ID.Text = Dt_Kayitlar.Rows[secilen].Cells[0].Value.ToString();
                    Txt_Ad.Text = Dt_Kayitlar.Rows[secilen].Cells[1].Value.ToString();
                    Txt_Soyad.Text = Dt_Kayitlar.Rows[secilen].Cells[2].Value.ToString();
                    Txt_KullaniciAdi.Text = Dt_Kayitlar.Rows[secilen].Cells[3].Value.ToString();
                    Txt_Sifre.Text = Dt_Kayitlar.Rows[secilen].Cells[4].Value.ToString();
                    Msk_Iletisim.Text = Dt_Kayitlar.Rows[secilen].Cells[5].Value.ToString();
                    Cmb_Guvenlik_Sorusu.Text = Dt_Kayitlar.Rows[secilen].Cells[6].Value.ToString();
                    Txt_Guvenlik_Cevap.Text = Dt_Kayitlar.Rows[secilen].Cells[7].Value.ToString();
                    OrtakDisabled();
                    Btn_Guncelle.Enabled = true;
                    Btn_Sil.Enabled = true;
                    Btn_KayıtOl.Enabled = false;
                    Btn_Listele.Enabled = false;                 
                    break;
                case "Antrenör":
                    secilen = Dt_Kayitlar.SelectedCells[0].RowIndex;
                    Txt_ID.Text = Dt_Kayitlar.Rows[secilen].Cells[0].Value.ToString();
                    Txt_Ad.Text = Dt_Kayitlar.Rows[secilen].Cells[1].Value.ToString();
                    Txt_Soyad.Text = Dt_Kayitlar.Rows[secilen].Cells[2].Value.ToString();
                    Txt_KullaniciAdi.Text = Dt_Kayitlar.Rows[secilen].Cells[3].Value.ToString();
                    Txt_Sifre.Text = Dt_Kayitlar.Rows[secilen].Cells[4].Value.ToString();
                    Msk_Iletisim.Text = Dt_Kayitlar.Rows[secilen].Cells[5].Value.ToString();
                    Date_Tarih.Value = Convert.ToDateTime(Dt_Kayitlar.Rows[secilen].Cells[6].Value);
                    Cmb_Guvenlik_Sorusu.Text = Dt_Kayitlar.Rows[secilen].Cells[7].Value.ToString();
                    Txt_Guvenlik_Cevap.Text = Dt_Kayitlar.Rows[secilen].Cells[8].Value.ToString();
                    OrtakDisabled();
                    Btn_Guncelle.Enabled = true;
                    Btn_Sil.Enabled = true;
                    Btn_KayıtOl.Enabled = false;
                    Btn_Listele.Enabled = false;
                    
                  
                    break;
                   
            }
        }

        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            switch (Cmb_Kayit_Tipi.Text)
            {
                case "Üye":  //ÜYE SİLME YERİ 
                    SqlCommand sil1 = new SqlCommand("Delete  from UyeBilgi where UyeID=@p1", baglan.baglanti());
                    sil1.Parameters.AddWithValue("@p1",Txt_ID.Text);
                    sil1.ExecuteNonQuery();
                    SqlCommand sil2 = new SqlCommand("Delete  from UyeOlculer where UyeID=@p1", baglan.baglanti());
                    sil2.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil2.ExecuteNonQuery();
                    SqlCommand sil3 = new SqlCommand("Delete  from GuvenlikBilgi where ID=@p1", baglan.baglanti());
                    sil3.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil3.ExecuteNonQuery();
                    SqlCommand sil11 = new SqlCommand("Delete from OdemePlani where UyeID=@p1", baglan.baglanti());
                    sil11.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil11.ExecuteNonQuery();
                    break;

                case "Özel Üye":  //ÖZEL ÜYE SİLME YERİ
                    SqlCommand sil4 = new SqlCommand("Delete  from UyeBilgi where UyeID=@p1", baglan.baglanti());
                    sil4.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil4.ExecuteNonQuery();
                    SqlCommand sil5 = new SqlCommand("Delete  from UyeOlculer where UyeID=@p1", baglan.baglanti());
                    sil5.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil5.ExecuteNonQuery();
                    SqlCommand sil6 = new SqlCommand("Delete  from GuvenlikBilgi where ID=@p1", baglan.baglanti());
                    sil6.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil6.ExecuteNonQuery();
                    SqlCommand sil12 = new SqlCommand("Delete from OdemePlani where UyeID=@p1", baglan.baglanti());
                    sil12.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil12.ExecuteNonQuery();
                    break;

                case "Admin": //ADMİN SİLME YERİ
                    SqlCommand sil7 = new SqlCommand("Delete  from AdminBilgi where AdminID=@p1", baglan.baglanti());
                    sil7.Parameters.AddWithValue("@p1",Txt_ID.Text);
                    sil7.ExecuteNonQuery();
                    SqlCommand sil8 = new SqlCommand("Delete  from GuvenlikBilgi where ID=@p1", baglan.baglanti());
                    sil8.Parameters.AddWithValue("@p1",Txt_ID.Text);
                    sil8.ExecuteNonQuery();
                    break;

                case "Antrenör": //ANTRENÖR SİLME YERİ
                    SqlCommand sil9 = new SqlCommand("Delete  from AntrenorBilgi where AntID=@p1", baglan.baglanti());
                    sil9.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil9.ExecuteNonQuery();
                    SqlCommand sil10 = new SqlCommand("Delete  from GuvenlikBilgi where ID=@p1", baglan.baglanti());
                    sil10.Parameters.AddWithValue("@p1", Txt_ID.Text);
                    sil10.ExecuteNonQuery();
                    break;
            }
            baglan.baglanti().Close();
            Varsayilan();
            MessageBox.Show("Kayıt Silindi!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);  //SİLME BAŞARILI DİALOĞU
        }

        

     

     

        private void AdminPaneli_FormClosing(object sender, FormClosingEventArgs e)
        {
            BW_Admin_Frm_Closing.RunWorkerAsync();
            //AntrenorPaneli ant = new AntrenorPaneli();
            //ant.Name = "AntrenorPaneli";
            //UyePaneli up = new UyePaneli();
            //up.Name = "UyePaneli";
            //HareketEkrani hrk = new HareketEkrani();
            //hrk.Name = "HareketEkrani";
            //if (Application.OpenForms[ant.Name] == null && Application.OpenForms[up.Name] == null && Application.OpenForms[hrk.Name] == null)
            //{
            //    Environment.Exit(0);
            //}
            //else
            //{
            //    Form.ActiveForm.Hide();
            //}


        }
        
        

        private void Msk_Iletisim_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Msk_Iletisim.Text== " (   )     ")
            {
                Msk_Iletisim.SelectionStart = 0;
            }
            
        }

        private void Msk_DogumTarihi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Msk_DogumTarihi.Text == "  -  -")
            {
                Msk_DogumTarihi.SelectionStart = 0;
            }
        }

        private void Txt_Ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar)
                   && !char.IsControl(e.KeyChar)
                   && !char.IsSeparator(e.KeyChar);
        }

        private void Txt_Soyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar)
                   && !char.IsControl(e.KeyChar)
                   && !char.IsSeparator(e.KeyChar);
        }

        private void Btn_Adm_Ayarlar_Click(object sender, EventArgs e)
        {
          Ayarlar ay  = new Ayarlar();
           ay.ShowDialog();
        }

        private void Btn_Istatistik_Click(object sender, EventArgs e)
        {
            Istatistikler ist = new Istatistikler();
            ist.ShowDialog();
        }

        private void Msk_Omuz_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Omuz.Text == "    cm")
            {
                Msk_Omuz.SelectionStart = 0;
            }
        }

        private void Msk_Baldir_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Baldir.Text == "    cm")
            {
                Msk_Baldir.SelectionStart = 0;
            }
        }

        private void Msk_Bel_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Bel.Text == "    cm")
            {
                Msk_Bel.SelectionStart = 0;
            }
        }

        private void Msk_Boy_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Boy.Text == "    cm")
            {
                Msk_Boy.SelectionStart = 0;
            }
        }

        private void Msk_Gogus_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Gogus.Text == "    cm")
            {
                Msk_Gogus.SelectionStart = 0;
            }
        }

        private void Msk_Kol_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Kol.Text == "    cm")
            {
                Msk_Kol.SelectionStart = 0;
            }
        }

        private void Msk_Kalca_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Kalca.Text == "    cm")
            {
                Msk_Kalca.SelectionStart = 0;
            }
        }

        private void Msk_Kilo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Msk_Kilo.Text == "    cm")
            {
                Msk_Kilo.SelectionStart = 0;
            }
        }

        private void Btn_Odeme_Plani_Click(object sender, EventArgs e)
        {
            OdemeEkrani odeme = new OdemeEkrani();
            odeme.ShowDialog();
        }

        
        private void BW_Admin_Frm_Closing_DoWork(object sender, DoWorkEventArgs e)
        {
            AntrenorPaneli ant = new AntrenorPaneli();
            ant.Name = "AdminPaneli";
            HareketEkrani hrk = new HareketEkrani();
            hrk.Name = "HareketEkrani";
            OzelUyePaneli oup = new OzelUyePaneli();
            oup.Name = "OzelUyePaneli";
            UyePaneli up = new UyePaneli();
            up.Name = "UyePaneli";
            if (Application.OpenForms[ant.Name] == null && BtnAnaMenuClick == true)
            {

            }
            else if (Application.OpenForms[ant.Name] == null && Application.OpenForms[hrk.Name] == null && Application.OpenForms[oup.Name] == null && Application.OpenForms[up.Name] == null)
            { 
               Environment.Exit(0);
            }
        }
    }
}
