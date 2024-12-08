using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon
{
    public partial class veliler : Form
    {
        public veliler()
        {
            InitializeComponent();
        }

        void listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from TBL_VELILER", Db.con());
            adapter.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtanne.Text=="" || txtbaba.Text=="" || txttelefon.Text=="")
            {
                MessageBox.Show("Lütfen alanları doldurunuz");
                return;
            }


            SqlCommand cmd = new SqlCommand("insert into TBL_VELILER(VELIANNE,VELIBABA,VELITEL,VELIEMAIL) VALUES(@VELIANNE,@VELIBABA,@VELITEL,@VELIEMAIL)", Db.con());
            cmd.Parameters.AddWithValue("VELIANNE", txtanne.Text);
            cmd.Parameters.AddWithValue("VELIBABA", txtbaba.Text);
            cmd.Parameters.AddWithValue("VELITEL", txttelefon.Text);
            cmd.Parameters.AddWithValue("VELIEMAIL", txtmail.Text);
            cmd.ExecuteNonQuery();

            listele();
        }

        private void veliler_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'okulOtomasyonDataSet1.TBL_VELILER' table. You can move, or remove it, as needed.
         
            listele();
            SatirBilgileriniAktar();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili satır silinsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("delete from TBL_VELILER where VELIID=@p1", Db.con());
                var id = gridView1.GetFocusedRowCellValue("VELIID");
                cmd.Parameters.AddWithValue("@p1", id);
                cmd.ExecuteNonQuery();
                listele();
                MessageBox.Show("Veli bilgileri başarıyla kaydeildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtanne.Text) ||
     string.IsNullOrWhiteSpace(txtbaba.Text) ||
     string.IsNullOrWhiteSpace(txttelefon.Text))
            {
                MessageBox.Show("Lütfen alanları doldurunuz");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtid.Text))
            {
                MessageBox.Show("Lütfen geçerli bir Veli ID'si giriniz");
                return;
            }

            SqlCommand cmd = new SqlCommand(
                "UPDATE TBL_VELILER SET " +
                "VELIANNE = @VELIANNE, VELIBABA = @VELIBABA, " +
                "VELITEL = @VELITEL, VELIEMAIL = @VELIEMAIL " +
                "WHERE VELIID = @VELIID",
                Db.con()
            );

            cmd.Parameters.AddWithValue("@VELIANNE", txtanne.Text.Trim());
            cmd.Parameters.AddWithValue("@VELIBABA", txtbaba.Text.Trim());
            cmd.Parameters.AddWithValue("@VELITEL", txttelefon.Text.Trim());
            cmd.Parameters.AddWithValue("@VELIEMAIL", txtmail.Text.Trim());
            cmd.Parameters.AddWithValue("@VELIID", int.Parse(txtid.Text.Trim()));

            try
            {
                cmd.ExecuteNonQuery();
                listele();
                MessageBox.Show("Veli bilgileri başarıyla güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SatirBilgileriniAktar()
        {
            if (gridView1.GetFocusedRow() == null) return;

            txtanne.Text = gridView1.GetFocusedRowCellValue("VELIANNE")?.ToString() ?? "";
            txtbaba.Text = gridView1.GetFocusedRowCellValue("VELIBABA")?.ToString() ?? "";
            txttelefon.Text = gridView1.GetFocusedRowCellValue("VELITEL")?.ToString() ?? "";
            txtmail.Text = gridView1.GetFocusedRowCellValue("VELIEMAIL")?.ToString() ?? "";
            txtid.Text = gridView1.GetFocusedRowCellValue("VELIID")?.ToString() ?? "";
        }

     

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            SatirBilgileriniAktar();
        }

 

        void temizle()
        {
            txtanne.Text = "";
            txtbaba.Text = "";
            txttelefon.Text = "";
            txtmail.Text = "";
            txtid.Text = "";
        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            SatirBilgileriniAktar();
        }
    }
}
