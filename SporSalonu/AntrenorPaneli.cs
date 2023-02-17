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


namespace SporSalonu
{
    public partial class AntrenorPaneli : KryptonForm
    {
        public AntrenorPaneli()
        {
            InitializeComponent();
        }

       
        SQLBaglanti baglan = new SQLBaglanti();

        public string antkullaniciadi;
        private void AntrenorPaneli_Load(object sender, EventArgs e) //ANTRENÖR PANELİ 
        {
            CheckForIllegalCrossThreadCalls = false;
            Lbl_Giris_Yapanin_Adi_Ant.Text = antkullaniciadi;
            SqlCommand komut = new SqlCommand("Select (AntAdi+' '+AntSoyadi) as Ad_Soyad from AntrenorBilgi where AntKullaniciAdi=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", antkullaniciadi);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Lbl_Giris_Yapanin_Adi_Ant.Text = dr[0].ToString();
            }

            SqlCommand tuye = new SqlCommand("Select count(*) from UyeBilgi where UyeYetkili=@p1", baglan.baglanti());
            tuye.Parameters.AddWithValue("@p1", Lbl_Giris_Yapanin_Adi_Ant.Text);
            SqlDataReader dr2 = tuye.ExecuteReader();
            while (dr2.Read())
            {
                Txt_Ant_Toplam_Uye.Text = dr2[0].ToString();
            }
        }
        public string antsorgu;
        public void DataGrid() //DATAGRIDVIEW METODU
        {

            SqlCommand Listele = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(antsorgu, baglan.baglanti());
            da.Fill(dt);
            Dt_Antrenor_Kayitlar.DataSource = dt;
        }
        private void Btn_Listele_Click(object sender, EventArgs e)//LİSTELE
        {
          
            if (Cmb_Ant_Kayit_Tipi.Text == "Üye")
            {
                Dt_Antrenor_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                antsorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(ub.UyeYetkili) as Yetkili,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where UyeYetkili=''";
                DataGrid();

            }
            else if (Cmb_Ant_Kayit_Tipi.Text == "Özel Üye")
            {
                Dt_Antrenor_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                antsorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(ub.UyeYetkili) as Yetkili,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where UyeYetkili='" + Lbl_Giris_Yapanin_Adi_Ant.Text + "'";
                DataGrid();
            }
            else if (Cmb_Ant_Kayit_Tipi.Text=="Tümü")
            {
                Dt_Antrenor_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                antsorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(ub.UyeYetkili) as Yetkili,(ub.UyeHedef) as Hedef,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where ub.UyeYetkili='' or ub.UyeYetkili= '" + Lbl_Giris_Yapanin_Adi_Ant.Text + "'";
                DataGrid();
            }
        }
        public bool BtnAnaMenuClick = false;
        private void Btn_Ogt_AnaMenu_Click(object sender, EventArgs e) //ANA MENÜ ÇAĞIR
        {
            Form.ActiveForm.Close();
            BtnAnaMenuClick = true;
            AnaForm fr = new AnaForm();
            fr.Show();
            this.Close();
           
        }

       

