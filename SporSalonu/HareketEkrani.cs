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
    public partial class HareketEkrani : KryptonForm
    {
        public HareketEkrani()
        {
            InitializeComponent();
        }

        SQLBaglanti baglan = new SQLBaglanti();

      
        public bool BtnHrkAnaMenuClick = false;
        private void Btn_Adm_AnaMenu_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Hide();
            BtnHrkAnaMenuClick = true;
            AnaForm fr = new AnaForm();
            fr.Show();
            this.Close();
        }

        private void HareketEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            BW_HareketEkrani_Frm_Closing.RunWorkerAsync();
            //AdminPaneli ad = new AdminPaneli();
            //ad.Name = "AdminPaneli";
            //AntrenorPaneli ap = new AntrenorPaneli();
            //ap.Name = "AntrenorPaneli";
            //UyePaneli up = new UyePaneli();
            //up.Name = "UyePaneli";
            //AnaForm af = new AnaForm();
            //af.Name = "AnaMenuForm";           
            //OzelUyePaneli oup = new OzelUyePaneli();
            //oup.Name = "OzelUyePaneli";
            //if (Application.OpenForms[ad.Name] == null && Application.OpenForms[ap.Name] == null && Application.OpenForms[up.Name] == null && Application.OpenForms[af.Name] == null && Application.OpenForms[oup.Name] == null && BtnHrkAnaMenuClick == true)
            //{
                
            //}
            //else if (Application.OpenForms[ad.Name] == null && Application.OpenForms[ap.Name] == null && Application.OpenForms[up.Name] == null && Application.OpenForms[af.Name] == null && Application.OpenForms[oup.Name] == null)
            //{
            //    Environment.Exit(0);
            //}
           
         
        }
        public string HrkEkraniHrkCagir;
        private void Cmb_Hrk_KasGrubu_SelectedIndexChanged(object sender, EventArgs e)
        {
            Txt_Hareketin_Aciklamasi.Clear();
            if (Cmb_Hrk_KasGrubu.SelectedIndex > -1)
            {
                Cmb_Hrk_Hareket.Items.Clear();
                HrkEkraniHrkCagir = "Select HareketAd from " + Cmb_Hrk_KasGrubu.Text + "Hareketler where Hareket_ID NOT LIKE '%E-%'";
                SqlCommand cagir = new SqlCommand(HrkEkraniHrkCagir, baglan.baglanti());
                SqlDataReader dr;
                dr = cagir.ExecuteReader();
                while (dr.Read())
                {
                    Cmb_Hrk_Hareket.Items.Add(dr["HareketAd"]);
                }              
            }
            Cmb_Hrk_Hareket.Enabled = true;
            Txt_Secilen_Hareket.Clear();
        }

        private void Cmb_Hrk_Hareket_SelectedIndexChanged(object sender, EventArgs e)
        {
            Btn_Hrk_Goster.Enabled = true;
            Txt_Secilen_Hareket.Text = Cmb_Hrk_Hareket.Text;
            Pb_Hareket_Ekrani.ImageLocation = "";
            Txt_Hareketin_Aciklamasi.Clear();
        }

        
        private void Btn_Hrk_Goster_Click(object sender, EventArgs e)
        {
            //string gifyolu = 
            //this.Pb_Hareket_Ekrani.ImageLocation = gifyolu;
            SqlCommand gifcagir = new SqlCommand("Select Hareket_ID from GogusHareketler where HareketAd='"+Cmb_Hrk_Hareket.Text+"'", baglan.baglanti());
            SqlDataReader dr = gifcagir.ExecuteReader();
            while (dr.Read())
            {              
                Lbl_HareketKodu.Text= dr[0].ToString();
                string yol = AppDomain.CurrentDomain.BaseDirectory;
                this.Pb_Hareket_Ekrani.ImageLocation = yol+"\\Hareketler\\"+Lbl_HareketKodu.Text+".gif";
            }

            SqlCommand aciklama = new SqlCommand("Select HareketAciklamasi from HareketEkrani where Hareket_ID=@p1",baglan.baglanti());
            aciklama.Parameters.AddWithValue("@p1", Lbl_HareketKodu.Text);
            SqlDataReader dr2=aciklama.ExecuteReader();
            while (dr2.Read())
            {
                Txt_Hareketin_Aciklamasi.Text = dr2[0].ToString();
            }

        }

        private void BW_HareketEkrani_Frm_Closing_DoWork(object sender, DoWorkEventArgs e)
        {
            AdminPaneli ad = new AdminPaneli();
            ad.Name = "AdminPaneli";
            AntrenorPaneli ap = new AntrenorPaneli();
            ap.Name = "AntrenorPaneli";
            UyePaneli up = new UyePaneli();
            up.Name = "UyePaneli";
            AnaForm af = new AnaForm();
            af.Name = "AnaMenuForm";
            OzelUyePaneli oup = new OzelUyePaneli();
            oup.Name = "OzelUyePaneli";
            if (Application.OpenForms[ad.Name] == null && Application.OpenForms[ap.Name] == null && Application.OpenForms[up.Name] == null && Application.OpenForms[af.Name] == null && Application.OpenForms[oup.Name] == null && BtnHrkAnaMenuClick == true)
            {

            }
            else if (Application.OpenForms[ad.Name] == null && Application.OpenForms[ap.Name] == null && Application.OpenForms[up.Name] == null && Application.OpenForms[af.Name] == null && Application.OpenForms[oup.Name] == null)
            {
                Environment.Exit(0);
            }
        }

        private void HareketEkrani_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
    }
}
