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
    public partial class Ayarlar : KryptonForm
    {
        public Ayarlar()
        {
            InitializeComponent();
        }
       
      

        SQLBaglanti baglan = new SQLBaglanti();

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            SqlCommand duyuruoku = new SqlCommand("Select Duyuru1 from IsletmeBilgi", baglan.baglanti());
            SqlDataReader dr2 = duyuruoku.ExecuteReader();
            while (dr2.Read())
            {
                Txt_Duyuru.Text = dr2[0].ToString();
            }


            SqlCommand tarihgetir = new SqlCommand("Select Tarih from IsletmeBilgi",baglan.baglanti());
            SqlDataReader dr = tarihgetir.ExecuteReader();
            while (dr.Read())
            {
                switch (dr[0].ToString())
                {
                    case "1":
                        kryptonCheckBox1.Checked = true;
                        break;
                    case "0":
                        kryptonCheckBox1.Checked = false;
                        break;
                }
                
            }
        }



       
        public int tarihgöster;

        public void Tarih()
        {
            SqlCommand tarih = new SqlCommand("Update IsletmeBilgi set Tarih=@p1", baglan.baglanti());
            tarih.Parameters.AddWithValue("@p1", tarihgöster);
            tarih.ExecuteNonQuery();
            
        }

        private void kryptonCheckBox1_Click(object sender, EventArgs e)
        {
            if (kryptonCheckBox1.Checked)
            {
                tarihgöster = 1;
                Tarih();
            }
            else
            {
                tarihgöster = 0;
                Tarih();
            }
            baglan.baglanti().Close();
        }
        
        private void Btn_AyarlarıKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand duyuru = new SqlCommand("Update IsletmeBilgi set Duyuru1=@p1", baglan.baglanti());
            duyuru.Parameters.AddWithValue("@p1", Txt_Duyuru.Text);
            duyuru.ExecuteNonQuery();

          
            MessageBox.Show("Ayarlarınız kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            baglan.baglanti().Close();
        }
        string karakter;
        private void Txt_Duyuru_TextChanged(object sender, EventArgs e)
        {
            karakter = Txt_Duyuru.Text;
            var karaktersayisi = karakter.Length;
            Lbl_Girilen_Karakter.Text = karaktersayisi.ToString();
            if (Lbl_Girilen_Karakter.Text=="55")
            {
                Txt_Duyuru.StateCommon.Border.Color1 = Color.Red;
            }
            else
            {
                Txt_Duyuru.StateCommon.Border.Color1 = Color.Green;
            }
        }
        public string dosyayolu;
        private void Btn_Logo_Ekle_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Filter = "Resim Dosyası |*.jpg;*.jpeg;*.png| Tüm Dosyalar |*.*";
                opf.ShowDialog();
                dosyayolu = opf.FileName;
                

                SqlCommand resim = new SqlCommand("Update IsletmeBilgi set DosyaYolu=@p1", baglan.baglanti());
                resim.Parameters.AddWithValue("@p1", dosyayolu);
                resim.ExecuteNonQuery();
                baglan.baglanti().Close();

            }
            catch
            {
                DialogResult drs = new DialogResult();
                drs = MessageBox.Show("Resim Eklenmedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Bilgi_Click(object sender, EventArgs e)
        {
            Bilgi blg = new Bilgi();
            blg.ShowDialog();
        }
    }
}
