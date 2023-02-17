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
    public partial class AnaForm : KryptonForm 
    {
        public AnaForm()
        {
            InitializeComponent();
        }
        SQLBaglanti baglan = new SQLBaglanti();
        public string dosyayolu;


      

        private void Form1_Load(object sender, EventArgs e)
        {
            
            CheckForIllegalCrossThreadCalls = false;
            SqlCommand duyuruoku = new SqlCommand("Select Duyuru1 from IsletmeBilgi",baglan.baglanti());
            SqlDataReader dr2 = duyuruoku.ExecuteReader();
            while (dr2.Read())
            {
                Lbl_Duyuru.Text=dr2[0].ToString();
            }
        
            SqlCommand tarihgöster = new SqlCommand("Select Tarih from IsletmeBilgi", baglan.baglanti());
            SqlDataReader dr1 = tarihgöster.ExecuteReader();
            while (dr1.Read())
            {
                switch (dr1[0].ToString())
                {
                    case "1":
                        Lbl_Saat.Visible = true;
                        Lbl_Tarih.Visible = true;
                        break;
                    case "0":
                        Lbl_Saat.Visible = false;
                        Lbl_Tarih.Visible = false;
                        break;
                }
            }
           

            

            Tmr_Saat.Start();
            Btn_AnaMenu_AnaMenu.Enabled = false;
            FormShow(new GirisEkrani());
            try
            {
                SqlCommand resimgetir = new SqlCommand("Select DosyaYolu from IsletmeBilgi", baglan.baglanti());
                SqlDataReader dr = resimgetir.ExecuteReader();
                while (dr.Read())
                {
                    Btn_Resim.StateCommon.Back.Image = Image.FromFile(dr[0].ToString());
                }
            }
            catch 
            {
                //MessageBox.Show("Logonuz silinmiş veya yer değiştirmiş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            

           
        }

     

        private void Btn_AnaMenu_Click(object sender, EventArgs e)
        {          
            FormShow(new GirisEkrani());

        }

        private KryptonForm frmaktif;

        public void FormShow(KryptonForm frm)
        {
            ActiveFormClose();
            frmaktif = frm;
            frm.TopLevel = false;
            Grp_AnaMenu.Panel.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }
        public void ActiveFormClose()
        {
            if (frmaktif!=null)
            {
                frmaktif.Close();
                

            }
        }
        

        private void Tmr_Saat_Tick(object sender, EventArgs e)
        {
            Lbl_Saat.Text= DateTime.Now.ToLongTimeString();
            Lbl_Tarih.Text= DateTime.Now.ToShortDateString();
        }
        
      

        private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BW_AnaMenu_Frm_Closing.RunWorkerAsync();
            //AdminPaneli ad = new AdminPaneli();
            //AntrenorPaneli ant = new AntrenorPaneli();        
            //ad.Name = "AdminPaneli";
            //ant.Name = "AntrenorPaneli";
            //if (Application.OpenForms[ad.Name] == null | Application.OpenForms[ad.Name] == null )
            //{
            //    Environment.Exit(0);
            //}
            //else
            //{

            //}





        }

        private void Btn_Hareket_Ogren_Click(object sender, EventArgs e)
        {
            HareketEkrani hrk = new HareketEkrani();
         
            hrk.Name = "HareketEkrani";
            if (Application.OpenForms[hrk.Name] == null)
            {
                Form.ActiveForm.Hide();
                hrk.Show();
                //ad.TopMost = true;

            }
            else
            {
                DialogResult drs = new DialogResult();
                drs = MessageBox.Show("Hareket Ekranı Zaten Kullanılıyor", "Açık Pencere!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (drs == DialogResult.OK)
                {
                    
                }

            }
            
          
        }

        private void Btn_AnaMenuCikis_Click(object sender, EventArgs e)
        {
            DialogResult cikis = MessageBox.Show("Uygulamadan çıkış yapılacak! Emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (cikis==DialogResult.Yes)
            {
                this.Close();
                BW_CikisYap.RunWorkerAsync();
            }
            else if (cikis==DialogResult.No)
            {
                
            }
        }

        private void BW_AnaMenu_Frm_Closing_DoWork(object sender, DoWorkEventArgs e)
        {
            AdminPaneli ad = new AdminPaneli();
            ad.Name = "AdminPaneli";
            AntrenorPaneli ant = new AntrenorPaneli();
            ant.Name = "AntrenorPaneli";
            OzelUyePaneli oup = new OzelUyePaneli();
            oup.Name = "OzelUyePaneli";
            UyePaneli up = new UyePaneli();
            up.Name = "UyePaneli";
            HareketEkrani hr = new HareketEkrani();
            hr.Name = "HareketEkrani";
            if (Application.OpenForms[ad.Name] == null && Application.OpenForms[ant.Name] == null && Application.OpenForms[oup.Name] == null && Application.OpenForms[up.Name] == null && Application.OpenForms[hr.Name] == null)
            {
                Environment.Exit(0);
            }
            else
            {
               
            }
        }

        private void BW_CikisYap_DoWork(object sender, DoWorkEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
