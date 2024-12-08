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
using System.IO;
using DevExpress.XtraBars;
namespace Otomasyon
{
    public partial class frmogretmenler : Form
    {
        public frmogretmenler()
        {
            InitializeComponent();
        }

        string yol = Application.StartupPath + "/Resimler/";
        private void frmogretmenler_Load(object sender, EventArgs e)
        {
     
            ilcek();
            bransgetir();
            listele();
        }
        public void listele()
        {
           DataTable dt=new DataTable();
            SqlDataAdapter adap=new SqlDataAdapter("select * from TBL_OGRETMENLER", Db.con());
            adap.Fill(dt);
            gridControl1.DataSource = dt;
            satiryakala();
        }

        public void ilcek()
        {
            DataTable dt=new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from ILLER",Db.con());
            adapter.Fill(dt);
           txtil.Properties.DataSource= dt;
            txtil.Properties.DisplayMember= "ILADI";
            txtil.Properties.ValueMember= "ID";

        }

        public void ilcecek()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from ILCELER WHERE ILID='"+txtil.EditValue+"'", Db.con());// iller tablosunu çek
            adapter.Fill(dt);
            txtilce.Properties.DataSource = dt;
            txtilce.Properties.DisplayMember = "ILCEADI";
            txtilce.Properties.ValueMember = "ILCEID";
        }


