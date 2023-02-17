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
    public partial class BeslenmeProgramim : KryptonForm
    {
        public BeslenmeProgramim()
        {
            InitializeComponent();
        }
        public string ozeluyeid2;
        SQLBaglanti baglan = new SQLBaglanti();
        private void BeslenmeProgramim_Load(object sender, EventArgs e)
        {
           
            SqlCommand bslbilgi = new SqlCommand ("Select * from UyeBilgi where UyeID=@p1", baglan.baglanti());
            bslbilgi.Parameters.AddWithValue("@p1", ozeluyeid2);
            SqlDataReader dr = bslbilgi.ExecuteReader();
            while (dr.Read())
            {
                Lbl_Beslenme_Ad.Text = dr[1].ToString();
                Lbl_Beslenme_Soyad.Text = dr[2].ToString();
                Lbl_Beslenme_Cinsiyet.Text = dr[8].ToString();
                Lbl_Beslenme_Dt.Text = dr[5].ToString();
                Lbl_Beslenme_Hedef.Text = dr[9].ToString();
                Lbl_Beslenme_PrYazan.Text = dr[10].ToString();
                Lbl_Beslenme_UyelikTarihi.Text = dr[11].ToString();
                Rch_Program.Text = dr[12].ToString();
            }
        }

        private void Btn_Program_Yazdir_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Rectangle rect1 = new Rectangle(0, 0, 827, 40); //Başlık için dikdörtgen
            Rectangle rect2 = new Rectangle(25, 55, 300, 130);//Kişisel bilgiler dikdörtgen
            Rectangle rect3 = new Rectangle(25, 200, 777, 944); //Program yazılma yeri için dikdörtgen
            Rectangle rect4 = new Rectangle(614, 10, 200, 25); //Programı yazan kişi için dikdörtgen
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font font1 = new Font("Montserrat", 16, FontStyle.Bold);//Kalın Başlık İçin
            Font font2 = new Font("Montserrat", 12, FontStyle.Bold);//Öğeler için
            Font font3 = new Font("Montserrrat", 12, FontStyle.Regular);//Metin için
            Font font4 = new Font("Montserrrat", 8, FontStyle.Regular);//Tarih için
            SolidBrush firca = new SolidBrush(Color.Black);
            e.Graphics.DrawString($"BESLENME PROGRAMI", font1, firca, rect1, sf);
            e.Graphics.DrawRectangle(Pens.Transparent, rect1); //Dikdörtgen çizim yeri
            e.Graphics.DrawRectangle(Pens.Transparent, rect2); //Kişisel bilgiler çizim yeri
            e.Graphics.DrawRectangle(Pens.Transparent, rect3); //Program yazılma çizim yeri
            e.Graphics.DrawRectangle(Pens.Transparent, rect4); //Programı yazan kişi için
            e.Graphics.DrawString(Rch_Program.Text, font3, firca, rect3); //x sağda y aşağı

            e.Graphics.DrawString($"Tarih: {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}", font4, firca, 30, 10);
            e.Graphics.DrawString($"Adı: {Lbl_Beslenme_Ad.Text}", font2, firca, 30, 60);
            e.Graphics.DrawString($"Soyadı: {Lbl_Beslenme_Soyad.Text}", font2, firca, 30, 80);
            e.Graphics.DrawString($"Cinsiyet: {Lbl_Beslenme_Cinsiyet.Text}", font2, firca, 30, 100);
            e.Graphics.DrawString($"Doğum Tarihi: {Lbl_Beslenme_Dt.Text}", font2, firca, 30, 120);
            e.Graphics.DrawString($"Hedefi: {Lbl_Beslenme_Hedef.Text}", font2, firca, 30, 140);
            e.Graphics.DrawString($"Üyelik Tarihi: {Lbl_Beslenme_UyelikTarihi.Text}", font2, firca, 30, 160);
            e.Graphics.DrawString($"Programı Yazan: {Lbl_Beslenme_PrYazan.Text}", font4, firca, rect4);
        }
    }
}