        private void Btn_Ara_Click(object sender, EventArgs e) // ID İLE ARAMA
        {
            if (Cmb_Ant_Kayit_Tipi.Text=="")
            {
                MessageBox.Show("Lütfen önce kayıt tipini seçiniz !","Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            if ((Cmb_Ant_Kayit_Tipi.Text=="Üye" || Cmb_Ant_Kayit_Tipi.Text=="Özel Üye") && Txt_Ant_ID_Ara.Text=="")
            {  
                MessageBox.Show("Lütfen 10 haneli bir ID numarası giriniz !","Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else if (Cmb_Ant_Kayit_Tipi.Text == "Üye")
            {
               
                Dt_Antrenor_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                antsorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeKullaniciAdi) as Kullanıcı_Adı,(ub.UyeSifre) as Şifre,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(ub.UyeYetkili) as Yetkili,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where ub.UyeID='" + Txt_Ant_ID_Ara.Text + "'and UyeYetkili=''";
                DataGrid();
                
            }
            else if (Cmb_Ant_Kayit_Tipi.Text == "Özel Üye")
            {
              
                Dt_Antrenor_Kayitlar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
                antsorgu = "Select (ub.UyeID) as ID,(ub.UyeAdi) as Adı,(ub.UyeSoyadi) as Soyadı,(ub.UyeKullaniciAdi) as Kullanıcı_Adı,(ub.UyeSifre) as Şifre,(ub.UyeDogumTarihi) as Doğum_Tarihi,(ub.UyeMeslek) as Mesleği,(ub.UyeIletisim) as İletişim,(ub.UyeCinsiyet) as Cinsiyet,(ub.UyeYetkili) as Yetkili,uo.Omuz,(uo.Gogus) as Göğüs,uo.Bel,(uo.Kalca) as Kalça,(uo.Baldir) as Baldır,uo.Kol,uo.Boy,uo.Kilo,uo.Soru1,uo.Soru1Cevap,uo.Soru2,uo.Soru2Cevap from UyeBilgi ub full outer join UyeOlculer uo on ub.UyeID=uo.UyeID where ub.UyeID='" + Txt_Ant_ID_Ara.Text + "'and (UyeYetkili='"+Lbl_Giris_Yapanin_Adi_Ant.Text+"' or UyeYetkili='')";
                DataGrid();

            }
            
           
           
        }

        private void Cmb_Ant_Kayit_Tipi_SelectedIndexChanged(object sender, EventArgs e) // KAYİT TİPİNE GÖRE DEĞİŞKENLİK GÖSTEREN NESNELER
        {
            doubleclicksayi= 0;
            Dt_Pr.DataSource = "";
            Dt_Antrenor_Kayitlar.DataSource = "";
            if (Cmb_Ant_Kayit_Tipi.Text == "Tümü")
            {
                Txt_Ant_ID_Ara.Enabled = false;
                Btn_Ant_Ara.Enabled = false;
            }
            else if (Cmb_Ant_Kayit_Tipi.Text == "Üye" || Cmb_Ant_Kayit_Tipi.Text == "Özel Üye")
            {
                Txt_Ant_ID_Ara.Enabled = true;
                Btn_Ant_Ara.Enabled = true;
            }

            Btn_Ant_Listele.Enabled = true;
            
           

        }

        private void Txt_Ant_ID_Ara_TextChanged(object sender, EventArgs e) //ARAMA KISMI BOŞ VE DOLU DURUMUNA GÖRE DEĞİŞKENLİK GÖSTEREN NESNELER
        {
            if (Txt_Ant_ID_Ara.Text!="")
            {              
                Btn_Ant_Listele.Enabled = false;
               
            }
            else
            {
                Cmb_Ant_Kayit_Tipi.Enabled = true;
                Btn_Ant_Listele.Enabled = true;
              
            }
            
        }

        public string dghareketler;

        public void DataGrid2() //ANTRENÖR DATAGRIDVIEW METODU
        {
            SqlCommand hareketekle = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(dghareketler, baglan.baglanti());
            da.Fill(dt);
            Dt_Pr.DataSource = dt;
        }

        public string HareketSorgu;

        private void Btn_Pr_Ekle_Click(object sender, EventArgs e)
        {
            HareketSorgu = "Select Hareket_ID from " + Cmb_Pr_KasGrubu.Text + "Hareketler where HareketAd='" + Cmb_Pr_Hareket.Text + "'";
            SqlCommand hareket = new SqlCommand(HareketSorgu, baglan.baglanti());
            SqlDataReader dr;
            dr = hareket.ExecuteReader();
            while (dr.Read())
            {
                LbL_Pr_HareketID.Text = dr["Hareket_ID"].ToString();
            }
           
           
        }


        public string HareketCagir;
        private void Cmb_Pr_KasGrubu_SelectedIndexChanged(object sender, EventArgs e) //HAREKET ITEMLERİ EKLENİYOR
        {
            if (Cmb_Pr_KasGrubu.SelectedIndex>-1)
            {
                Cmb_Pr_Hareket.Items.Clear();
                HareketCagir = "Select HareketAd from " + Cmb_Pr_KasGrubu.Text + "Hareketler";
                SqlCommand cagir = new SqlCommand(HareketCagir, baglan.baglanti());
                SqlDataReader dr;
                dr = cagir.ExecuteReader();
                while (dr.Read())
                {
                    Cmb_Pr_Hareket.Items.Add(dr["HareketAd"]);
                }

                LbL_Pr_HareketID2.Text = LbL_Pr_HareketID.Text;
            }
            //SqlCommand boslarisil = new SqlCommand("Delete from TekrarSayisi where ((KasGrubu='' or HareketID='') or (KasGrubu IS NULL or HareketID IS NULL))", baglan.baglanti());
            //boslarisil.ExecuteNonQuery();


        }

        private void Cmb_Pr_Hareket_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            Btn_Pr_Yeni_Hareket.Enabled = true;
            LbL_Pr_HareketID2.Text = LbL_Pr_HareketID.Text;

            if (Cmb_Pr_Hareket.SelectedIndex > -1)
            {
                // LABEL'A HAREKET ID'SİNİ YAZAN YER ÇALIŞIYOR - LABELA ID 
                HareketSorgu = "Select Hareket_ID from " + Cmb_Pr_KasGrubu.Text + "Hareketler where HareketAd='" + Cmb_Pr_Hareket.Text + "'";
                SqlCommand hareket = new SqlCommand(HareketSorgu, baglan.baglanti());
                SqlDataReader dr;
                dr = hareket.ExecuteReader();
                while (dr.Read())
                {
                    LbL_Pr_HareketID.Text = dr["Hareket_ID"].ToString();
                }
            }

            SqlCommand hrkoku = new SqlCommand("Select HareketAd from TekrarSayisi where UyeID=@p1 and HareketAd=@p2", baglan.baglanti());
            hrkoku.Parameters.AddWithValue("@p1", Txt_Pr_ID.Text);
            hrkoku.Parameters.AddWithValue("@p2", Cmb_Pr_Hareket.Text);
            SqlDataReader dr2 = hrkoku.ExecuteReader();
            if (dr2.Read())
            {
                MessageBox.Show("Bu hareketi zaten daha önce eklediniz!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Btn_Pr_Yeni_Hareket.Enabled = true; 
                Cmb_Pr_KasGrubu.Enabled = false;
                Cmb_Pr_Hareket.Enabled = false;
                Cmb_Pr_Ay.Enabled = false;
                Cmb_Pr_Agirlik.Enabled = false;
                Cmb_Pr_SetSayisi.Enabled = false;
                Cmb_Pr_TekrarSayisi.Enabled = false;
                LbL_Pr_HareketID.Text = "";

                Cmb_Pr_KasGrubu.SelectedIndex = -1;
                Cmb_Pr_Hareket.SelectedIndex = -1;
                Cmb_Pr_Ay.SelectedIndex = -1;
                Cmb_Pr_Agirlik.SelectedIndex = -1;
                Cmb_Pr_SetSayisi.SelectedIndex = -1;
                Cmb_Pr_TekrarSayisi.SelectedIndex = -1;
                Dt_Pr.DataSource = "";
            }
            else
            {

                //HAREKET ID'Yİ UYEID'YE GÖRE YAZAN YER - UYEYE HAREKET ID
                SqlCommand komut = new SqlCommand("Update TekrarSayisi set HareketID=@p1 where UyeID=@p2 and HareketID IS NULL", baglan.baglanti());
                komut.Parameters.AddWithValue("@p1", LbL_Pr_HareketID.Text);
                komut.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                komut.ExecuteNonQuery();
                dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
                DataGrid2();

                if (LbL_Pr_HareketID.Text != "HRK")
                {
                    //HAREKETİN ADINI SQL'E COMBOBOXTAN ÇEKİP YAZAN YER - UYEYE HAREKET ADI
                    SqlCommand hareketad = new SqlCommand("Update TekrarSayisi set HareketAd=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                    hareketad.Parameters.AddWithValue("@p1", Cmb_Pr_Hareket.Text);
                    hareketad.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    hareketad.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                    hareketad.ExecuteNonQuery();
                    dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
                    DataGrid2();


                }


                if (LbL_Pr_HareketID2.Text != "HRK")
                {

                    //HAREKETİN ADINI SQL'E COMBOBOXTAN ÇEKİP YAZAN YER - UYEYE HAREKET ADI
                    SqlCommand hareketad = new SqlCommand("Update TekrarSayisi set HareketAd=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                    hareketad.Parameters.AddWithValue("@p1", Cmb_Pr_Hareket.Text);
                    hareketad.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    hareketad.Parameters.AddWithValue("@p3", LbL_Pr_HareketID2.Text);
                    hareketad.ExecuteNonQuery();
                    dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
                    DataGrid2();
                }

                //HAREKET KODUNU SQL'E LABEL'DAN ÇEKİP GÜNCELLETEN YER 
                SqlCommand hareketid = new SqlCommand("Update TekrarSayisi set HareketID=@p1 where UyeID=@p2 and HareketAd=@p3", baglan.baglanti());
                hareketid.Parameters.AddWithValue("@p1", LbL_Pr_HareketID.Text);
                hareketid.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                hareketid.Parameters.AddWithValue("@p3", Cmb_Pr_Hareket.Text);
                hareketid.ExecuteNonQuery();
                dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
                DataGrid2();

                //KAS GRUBU ADINI SQL'E COMBOBOXTAN ÇEKİP YAZAN YER - UYEYE KAS GRUBU
                SqlCommand kasgrubu = new SqlCommand("Update TekrarSayisi set KasGrubu=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                kasgrubu.Parameters.AddWithValue("@p1", Cmb_Pr_KasGrubu.Text);
                kasgrubu.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                kasgrubu.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                kasgrubu.ExecuteNonQuery();
                dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
                DataGrid2();
                
            }
            SqlCommand boslarisil = new SqlCommand("Delete from TekrarSayisi where ((KasGrubu='' or HareketAd='') or (KasGrubu IS NULL or HareketAd IS NULL))", baglan.baglanti());
            boslarisil.ExecuteNonQuery();
            baglan.baglanti().Close();

        }

      

        private void Cmb_Pr_Agirlik_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Cmb_Pr_Ay.Text)
            {
                case "1":
                    SqlCommand komut2 = new SqlCommand("Update TekrarSayisi set BirinciAyAgirlik=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                    komut2.Parameters.AddWithValue("@p1", Cmb_Pr_Agirlik.Text);
                    komut2.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    komut2.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                    komut2.ExecuteNonQuery();
                    break;

                case "2":
                    SqlCommand komut3 = new SqlCommand("Update TekrarSayisi set IkinciAyAgirlik=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                    komut3.Parameters.AddWithValue("@p1", Cmb_Pr_Agirlik.Text);
                    komut3.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    komut3.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                    komut3.ExecuteNonQuery();
                    break;

                case "3":
                    SqlCommand komut4 = new SqlCommand("Update TekrarSayisi set UcuncuAyAgirlik=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                    komut4.Parameters.AddWithValue("@p1", Cmb_Pr_Agirlik.Text);
                    komut4.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    komut4.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                    komut4.ExecuteNonQuery();
                    break;
            }
            dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
            DataGrid2();
            baglan.baglanti().Close();
        }

        private void Cmb_Pr_TekrarSayisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Cmb_Pr_Ay.Text)
            {
                case "1":
                    SqlCommand komut2 = new SqlCommand("Update TekrarSayisi set BirinciAy=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());

                    komut2.Parameters.AddWithValue("@p1", Cmb_Pr_SetSayisi.Text + " x " + Cmb_Pr_TekrarSayisi.Text);
                    komut2.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    komut2.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                    komut2.ExecuteNonQuery();
                    break;

                case "2":
                    SqlCommand komut3 = new SqlCommand("Update TekrarSayisi set IkinciAy=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                    komut3.Parameters.AddWithValue("@p1", Cmb_Pr_SetSayisi.Text + " x " + Cmb_Pr_TekrarSayisi.Text);
                    komut3.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    komut3.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                    komut3.ExecuteNonQuery();
                    break;

                case "3":
                    SqlCommand komut4 = new SqlCommand("Update TekrarSayisi set UcuncuAy=@p1 where UyeID=@p2 and HareketID=@p3", baglan.baglanti());
                    komut4.Parameters.AddWithValue("@p1", Cmb_Pr_SetSayisi.Text + " x " + Cmb_Pr_TekrarSayisi.Text);
                    komut4.Parameters.AddWithValue("@p2", Txt_Pr_ID.Text);
                    komut4.Parameters.AddWithValue("@p3", LbL_Pr_HareketID.Text);
                    komut4.ExecuteNonQuery();
                    break;
            }
            dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
            DataGrid2();
           
            baglan.baglanti().Close();
        }

       

        private void Btn_Pr_Yeni_Click(object sender, EventArgs e) //YENİ HAREKET
        {
            Dt_Pr.Enabled = false;
            Btn_Pr_Yeni_Hareket.Enabled = false;
            Cmb_Pr_KasGrubu.Enabled = true;
            Cmb_Pr_Hareket.Enabled = true;
            Cmb_Pr_Ay.Enabled = true;
            Cmb_Pr_Agirlik.Enabled = true;
            Cmb_Pr_SetSayisi.Enabled = true;
            Cmb_Pr_TekrarSayisi.Enabled = true;

            LbL_Pr_HareketID.Text = "HRK";
            LbL_Pr_HareketID2.Text = "HRK";
            SqlCommand komut8 = new SqlCommand("Insert into TekrarSayisi (UyeID) values (@p1)", baglan.baglanti());
            komut8.Parameters.AddWithValue("@p1", Txt_Pr_ID.Text);
            komut8.ExecuteNonQuery();
            Cmb_Pr_KasGrubu.Text = "";
            Cmb_Pr_Hareket.Text = "";
            Cmb_Pr_Ay.SelectedIndex = -1;
            Cmb_Pr_Agirlik.SelectedIndex = -1;
            Cmb_Pr_SetSayisi.SelectedIndex = -1;
            Cmb_Pr_TekrarSayisi.SelectedIndex = -1;
            LbL_Pr_HareketID.Text = "HRK";
            dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "'";
            DataGrid2();
        }
        public int secilen;
        
        private void Dt_Antrenor_Kayitlar_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Dt_Pr.Enabled = true;
            SqlCommand boslarisil = new SqlCommand("Delete from TekrarSayisi where KasGrubu IS NULL or HareketID IS NULL",baglan.baglanti());
            boslarisil.ExecuteNonQuery();
            Btn_Pr_Yeni_Hareket.Enabled = false;
            doubleclicksayi = 0;
            LbL_Pr_HareketID.Text = "HRK";
            Cmb_Pr_KasGrubu.SelectedIndex = -1;
            Cmb_Pr_Hareket.SelectedIndex = -1;
            Cmb_Pr_Ay.SelectedIndex = -1;
            Cmb_Pr_Agirlik.SelectedIndex = -1;
            Cmb_Pr_SetSayisi.SelectedIndex = -1;
            Cmb_Pr_TekrarSayisi.SelectedIndex= -1;

            Cmb_Pr_KasGrubu.Enabled = false;
            Cmb_Pr_Hareket.Enabled = false;
            Cmb_Pr_Ay.Enabled = false;
            Cmb_Pr_Agirlik.Enabled = false;
            Cmb_Pr_SetSayisi.Enabled = false;
            Cmb_Pr_TekrarSayisi.Enabled = false;

            Btn_Pr_Yeni_Hareket.Enabled = true;
            Btn_Pr_Butun_Hareketleri_Sil.Enabled = true;
            secilen = Dt_Antrenor_Kayitlar.SelectedCells[0].RowIndex;
            Txt_Pr_ID.Text = Dt_Antrenor_Kayitlar.Rows[secilen].Cells[0].Value.ToString();
            Txt_Pr_Ad.Text = Dt_Antrenor_Kayitlar.Rows[secilen].Cells[1].Value.ToString();
           
            SqlCommand komut8 = new SqlCommand("Select * from TekrarSayisi where UyeID=@p1", baglan.baglanti());
            komut8.Parameters.AddWithValue("@p1", Txt_Pr_ID.Text);
            komut8.ExecuteNonQuery();
            dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "'";
            DataGrid2();

            SqlCommand hrkkontrol = new SqlCommand("Select UyeID from TekrarSayisi where UyeID=@p1", baglan.baglanti());
            hrkkontrol.Parameters.AddWithValue("@p1", Txt_Pr_ID.Text);
            SqlDataReader dr2 = hrkkontrol.ExecuteReader();
            if (dr2.Read())
            {
                Btn_Pr_Butun_Hareketleri_Sil.Enabled = true;
            }
            else
            {
                Btn_Pr_Butun_Hareketleri_Sil.Enabled=false;
            }

            SqlCommand antkontrol = new SqlCommand("Select * from UyeBilgi where UyeID=@p1", baglan.baglanti());
            antkontrol.Parameters.AddWithValue("@p1", Txt_Pr_ID.Text);
            SqlDataReader dr = antkontrol.ExecuteReader();
            while (dr.Read())
            {
                Lbl_Pr_Antrenor_Kontrol.Text = dr[10].ToString();
            }
            if (Lbl_Pr_Antrenor_Kontrol.Text=="")
            {
                Btn_Pr_Beslenme.Enabled = false;
            }
            if (Lbl_Pr_Antrenor_Kontrol.Text!="")
            {
                Btn_Pr_Beslenme.Enabled = true;
            }
        }

       

        private void Btn_Pr_Sil_Click(object sender, EventArgs e)
        {
            Dt_Pr.Enabled = true; 
            SqlCommand silpr = new SqlCommand("Delete from TekrarSayisi where UyeID=@p1 and HareketID=@p2", baglan.baglanti());
            silpr.Parameters.AddWithValue("@p1", Txt_Pr_ID.Text);
            silpr.Parameters.AddWithValue("@p2", LbL_Pr_HareketID.Text);
            silpr.ExecuteNonQuery();
            MessageBox.Show("Hareket Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "'";
            DataGrid2();           
            Cmb_Pr_KasGrubu.SelectedIndex = -1;
            Cmb_Pr_Hareket.SelectedIndex = -1;
            Cmb_Pr_Ay.SelectedIndex = -1;
            Cmb_Pr_Agirlik.SelectedIndex = -1;
            Cmb_Pr_SetSayisi.SelectedIndex = -1;
            Cmb_Pr_TekrarSayisi.SelectedIndex = -1;
            Cmb_Pr_KasGrubu.Enabled = false;
            Cmb_Pr_Hareket.Enabled = false;
            Cmb_Pr_Ay.Enabled = false;
            Cmb_Pr_Agirlik.Enabled = false;
            Cmb_Pr_SetSayisi.Enabled = false;
            Cmb_Pr_TekrarSayisi.Enabled = false;
            doubleclicksayi = 0;
            baglan.baglanti().Close();
        }

        

        private void AntrenorPaneli_FormClosing(object sender, FormClosingEventArgs e)
        {
            BW_Antrenor_Frm_Closing.RunWorkerAsync();
            //AdminPaneli ad = new AdminPaneli();
            //ad.Name = "AdminPaneli";
            //HareketEkrani hrk = new HareketEkrani();
            //hrk.Name = "HareketEkrani";
            //if (Application.OpenForms[ad.Name] == null && BtnAnaMenuClick == true)
            //{
               
            //}
            //else if (Application.OpenForms[ad.Name] == null && Application.OpenForms[hrk.Name] == null)
            //{
            //    Environment.Exit(0);
                
            //}
        }

        private void Btn_El_Ile_Ekle_Click(object sender, EventArgs e)
        {
            ManuelHareketEkrani mhe = new ManuelHareketEkrani();
            mhe.ShowDialog();          
        }


        public int doubleclicksayi = 0;  
        private void Dt_Pr_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Dt_Pr.Enabled = false;
            Cmb_Pr_KasGrubu.Enabled = false;
            Cmb_Pr_Hareket.Enabled = false;
            doubleclicksayi++;
            if (doubleclicksayi<2)
            {
                LbL_Pr_HareketID.Text = "HRK";
                Cmb_Pr_Ay.SelectedIndex = -1;
                Cmb_Pr_Agirlik.SelectedIndex = -1;
                Cmb_Pr_SetSayisi.SelectedIndex = -1;
                Cmb_Pr_TekrarSayisi.SelectedIndex = -1;              
                Cmb_Pr_Ay.Enabled = true;
                Cmb_Pr_Agirlik.Enabled = true;
                Cmb_Pr_SetSayisi.Enabled = true;
                Cmb_Pr_TekrarSayisi.Enabled = true;
                Btn_Pr_Sil.Enabled = true;
                int secilenpr = Dt_Pr.SelectedCells[0].RowIndex;              
                //Cmb_Pr_KasGrubu.Text = Dt_Pr.Rows[secilenpr].Cells[1].Value.ToString();
                //Cmb_Pr_Hareket.Text = Dt_Pr.Rows[secilenpr].Cells[3].Value.ToString();
                LbL_Pr_HareketID.Text = Dt_Pr.Rows[secilenpr].Cells[2].Value.ToString();
                dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "' and HareketID='" + LbL_Pr_HareketID.Text + "'";
                DataGrid2();
                Btn_Pr_Yeni_Hareket.Enabled = false;
            }
            else
            {

            }
            doubleclicksayi++;
        }

        private void Cmb_Pr_Ay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmb_Pr_Agirlik.SelectedIndex = -1;
            Cmb_Pr_SetSayisi.SelectedIndex = -1;
            Cmb_Pr_TekrarSayisi.SelectedIndex = -1;
        }

        private void Btn_Pr_Beslenme_Click(object sender, EventArgs e)
        {
            BeslenmeProgrami bsl = new BeslenmeProgrami();
            bsl.beslenmeid = Txt_Pr_ID.Text;
            bsl.ShowDialog();
        }

        private void LbL_Pr_HareketID_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        private void Btn_Pr_Butun_Hareketleri_Sil_Click(object sender, EventArgs e)
        {
            DialogResult drs = new DialogResult();
            drs = MessageBox.Show("Kullanıcıya ait kayıtlı bütün hareketler silinecektir. Emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (drs==DialogResult.Yes)
            {
                SqlCommand prsil = new SqlCommand("Delete from TekrarSayisi where UyeID=@p1", baglan.baglanti());
                prsil.Parameters.AddWithValue("@p1", Txt_Pr_ID.Text);
                prsil.ExecuteNonQuery();
                MessageBox.Show("Kullanıcıya ait kayıtlı bütün hareketler silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dghareketler = "Select * from TekrarSayisi where UyeID='" + Txt_Pr_ID.Text + "'";
                DataGrid2();
                Btn_Pr_Butun_Hareketleri_Sil.Enabled = false;
            }
            else
            {

            }
            baglan.baglanti().Close();
        }

        private void BW_Antrenor_Frm_Closing_DoWork(object sender, DoWorkEventArgs e)
        {
            AdminPaneli ad = new AdminPaneli();
            ad.Name = "AdminPaneli";
            HareketEkrani hrk = new HareketEkrani();
            hrk.Name = "HareketEkrani";
            OzelUyePaneli oup = new OzelUyePaneli();
            oup.Name = "OzelUyePaneli";
            UyePaneli up = new UyePaneli();
            up.Name = "UyePaneli";
            if (Application.OpenForms[ad.Name] == null && BtnAnaMenuClick == true)
            {

            }
            else if (Application.OpenForms[ad.Name] == null && Application.OpenForms[hrk.Name] == null && Application.OpenForms[oup.Name] == null && Application.OpenForms[up.Name] == null)
            {
                
               Environment.Exit(0);

            }
        }
    }
}
