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
            // TODO: This line of code loads data into the 'okulOtomasyonDataSet.TBL_OGRETMENLER' table. You can move, or remove it, as needed.
            this.tBL_OGRETMENLERTableAdapter.Fill(this.okulOtomasyonDataSet.TBL_OGRETMENLER);
            ilcek();
            bransgetir();
            //satiryakala();
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
            DataTable dt=new DataTable();// boş tablo oluştur
            SqlDataAdapter adapter = new SqlDataAdapter("select * from ILLER",Db.con());// iller tablosunu çek
            adapter.Fill(dt);// adaptaki bilgileri boş tabloya doldur
           txtil.Properties.DataSource= dt;
            txtil.Properties.DisplayMember= "ILADI";
            txtil.Properties.ValueMember= "ID";

        }

        public void ilcecek()
        {
            DataTable dt = new DataTable();// boş tablo oluştur
            SqlDataAdapter adapter = new SqlDataAdapter("select * from ILCELER WHERE ILID='"+txtil.EditValue+"'", Db.con());// iller tablosunu çek
            adapter.Fill(dt);// adaptaki bilgileri boş tabloya doldur
            txtilce.Properties.DataSource = dt;
            txtilce.Properties.DisplayMember = "ILCEADI";
            txtilce.Properties.ValueMember = "ILCEID";
        }


        public void bransgetir() 
        {
            DataTable dt = new DataTable();// boş tablo oluştur
            SqlDataAdapter adapter = new SqlDataAdapter("select * from TBL_BRANSLAR", Db.con());// iller tablosunu çek
            adapter.Fill(dt);// adaptaki bilgileri boş tabloya doldur
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

            // Hedef klasörün var olup olmadığını kontrol et
            if (!Directory.Exists(resimkayityeri))
            {
                Directory.CreateDirectory(resimkayityeri); // Eğer yoksa oluştur
            }

            // Dosya yolunun doğru olduğundan emin olun
            if (File.Exists(resimokumayolu))
            {
          
                // Hedef dosya yolunu oluşturun
                string hedefDosyaYolu = Path.Combine(resimkayityeri, Path.GetFileName(resimokumayolu));

                // SQL komutunu oluşturun
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

                // Veritabanına kaydet
                cmd.ExecuteNonQuery();

                // Dosyayı hedef klasöre kopyala
                File.Copy(resimokumayolu, hedefDosyaYolu, true); // 'true' overwrite (üstüne yazma) işlemi yapar

                // Listeyi yenile
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
                // image dosya yolunu text de göster  
                txtresimyol.Text = file.SafeFileName;
                pictureBox1.ImageLocation=file.FileName;
            }

        }


        public void satiryakala()
        {
            string yol = Application.StartupPath + "/Resimler/";  
            var resim = gridView1.GetFocusedRowCellValue("OGRTFOTO");
            txtad.Text = gridView1.GetFocusedRowCellValue("OGRTAD").ToString();
            txtsoyad.Text = gridView1.GetFocusedRowCellValue("OGRTSOYAD").ToString();
            txttcno.Text = gridView1.GetFocusedRowCellValue("OGRTTC")?.ToString();
            txttelefon.Text = gridView1.GetFocusedRowCellValue("OGRTTEL").ToString();
            txtmail.Text = gridView1.GetFocusedRowCellValue("OGRTMAIL").ToString();
            pictureBox1.ImageLocation = yol + resim;
            txtadres.Text = gridView1.GetFocusedRowCellValue("OGRTADRES")?.ToString();
            // txtil ve txtilce için null kontrolü
            var ilValue = gridView1.GetFocusedRowCellValue("OGRTIL");
            if (int.TryParse(ilValue?.ToString(), out int ilResult))
            {
                txtil.EditValue = ilResult;
            }
            else
            {
                txtil.EditValue = null; // veya varsayılan değer

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

            // txtbrans için aynı kontrol
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
            var id = gridView1.GetFocusedRowCellValue("OGRTID");// gridView deki konumlandığı satırın hücre değerini getir
            SqlCommand cmd = new SqlCommand("UPDATE TBL_OGRETMENLER SET OGRTAD = @OGRTAD, OGRTSOYAD = @OGRTSOYAD, OGRTTC = @OGRTTC, OGRTTEL = @OGRTTEL, OGRTMAIL = @OGRTMAIL, OGRTIL = @OGRTIL, OGRTILCE = @OGRTILCE, OGRTADRES = @OGRTADRES, OGRTBRANS = @OGRTBRANS, OGRTFOTO = @OGRTFOTO WHERE OGRTID = @OGRTID", Db.con());

            // Parametreleri ekle
            cmd.Parameters.AddWithValue("@OGRTAD", txtad.Text);
           
            cmd.Parameters.AddWithValue("@OGRTSOYAD", txtsoyad.Text);
            cmd.Parameters.AddWithValue("@OGRTTC", txttcno.Text);
            cmd.Parameters.AddWithValue("@OGRTTEL", txttelefon.Text);
            cmd.Parameters.AddWithValue("@OGRTMAIL", txtmail.Text);
            cmd.Parameters.AddWithValue("@OGRTIL", txtil.EditValue);
            cmd.Parameters.AddWithValue("@OGRTILCE", txtilce.EditValue);
            cmd.Parameters.AddWithValue("@OGRTADRES", txtadres.Text);
            cmd.Parameters.AddWithValue("@OGRTBRANS", txtbrans.EditValue);
            cmd.Parameters.AddWithValue("@OGRTFOTO", txtresimyol.Text);

        
            cmd.Parameters.AddWithValue("@OGRTID", id);  
            // Veritabanını güncelle
            cmd.ExecuteNonQuery();
            listele();

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
            SqlCommand cmd = new SqlCommand("delete from TBL_OGRETMENLER where OGRTID=@p1", Db.con());
            var id = gridView1.GetFocusedRowCellValue("OGRTID");
            cmd.Parameters.AddWithValue("@p1", id);
            cmd.ExecuteNonQuery();
            listele();
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