        public void bransgetir() 
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from TBL_BRANSLAR", Db.con());
            adapter.Fill(dt);
            txtbrans.Properties.DataSource = dt;
            txtbrans.Properties.DisplayMember = "BRANSADI";
            txtbrans.Properties.ValueMember = "ID";
        }

        private void txtil_EditValueChanged(object sender, EventArgs e)
        {
            ilcek();
            ilcecek();
        }


        void temizle() 
        {

            txtad.Text ="";
            txtsoyad.Text ="";
            txttcno.Text = "";
            txttelefon.Text = "";
            txtmail.Text = "";
            pictureBox1.ImageLocation = null;
            txtadres.Text ="" ;



        }



        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string resimkayityeri = Application.StartupPath + "/Resimler/";
            string resimokumayolu = pictureBox1.ImageLocation;

            if (!Directory.Exists(resimkayityeri))
            {
                Directory.CreateDirectory(resimkayityeri);
            }

            if (File.Exists(resimokumayolu))
            {
          
                string hedefDosyaYolu = Path.Combine(resimkayityeri, Path.GetFileName(resimokumayolu));

                SqlCommand cmd = new SqlCommand("insert into TBL_OGRETMENLER (OGRTAD,OGRTSOYAD,OGRTTC,OGRTTEL,OGRTMAIL,OGRTIL,OGRTILCE,OGRTADRES,OGRTBRANS,OGRTFOTO) values(@OGRTAD,@OGRTSOYAD,@OGRTTC,@OGRTTEL,@OGRTMAIL,@OGRTIL,@OGRTILCE,@OGRTADRES,@OGRTBRANS,@OGRTFOTO)", Db.con());
                cmd.Parameters.AddWithValue("@OGRTAD", txtad.Text);
                cmd.Parameters.AddWithValue("@OGRTSOYAD", txtsoyad.Text);
                cmd.Parameters.AddWithValue("@OGRTTC", txttcno.Text);
                cmd.Parameters.AddWithValue("@OGRTTEL", txttelefon.Text);
                cmd.Parameters.AddWithValue("@OGRTMAIL", txtmail.Text);
                cmd.Parameters.AddWithValue("@OGRTIL",int.Parse(txtil.EditValue.ToString()));
                cmd.Parameters.AddWithValue("@OGRTILCE",int.Parse(txtilce.EditValue.ToString()));
                cmd.Parameters.AddWithValue("@OGRTBRANS",int.Parse(txtbrans.EditValue.ToString()));
                cmd.Parameters.AddWithValue("@OGRTADRES", txtadres.Text);
                cmd.Parameters.AddWithValue("@OGRTFOTO", txtresimyol.Text);

                cmd.ExecuteNonQuery();

                File.Copy(resimokumayolu, hedefDosyaYolu, true); 

                listele();

                MessageBox.Show("Personel Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen Personel Resmi Seçiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


      
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(file.FileName);
                txtresimyol.Text = file.SafeFileName;
                pictureBox1.ImageLocation=file.FileName;
            }

        }


        public void satiryakala()
        {
            string yol = Application.StartupPath + "/Resimler/";  
            var resim = gridView1.GetFocusedRowCellValue("OGRTFOTO");
            txtad.Text = gridView1.GetFocusedRowCellValue("OGRTAD")?.ToString();
            txtsoyad.Text = gridView1.GetFocusedRowCellValue("OGRTSOYAD")?.ToString();
            txttcno.Text = gridView1.GetFocusedRowCellValue("OGRTTC")?.ToString();
            txttelefon.Text = gridView1.GetFocusedRowCellValue("OGRTTEL")?.ToString();
            txtmail.Text = gridView1.GetFocusedRowCellValue("OGRTMAIL")?.ToString();
            pictureBox1.ImageLocation = yol + resim;
            txtadres.Text = gridView1.GetFocusedRowCellValue("OGRTADRES")?.ToString();
            var ilValue = gridView1.GetFocusedRowCellValue("OGRTIL");
            if (int.TryParse(ilValue?.ToString(), out int ilResult))
            {
                txtil.EditValue = ilResult;
            }
            else
            {
                txtil.EditValue = null;

            }

            var ilceValue = gridView1.GetFocusedRowCellValue("OGRTILCE");
            if (int.TryParse(ilceValue?.ToString(), out int ilceResult))
            {
                txtilce.EditValue = ilceResult;
            }
            else
            {
                txtilce.EditValue = null;
            }

            var bransValue = gridView1.GetFocusedRowCellValue("OGRTBRANS");
            if (int.TryParse(bransValue?.ToString(), out int bransResult))
            {
                txtbrans.EditValue = bransResult;
            }
            else
            {
                txtbrans.EditValue = null;
            }

          
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {

            satiryakala();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string resimkayityeri = Application.StartupPath + "/Resimler/";
                string resimokumayolu = pictureBox1.ImageLocation;

                var id = gridView1.GetFocusedRowCellValue("OGRTID"); 
                SqlCommand cmd = new SqlCommand("UPDATE TBL_OGRETMENLER SET OGRTAD = @OGRTAD, OGRTSOYAD = @OGRTSOYAD, OGRTTC = @OGRTTC, OGRTTEL = @OGRTTEL, OGRTMAIL = @OGRTMAIL, OGRTIL = @OGRTIL, OGRTILCE = @OGRTILCE, OGRTADRES = @OGRTADRES, OGRTBRANS = @OGRTBRANS" +(string.IsNullOrEmpty(txtresimyol.Text) ? "" : ", OGRTFOTO = @OGRTFOTO") +" WHERE OGRTID = @OGRTID", Db.con());

                cmd.Parameters.AddWithValue("@OGRTAD", txtad.Text);
                cmd.Parameters.AddWithValue("@OGRTSOYAD", txtsoyad.Text);
                cmd.Parameters.AddWithValue("@OGRTTC", txttcno.Text);
                cmd.Parameters.AddWithValue("@OGRTTEL", txttelefon.Text);
                cmd.Parameters.AddWithValue("@OGRTMAIL", txtmail.Text);
                cmd.Parameters.AddWithValue("@OGRTIL", txtil.EditValue);
                cmd.Parameters.AddWithValue("@OGRTILCE", txtilce.EditValue);
                cmd.Parameters.AddWithValue("@OGRTADRES", txtadres.Text);
                cmd.Parameters.AddWithValue("@OGRTBRANS", txtbrans.EditValue);
                cmd.Parameters.AddWithValue("@OGRTID", id);

                if (!string.IsNullOrEmpty(txtresimyol.Text))
                {
                    cmd.Parameters.AddWithValue("@OGRTFOTO", txtresimyol.Text);

                    if (File.Exists(resimokumayolu))
                    {
                        if (File.Exists(resimokumayolu))
                        {
                            string hedefDosyaYolu = Path.Combine(resimkayityeri, Path.GetFileName(resimokumayolu));

                            pictureBox1.Image.Dispose();
                            pictureBox1.Image = null;

                            File.Copy(resimokumayolu, hedefDosyaYolu, true);
                        }


                    }
                    else
                    {
                        MessageBox.Show("Seçilen resim dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                cmd.ExecuteNonQuery();

                listele();

                MessageBox.Show("Güncelleme Başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void txtil_EditValueChanged_1(object sender, EventArgs e)
        {
            ilcecek();
        }

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            satiryakala();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili satır silinsin mi?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("delete from TBL_OGRETMENLER where OGRTID=@p1", Db.con());
                var id = gridView1.GetFocusedRowCellValue("OGRTID");
                cmd.Parameters.AddWithValue("@p1", id);
                cmd.ExecuteNonQuery();
                listele();
            }
           
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void txtresimyol_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
