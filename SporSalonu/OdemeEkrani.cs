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
    public partial class OdemeEkrani : KryptonForm
    {
        public OdemeEkrani()
        {
            InitializeComponent();
        }

        SQLBaglanti baglan = new SQLBaglanti();
        public string sorgu;
        public void OdemeEkraniCagir()
        {
            SqlCommand odemeplani = new SqlCommand();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sorgu, baglan.baglanti());
            da.Fill(dt);
            Dt_Odeme_Ekrani.DataSource = dt;
        }

        private void OdemeEkrani_Load(object sender, EventArgs e)
        {
            Cmb_Odeme_Uye_Tipi.SelectedIndex = 2;
        }

        private void Grp_OdemePlani_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cmb_Uye_Tipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Cmb_Odeme_Uye_Tipi.Text)
            {
                case "Üye":
                    sorgu = "Select UyeID as ID, (UyeAdi) as [Üye Adı],(UyeSoyadi) as [Üye Soyadı],  [1.Ay], [2.Ay], [3.Ay], [4.Ay], [5.Ay], [6.Ay], [7.Ay], [8.Ay], [9.Ay], [10.Ay], [11.Ay], [12.Ay] from OdemePlani where UyeYetkili=''";
                    OdemeEkraniCagir();
                    break;

                case "Özel Üye":
                    sorgu = "Select UyeID as ID, (UyeAdi) as [Üye Adı], (UyeSoyadi) as [Üye Soyadı], [1.Ay], [2.Ay], [3.Ay], [4.Ay], [5.Ay], [6.Ay], [7.Ay], [8.Ay], [9.Ay], [10.Ay], [11.Ay], [12.Ay] from OdemePlani where UyeYetkili NOT LIKE ''";
                    OdemeEkraniCagir();
                    break;

                case "Tümü":
                    sorgu = "Select UyeID as ID, (UyeAdi) as [Üye Adı],(UyeSoyadi) as [Üye Soyadı],  [1.Ay], [2.Ay], [3.Ay], [4.Ay], [5.Ay], [6.Ay], [7.Ay], [8.Ay], [9.Ay], [10.Ay], [11.Ay], [12.Ay] from OdemePlani";
                    OdemeEkraniCagir();
                    break;
            }
            Txt_Odeme_ID.Text = "";
            Txt_Odeme_Ad.Text = "";
            Txt_Odeme_Soyad.Text = "";
            Cmb_Odeme_Ay.SelectedIndex = -1;
            Cmb_Odeme_Durumu.SelectedIndex = -1;
            Cb_OzelUye.Checked = false;
            Cb_Uye.Checked = false;
        }

        private void Dt_Odeme_Ekrani_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Txt_Odeme_ID.Enabled = false;
            int secilen = Dt_Odeme_Ekrani.SelectedCells[0].RowIndex;

            Txt_Odeme_ID.Text=Dt_Odeme_Ekrani.Rows[secilen].Cells[0].Value.ToString();
            Txt_Odeme_Ad.Text=Dt_Odeme_Ekrani.Rows[secilen].Cells[1].Value.ToString();
            Txt_Odeme_Soyad.Text = Dt_Odeme_Ekrani.Rows[secilen].Cells[2].Value.ToString();


            SqlCommand odemeidara = new SqlCommand("Select UyeYetkili from OdemePlani where UyeID=@p1",baglan.baglanti());
            odemeidara.Parameters.AddWithValue("@p1", Txt_Odeme_ID.Text);
            SqlDataReader dr = odemeidara.ExecuteReader();
            while (dr.Read())
            {

                if (dr[0].ToString()=="")
                {
                    Cb_OzelUye.Checked = false;
                    Cb_Uye.Checked = true;
                }
                else
                {
                    Cb_OzelUye.Checked = true;
                    Cb_Uye.Checked = false;
                }


            }


        }

        private void Btn_Odeme_Kaydet_Click(object sender, EventArgs e)
        {
            SqlCommand odemeekle = new SqlCommand("Update OdemePlani  set "+"["+Cmb_Odeme_Ay.Text+"]=@p1 where UyeID=@p2",baglan.baglanti());
            odemeekle.Parameters.AddWithValue("@p1", Cmb_Odeme_Durumu.Text);        
            odemeekle.Parameters.AddWithValue("@p2", Txt_Odeme_ID.Text);
            odemeekle.ExecuteNonQuery();
            MessageBox.Show(Cmb_Odeme_Ay.Text + " Ödemesi Kaydedildi!", "Ödeme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            OdemeEkraniCagir();
            Cmb_Odeme_Ay.SelectedIndex = -1;
            Cmb_Odeme_Durumu.SelectedIndex = -1;
        }

        private void Btn_Odeme_Ara_Click(object sender, EventArgs e)
        {
           
            SqlCommand idara = new SqlCommand("Select UyeID from OdemePlani where UyeID=@p1",baglan.baglanti());
            idara.Parameters.AddWithValue("@p1", Txt_Odeme_ID.Text);
            SqlDataReader dr = idara.ExecuteReader();
            if (dr.Read())
            {
                sorgu = "Select UyeID as ID, (UyeAdi) as [Üye Adı], (UyeSoyadi) as [Üye Soyadı], [1.Ay], [2.Ay], [3.Ay], [4.Ay], [5.Ay], [6.Ay], [7.Ay], [8.Ay], [9.Ay], [10.Ay], [11.Ay], [12.Ay] from OdemePlani where UyeID='" + Txt_Odeme_ID.Text + "'";
                OdemeEkraniCagir();

            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir ID numarası giriniz", "ID Arama", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void Btn_Odeme_Temizle_Click(object sender, EventArgs e)
        {
            Txt_Odeme_ID.Text = "";
            Txt_Odeme_ID.Enabled = true;
            Txt_Odeme_Ad.Text = "";
            Txt_Odeme_Soyad.Text = "";
            Cmb_Odeme_Ay.SelectedIndex = -1;
            Cmb_Odeme_Durumu.SelectedIndex = -1;
            Cb_OzelUye.Checked = false;
            Cb_Uye.Checked = false;
            Cmb_Odeme_Uye_Tipi.SelectedIndex = 2;
        }
    }
}
