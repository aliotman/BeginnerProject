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
    public partial class ManuelHareketEkrani : KryptonForm
    {
        public ManuelHareketEkrani()
        {
            InitializeComponent();
        }

       

        SQLBaglanti baglan = new SQLBaglanti();


        private void Txt_Pr2_Hareket_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Pr2_Hareket.Text=="")
            {
                Btn_Pr2_Kaydet.Enabled = false;
            }
            else
            {
                Btn_Pr2_Kaydet.Enabled = true;
            }
        }

        public string manuelhareketcagir;

        public void ManuelHareketCagir()
        {
            SqlCommand manuelistele = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(manuelhareketcagir,baglan.baglanti());
            da.Fill(dt);
            Dt_Pr2.DataSource = dt;          
        }

        private void Cmb_Pr2_KasGrubu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Txt_Pr2_Hareket.Enabled = true;
            Btn_Pr2_Sil.Enabled = false;
            Txt_Pr2_HareketID.Text = "";
            Txt_Pr2_Hareket.Text = "";
            switch (Cmb_Pr2_KasGrubu.Text)
            {
                case "Gogus":
                    manuelhareketcagir = "Select * from GogusHareketler where Hareket_ID LIKE '%E-%'";
                    break;
                case "Omuz":
                    manuelhareketcagir = "Select * from OmuzHareketler where Hareket_ID LIKE '%E-%'";
                    break;
                case "Sirt":
                    manuelhareketcagir = "Select * from SirtHareketler where Hareket_ID LIKE '%E-%'";
                    break;
                case "OnKol":
                    manuelhareketcagir = "Select * from OnKolHareketler where Hareket_ID LIKE '%E-%'";
                    break;
                case "ArkaKol":
                    manuelhareketcagir = "Select * from ArkaKolHareketler where Hareket_ID LIKE '%E-%'";
                    break;
                case "Bacak":
                    manuelhareketcagir = "Select * from BacakHareketler where Hareket_ID LIKE '%E-%'";
                    break;
                case "Bel":
                    manuelhareketcagir = "Select * from BelHareketler where Hareket_ID LIKE '%E-%'";
                    break;
                case "Karin":
                    manuelhareketcagir = "Select * from KarinHareketler where Hareket_ID LIKE '%E-%'";
                    break;
            }
            ManuelHareketCagir();


        }
        public string manuelhareket;
        public string manuelhareketadi;
   

        public string rndhareketid;
        static string RandomID(ref Random rnd)// ID ÜRETİLEN YER
        {              
           string ID = Convert.ToString(rnd.Next(100,1000));
           return ID;
        }

        public string hareketconcat;

        public void RandomHareketIDKontrol()
        {
            Random rnd = new Random();
            
            string idcheck2 = (RandomID(ref rnd));
            hareketconcat = "E-" + manuelhareketadi + idcheck2;
            tekrar:
            SqlCommand idkontrol2 = new SqlCommand("Select Hareket_ID from " + Cmb_Pr2_KasGrubu.Text + "Hareketler where Hareket_ID=@p1", baglan.baglanti());
            idkontrol2.Parameters.AddWithValue("@p1", hareketconcat);
            SqlDataReader dr = idkontrol2.ExecuteReader();
            if (dr.Read())
            {
                Random rnd2 = new Random();
                rndhareketid = (RandomID(ref rnd2));//ÜRETİLEN ID' AYNI İSE TEKRAR ÜRET
                hareketconcat= "E-" + manuelhareketadi + rndhareketid;
                goto tekrar;
            }
            

        }


        private void Btn_Pr2_Kaydet_Click(object sender, EventArgs e) //MANUEL PROGRAM EKLE
        {
           
            switch (Cmb_Pr2_KasGrubu.Text)
            {
                case "Gogus":
                    manuelhareket = "Insert into GogusHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "GGS";
                    break;
                case "Omuz":
                    manuelhareket = "Insert into OmuzHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "OMZ";
                    break;
                case "Sirt":
                    manuelhareket = "Insert into SirtHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "SRT";
                    break;
                case "OnKol":
                    manuelhareket = "Insert into OnKolHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "ONK";
                    break;
                case "ArkaKol":
                    manuelhareket = "Insert into ArkaKolHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "ARK";
                    break;
                case "Bacak":
                    manuelhareket = "Insert into BacakHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "BCK";
                    break;
                case "Bel":
                    manuelhareket = "Insert into BelHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "BEL";
                    break;
                case "Karin":
                    manuelhareket = "Insert into KarinHareketler (Hareket_ID,HareketAd) values (@p1,@p2)";
                    manuelhareketadi = "KRN";
                    break;
            }
           
            RandomHareketIDKontrol();
            SqlCommand manuelkayit = new SqlCommand(manuelhareket, baglan.baglanti());
            manuelkayit.Parameters.AddWithValue("@p1", hareketconcat);
            manuelkayit.Parameters.AddWithValue("@p2", Txt_Pr2_Hareket.Text);
            manuelkayit.ExecuteNonQuery();

            MessageBox.Show("Hareket kaydedildi!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cmb_Pr2_KasGrubu.SelectedIndex = -1;
            Txt_Pr2_Hareket.Text = "";
            Dt_Pr2.DataSource = "";
            baglan.baglanti().Close();
        }

        private void Dt_Pr2_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = Dt_Pr2.SelectedCells[0].RowIndex;
            Txt_Pr2_HareketID.Text=Dt_Pr2.Rows[secilen].Cells[0].Value.ToString();
            Txt_Pr2_Hareket.Text=Dt_Pr2.Rows[secilen].Cells[1].Value.ToString();
            Btn_Pr2_Kaydet.Enabled = false;
            Btn_Pr2_Sil.Enabled = true;
            Txt_Pr2_Hareket.Enabled = false;
        }

        public string manuelhareketsil;

        public void ManuelHareketSil()//MANUEL PROGRAM SİLME METODU 
        {
            SqlCommand manuelsil = new SqlCommand(manuelhareketsil, baglan.baglanti());
            manuelsil.ExecuteNonQuery();
            baglan.baglanti().Close();
        }

        private void Btn_Pr2_Sil_Click(object sender, EventArgs e) //MANUEL PROGRAM SİL
        {
            switch (Cmb_Pr2_KasGrubu.Text)
            {
                case "Gogus":
                    manuelhareketsil = "Delete from GogusHareketler where Hareket_ID='"+Txt_Pr2_HareketID.Text+"'";
                    break;
                case "Omuz":
                    manuelhareketsil = "Delete from OmuzHareketler where Hareket_ID='" + Txt_Pr2_HareketID.Text + "'";
                    break;
                case "Sirt":
                    manuelhareketsil = "Delete from SirtHareketler where Hareket_ID='" + Txt_Pr2_HareketID.Text + "'";
                    break;
                case "OnKol":
                    manuelhareketsil = "Delete from OnKolHareketler where Hareket_ID='" + Txt_Pr2_HareketID.Text + "'";
                    break;
                case "ArkaKol":
                    manuelhareketsil = "Delete from ArkaKolHareketler where Hareket_ID='" + Txt_Pr2_HareketID.Text + "'";
                    break;
                case "Bacak":
                    manuelhareketsil = "Delete from BacakHareketler where Hareket_ID='" + Txt_Pr2_HareketID.Text + "'";
                    break;
                case "Bel":
                    manuelhareketsil = "Delete from BelHareketler where Hareket_ID='" + Txt_Pr2_HareketID.Text + "'";
                    break;
                case "Karin":
                    manuelhareketsil = "Delete from KarinHareketler where Hareket_ID='" + Txt_Pr2_HareketID.Text + "'";
                    break;
            }
            ManuelHareketSil();
            MessageBox.Show("Hareket silindi!","", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Cmb_Pr2_KasGrubu.SelectedIndex = -1;
            Txt_Pr2_Hareket.Text = "";
            Dt_Pr2.DataSource = "";
        }
    }
}
