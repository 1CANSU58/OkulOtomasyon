using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon
{
    public partial class ogrenciler : Form
    {
        public ogrenciler()
        {
            InitializeComponent();
        }

        private void ogrenciler_Load(object sender, EventArgs e)
        {
        
            listele();
            ilcek();
            velicek();  
        }

        public void ilcek()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from ILLER", Db.con());
            adapter.Fill(dt);
            txtil.Properties.DataSource = dt;
            txtil.Properties.DisplayMember = "ILADI";
            txtil.Properties.ValueMember = "ID";

        }

        public void velicek()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select VELIID,COALESCE(VELIANNE+' | '+VELIBABA,'')AS VELI from TBL_VELILER", Db.con());// iller tablosunu çek
            adapter.Fill(dt);
            txtveli.Properties.DataSource = dt;
            txtveli.Properties.DisplayMember = "VELI";
            txtveli.Properties.ValueMember = "VELIID";

        }

        public void ilcecek()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("select * from ILCELER WHERE ILID='" + txtil.EditValue + "'", Db.con());// iller tablosunu çek
            adapter.Fill(dt);
            txtilce.Properties.DataSource = dt;
            txtilce.Properties.DisplayMember = "ILCEADI";
            txtilce.Properties.ValueMember = "ILCEID";
        }

        public void listele()
        {
            

            string sorgu1 = @"select *,ILADI,ILCEADI,VELIANNE,VELIBABA,COALESCE(VELIANNE+' | '+VELIBABA,'')AS VELILER from TBL_OGRENCILER ogr  left join (select ID AS ILID,ILADI FROM ILLER) i on i.ILID=ogr.OGRIL left join (select ILCEID AS ILCID ,ILCEADI FROM ILCELER) iLC on iLC.ILCID=ogr.OGRILCE left join (select VELIID,VELIANNE,VELIBABA from  TBL_VELILER)AS VELILER ON VELILER.VELIID=ogr.VELIID WHERE OGRSINIF='5.SINIF'";
            string sorgu2 = @"select *,ILADI,ILCEADI,VELIANNE,VELIBABA,COALESCE(VELIANNE+' | '+VELIBABA,'')AS VELILER from TBL_OGRENCILER ogr  left join (select ID AS ILID,ILADI FROM ILLER) i on i.ILID=ogr.OGRIL left join (select ILCEID AS ILCID ,ILCEADI FROM ILCELER) iLC on iLC.ILCID=ogr.OGRILCE left join (select VELIID,VELIANNE,VELIBABA from  TBL_VELILER)AS VELILER ON VELILER.VELIID=ogr.VELIID WHERE OGRSINIF='6.SINIF'";
            string sorgu3 = @"select *,ILADI,ILCEADI,VELIANNE,VELIBABA,COALESCE(VELIANNE+' | '+VELIBABA,'')AS VELILER from TBL_OGRENCILER ogr  left join (select ID AS ILID,ILADI FROM ILLER) i on i.ILID=ogr.OGRIL left join (select ILCEID AS ILCID ,ILCEADI FROM ILCELER) iLC on iLC.ILCID=ogr.OGRILCE left join (select VELIID,VELIANNE,VELIBABA from  TBL_VELILER)AS VELILER ON VELILER.VELIID=ogr.VELIID WHERE OGRSINIF='7.SINIF'";
            string sorgu4 = @"select *,ILADI,ILCEADI,VELIANNE,VELIBABA,COALESCE(VELIANNE+' | '+VELIBABA,'')AS VELILER from TBL_OGRENCILER ogr  left join (select ID AS ILID,ILADI FROM ILLER) i on i.ILID=ogr.OGRIL left join (select ILCEID AS ILCID ,ILCEADI FROM ILCELER) iLC on iLC.ILCID=ogr.OGRILCE left join (select VELIID,VELIANNE,VELIBABA from  TBL_VELILER)AS VELILER ON VELILER.VELIID=ogr.VELIID WHERE OGRSINIF='8.SINIF'";
            DataTable dt1 = new DataTable();
            SqlDataAdapter adap1 = new SqlDataAdapter(sorgu1, Db.con());
            adap1.Fill(dt1);
            gridControl1.DataSource = dt1;


            DataTable dt2 = new DataTable();
            SqlDataAdapter adap2 = new SqlDataAdapter(sorgu2, Db.con());
            adap2.Fill(dt2);
            gridControl2.DataSource = dt2;

            DataTable dt3 = new DataTable();
            SqlDataAdapter adap3 = new SqlDataAdapter(sorgu3, Db.con());
            adap3.Fill(dt3);
            gridControl3.DataSource = dt3;


            DataTable dt4= new DataTable();
            SqlDataAdapter adap4 = new SqlDataAdapter(sorgu4, Db.con());
            adap4.Fill(dt4);
            gridControl4.DataSource = dt4;
            satiryakala1();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(file.FileName);
                txtresimyol.Text = file.SafeFileName;
                pictureBox1.ImageLocation = file.FileName;
            }
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

                string hedefDosyaYolu = Path.Combine(resimkayityeri, $"{Guid.NewGuid()}_{Path.GetFileName(resimokumayolu)}");


                if (txtsinif.Text=="")
                {
                    MessageBox.Show("Lütfen sınıf seçiniz");
                    return;
                }

                if (txtil.Text == "" || txtilce.Text=="")
                {
                    MessageBox.Show("Lütfen İl İlçe seçiniz");
                    return;
                }
     
    string query = "INSERT INTO TBL_OGRENCILER (OGRAD, OGRSOYAD, OGRNO, OGRDTARIHI, OGRTC, OGRSINIF, OGRCINSIYET, OGRIL, OGRILCE, VELIID" +
                   (string.IsNullOrEmpty(txtresimyol.Text) ? "" : ", OGRFOTO") + ") " +
                   "VALUES (@OGRAD, @OGRSOYAD, @OGRNO, @OGRDTARIHI, @OGRTC, @OGRSINIF, @OGRCINSIYET, @OGRIL, @OGRILCE, @VELIID" +
                   (string.IsNullOrEmpty(txtresimyol.Text) ? "" : ", @OGRFOTO") + ")";

                SqlCommand cmd = new SqlCommand(query, Db.con());

                cmd.Parameters.AddWithValue("@OGRAD", txtad.Text);
                cmd.Parameters.AddWithValue("@OGRSOYAD", txtsoyad.Text);
                cmd.Parameters.AddWithValue("@OGRNO", txtogrno.Text);
                cmd.Parameters.AddWithValue("@OGRDTARIHI", txtdtarihi.Text);
                cmd.Parameters.AddWithValue("@OGRTC", txttcno.Text);
                cmd.Parameters.AddWithValue("@OGRSINIF", txtsinif.Text);
                cmd.Parameters.AddWithValue("@OGRIL", int.Parse(txtil.EditValue.ToString()));
                cmd.Parameters.AddWithValue("@OGRILCE", int.Parse(txtilce.EditValue.ToString()));

                if (rberkek.Checked)
                {
                    cmd.Parameters.AddWithValue("@OGRCINSIYET", "Erkek");
                }
                else if (rbbayan.Checked)
                {
                    cmd.Parameters.AddWithValue("@OGRCINSIYET", "Bayan");
                }
                else
                {
                    MessageBox.Show("Cinsiyet seçiniz");
                    return;
                }

                if (txtveli.Text == "")
                {
                    MessageBox.Show("Lütfen veli seçiniz");
                    return;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@VELIID", int.Parse(txtveli.EditValue.ToString()));
                }

                if (!string.IsNullOrEmpty(txtresimyol.Text))
                {
                    cmd.Parameters.AddWithValue("@OGRFOTO", txtresimyol.Text);
                }

                cmd.ExecuteNonQuery();

                File.Copy(resimokumayolu, hedefDosyaYolu, true);

                listele();

                MessageBox.Show("Öğrenci Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen Öğrenci Resmi Seçiniz", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seçili satır silinsin mi?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("delete from TBL_OGRENCILER where OGRID=@p1", Db.con());
                var id = gridView1.GetFocusedRowCellValue("OGRID");
                cmd.Parameters.AddWithValue("@p1", id);
                cmd.ExecuteNonQuery();
                listele();
            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string resimkayityeri = Application.StartupPath + "/Resimler/";
            string resimokumayolu = pictureBox1.ImageLocation;

            if (!Directory.Exists(resimkayityeri))
            {
                Directory.CreateDirectory(resimkayityeri);
            }

            if (string.IsNullOrWhiteSpace(txtsinif.Text))
            {
                MessageBox.Show("Lütfen sınıf seçiniz");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtil.Text) || string.IsNullOrWhiteSpace(txtilce.Text))
            {
                MessageBox.Show("Lütfen İl ve İlçe seçiniz");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtveli.Text))
            {
                MessageBox.Show("Lütfen veli seçiniz");
                return;
            }

            if (!rberkek.Checked && !rbbayan.Checked)
            {
                MessageBox.Show("Lütfen cinsiyet seçiniz");
                return;
            }

            string query = "UPDATE TBL_OGRENCILER SET " +
                "OGRAD = @OGRAD, OGRSOYAD = @OGRSOYAD, OGRNO = @OGRNO, OGRDTARIHI = @OGRDTARIHI, " +
                "OGRTC = @OGRTC, OGRSINIF = @OGRSINIF, OGRCINSIYET = @OGRCINSIYET, OGRIL = @OGRIL, " +
                "OGRILCE = @OGRILCE,VELIID=@VELIID";

            if (!string.IsNullOrWhiteSpace(txtresimyol.Text))
            {
                query += ", OGRFOTO = @OGRFOTO";
            }

            query += " WHERE OGRID = @OGRID";

            SqlCommand cmd = new SqlCommand(query, Db.con());

            cmd.Parameters.AddWithValue("@OGRAD", txtad.Text.Trim());
            cmd.Parameters.AddWithValue("@OGRSOYAD", txtsoyad.Text.Trim());
            cmd.Parameters.AddWithValue("@OGRNO", txtogrno.Text.Trim());
            cmd.Parameters.AddWithValue("@OGRDTARIHI", txtdtarihi.Text.Trim());
            cmd.Parameters.AddWithValue("@OGRTC", txttcno.Text.Trim());
            cmd.Parameters.AddWithValue("@OGRSINIF", txtsinif.Text.Trim());
            cmd.Parameters.AddWithValue("@OGRIL", int.Parse(txtil.EditValue.ToString()));
            cmd.Parameters.AddWithValue("@OGRILCE", int.Parse(txtilce.EditValue.ToString()));
            cmd.Parameters.AddWithValue("@VELIID", int.Parse(txtveli.EditValue.ToString()));
            

            if (!string.IsNullOrEmpty(txtresimyol.Text))
            {
                cmd.Parameters.AddWithValue("@OGRFOTO", txtresimyol.Text);

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

            cmd.Parameters.AddWithValue("@OGRID", int.Parse(txtogrid.Text.Trim()));

            if (rberkek.Checked)
            {
                cmd.Parameters.AddWithValue("@OGRCINSIYET", rberkek.Text);
            }
            else if (rbbayan.Checked)
            {
                cmd.Parameters.AddWithValue("@OGRCINSIYET", rbbayan.Text);
            }


            try
            {
                cmd.ExecuteNonQuery();

                listele();
                txtresimyol.Text = "";
                MessageBox.Show("Öğrenci bilgileri güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void txtil_EditValueChanged(object sender, EventArgs e)
        {
            ilcecek();
        }

        private void xtraTabPage1_Click(object sender, EventArgs e)
        {
            listele();
         
        }

        private void xtraTabPage2_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            listele();
       
        }

        private void xtraTabControl1_MouseClick(object sender, MouseEventArgs e)
        {
       
        }
        public void satiryakala1()
        {

            string yol = Application.StartupPath + "/Resimler/";

            var resim = gridView1.GetFocusedRowCellValue("OGRFOTO");
            txtad.Text = gridView1.GetFocusedRowCellValue("OGRAD")?.ToString() ?? "Ad Bilinmiyor";
            txtsoyad.Text = gridView1.GetFocusedRowCellValue("OGRSOYAD")?.ToString() ?? "Soyad Bilinmiyor";
            txttcno.Text = gridView1.GetFocusedRowCellValue("OGRTC")?.ToString() ?? "TC No Yok";
            txtogrno.Text = gridView1.GetFocusedRowCellValue("OGRNO")?.ToString() ?? "Öğrenci No Yok";

            txtsinif.Text= gridView1.GetFocusedRowCellValue("OGRSINIF")?.ToString() ?? "Sınıf Bilgisi yok";
            txtogrid.Text= gridView1.GetFocusedRowCellValue("OGRID")?.ToString() ?? "";
            string veliIdValue = gridView1.GetFocusedRowCellValue("VELIID")?.ToString();

        
            if (int.TryParse(veliIdValue, out int veliId))
            {
                txtveli.EditValue = veliId;
            }
            else
            {
                txtveli.EditValue = 0; 
            }
            if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString()=="Erkek")
            {
                rberkek.Checked=true;
                rbbayan.Checked=false;
            }
            else if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString() == "Bayan")
            {
                rberkek.Checked = false;
                rbbayan.Checked = true;
            }

            var dtarihValue = gridView1.GetFocusedRowCellValue("OGRDTARIHI");
            if (DateTime.TryParse(dtarihValue?.ToString(), out DateTime dtarihResult))
            {
                txtdtarihi.Text = dtarihResult.ToShortDateString();
            }
            else
            {
                txtdtarihi.Text = ""; 
            }

           
            if (!string.IsNullOrEmpty(resim?.ToString()))
            {
                pictureBox1.ImageLocation = yol + resim;
            }
            else
            {
               
            }

            // İl bilgisi kontrolü ve ComboBox'a atama
            var ilValue = gridView1.GetFocusedRowCellValue("OGRIL");
            if (int.TryParse(ilValue?.ToString(), out int ilResult))
            {
                txtil.EditValue = ilResult;
            }
            else
            {
                txtil.EditValue = null; 
            }

            // İlçe bilgisi kontrolü ve ComboBox'a atama
            var ilceValue = gridView1.GetFocusedRowCellValue("OGRILCE");
            if (int.TryParse(ilceValue?.ToString(), out int ilceResult))
            {
                txtilce.EditValue = ilceResult; 
            }
            else
            {
                txtilce.EditValue = null; 
            }

           
            try
            {
                // Özel bir alanı loglayabiliriz
                var logValue = gridView1.GetFocusedRowCellValue("LOGALAN");
                if (logValue == null)
                {
                    Console.WriteLine("LOGALAN bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }





        }

        public void satiryakala2()
        {

            string yol = Application.StartupPath + "/Resimler/";

            var resim = gridView2.GetFocusedRowCellValue("OGRFOTO");
            txtad.Text = gridView2.GetFocusedRowCellValue("OGRAD")?.ToString() ?? "Ad Bilinmiyor";
            txtsoyad.Text = gridView2.GetFocusedRowCellValue("OGRSOYAD")?.ToString() ?? "Soyad Bilinmiyor";
            txttcno.Text = gridView2.GetFocusedRowCellValue("OGRTC")?.ToString() ?? "TC No Yok";
            txtogrno.Text = gridView2.GetFocusedRowCellValue("OGRNO")?.ToString() ?? "Öğrenci No Yok";

            txtsinif.Text = gridView2.GetFocusedRowCellValue("OGRSINIF")?.ToString() ?? "Sınıf Bilgisi yok";
            txtogrid.Text = gridView2.GetFocusedRowCellValue("OGRID")?.ToString() ?? "";
            string veliIdValue = gridView2.GetFocusedRowCellValue("VELIID")?.ToString();


            if (int.TryParse(veliIdValue, out int veliId))
            {
                txtveli.EditValue = veliId;
            }
            else
            {
                txtveli.EditValue = 0;
            }
            if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString() == "Erkek")
            {
                rberkek.Checked = true;
                rbbayan.Checked = false;
            }
            else if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString() == "Bayan")
            {
                rberkek.Checked = false;
                rbbayan.Checked = true;
            }

            var dtarihValue = gridView2.GetFocusedRowCellValue("OGRDTARIHI");
            if (DateTime.TryParse(dtarihValue?.ToString(), out DateTime dtarihResult))
            {
                txtdtarihi.Text = dtarihResult.ToShortDateString();
            }
            else
            {
                txtdtarihi.Text = "";
            }


            if (!string.IsNullOrEmpty(resim?.ToString()))
            {
                pictureBox1.ImageLocation = yol + resim;
            }
            else
            {

            }

            // İl bilgisi kontrolü ve ComboBox'a atama
            var ilValue = gridView2.GetFocusedRowCellValue("OGRIL");
            if (int.TryParse(ilValue?.ToString(), out int ilResult))
            {
                txtil.EditValue = ilResult;
            }
            else
            {
                txtil.EditValue = null;
            }

            // İlçe bilgisi kontrolü ve ComboBox'a atama
            var ilceValue = gridView2.GetFocusedRowCellValue("OGRILCE");
            if (int.TryParse(ilceValue?.ToString(), out int ilceResult))
            {
                txtilce.EditValue = ilceResult;
            }
            else
            {
                txtilce.EditValue = null;
            }


            try
            {
                // Özel bir alanı loglayabiliriz
                var logValue = gridView2.GetFocusedRowCellValue("LOGALAN");
                if (logValue == null)
                {
                    Console.WriteLine("LOGALAN bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }





        }

        public void satiryakala3()
        {

            string yol = Application.StartupPath + "/Resimler/";

            var resim = gridView3.GetFocusedRowCellValue("OGRFOTO");
            txtad.Text = gridView3.GetFocusedRowCellValue("OGRAD")?.ToString() ?? "Ad Bilinmiyor";
            txtsoyad.Text = gridView3.GetFocusedRowCellValue("OGRSOYAD")?.ToString() ?? "Soyad Bilinmiyor";
            txttcno.Text = gridView3.GetFocusedRowCellValue("OGRTC")?.ToString() ?? "TC No Yok";
            txtogrno.Text = gridView3.GetFocusedRowCellValue("OGRNO")?.ToString() ?? "Öğrenci No Yok";

            txtsinif.Text = gridView3.GetFocusedRowCellValue("OGRSINIF")?.ToString() ?? "Sınıf Bilgisi yok";
            txtogrid.Text = gridView3.GetFocusedRowCellValue("OGRID")?.ToString() ?? "";
            string veliIdValue = gridView3.GetFocusedRowCellValue("VELIID")?.ToString();


            if (int.TryParse(veliIdValue, out int veliId))
            {
                txtveli.EditValue = veliId;
            }
            else
            {
                txtveli.EditValue = 0;
            }
            if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString() == "Erkek")
            {
                rberkek.Checked = true;
                rbbayan.Checked = false;
            }
            else if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString() == "Bayan")
            {
                rberkek.Checked = false;
                rbbayan.Checked = true;
            }

            var dtarihValue = gridView3.GetFocusedRowCellValue("OGRDTARIHI");
            if (DateTime.TryParse(dtarihValue?.ToString(), out DateTime dtarihResult))
            {
                txtdtarihi.Text = dtarihResult.ToShortDateString();
            }
            else
            {
                txtdtarihi.Text = "";
            }


            if (!string.IsNullOrEmpty(resim?.ToString()))
            {
                pictureBox1.ImageLocation = yol + resim;
            }
            else
            {

            }

            // İl bilgisi kontrolü ve ComboBox'a atama
            var ilValue = gridView3.GetFocusedRowCellValue("OGRIL");
            if (int.TryParse(ilValue?.ToString(), out int ilResult))
            {
                txtil.EditValue = ilResult;
            }
            else
            {
                txtil.EditValue = null;
            }

            // İlçe bilgisi kontrolü ve ComboBox'a atama
            var ilceValue = gridView3.GetFocusedRowCellValue("OGRILCE");
            if (int.TryParse(ilceValue?.ToString(), out int ilceResult))
            {
                txtilce.EditValue = ilceResult;
            }
            else
            {
                txtilce.EditValue = null;
            }


            try
            {
                // Özel bir alanı loglayabiliriz
                var logValue = gridView3.GetFocusedRowCellValue("LOGALAN");
                if (logValue == null)
                {
                    Console.WriteLine("LOGALAN bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }





        }

        public void satiryakala4()
        {

            string yol = Application.StartupPath + "/Resimler/";

            var resim = gridView4.GetFocusedRowCellValue("OGRFOTO");
            txtad.Text = gridView4.GetFocusedRowCellValue("OGRAD")?.ToString() ?? "Ad Bilinmiyor";
            txtsoyad.Text = gridView4.GetFocusedRowCellValue("OGRSOYAD")?.ToString() ?? "Soyad Bilinmiyor";
            txttcno.Text = gridView4.GetFocusedRowCellValue("OGRTC")?.ToString() ?? "TC No Yok";
            txtogrno.Text = gridView4.GetFocusedRowCellValue("OGRNO")?.ToString() ?? "Öğrenci No Yok";

            txtsinif.Text = gridView4.GetFocusedRowCellValue("OGRSINIF")?.ToString() ?? "Sınıf Bilgisi yok";
            txtogrid.Text = gridView4.GetFocusedRowCellValue("OGRID")?.ToString() ?? "";
            string veliIdValue = gridView4.GetFocusedRowCellValue("VELIID")?.ToString();


            if (int.TryParse(veliIdValue, out int veliId))
            {
                txtveli.EditValue = veliId;
            }
            else
            {
                txtveli.EditValue = 0;
            }
            if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString() == "Erkek")
            {
                rberkek.Checked = true;
                rbbayan.Checked = false;
            }
            else if (gridView1.GetFocusedRowCellValue("OGRCINSIYET")?.ToString() == "Bayan")
            {
                rberkek.Checked = false;
                rbbayan.Checked = true;
            }

            var dtarihValue = gridView4.GetFocusedRowCellValue("OGRDTARIHI");
            if (DateTime.TryParse(dtarihValue?.ToString(), out DateTime dtarihResult))
            {
                txtdtarihi.Text = dtarihResult.ToShortDateString();
            }
            else
            {
                txtdtarihi.Text = "";
            }


            if (!string.IsNullOrEmpty(resim?.ToString()))
            {
                pictureBox1.ImageLocation = yol + resim;
            }
            else
            {

            }

            // İl bilgisi kontrolü ve ComboBox'a atama
            var ilValue = gridView4.GetFocusedRowCellValue("OGRIL");
            if (int.TryParse(ilValue?.ToString(), out int ilResult))
            {
                txtil.EditValue = ilResult;
            }
            else
            {
                txtil.EditValue = null;
            }

            // İlçe bilgisi kontrolü ve ComboBox'a atama
            var ilceValue = gridView4.GetFocusedRowCellValue("OGRILCE");
            if (int.TryParse(ilceValue?.ToString(), out int ilceResult))
            {
                txtilce.EditValue = ilceResult;
            }
            else
            {
                txtilce.EditValue = null;
            }


            try
            {
                // Özel bir alanı loglayabiliriz
                var logValue = gridView4.GetFocusedRowCellValue("LOGALAN");
                if (logValue == null)
                {
                    Console.WriteLine("LOGALAN bilgisi bulunamadı.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }





        }








        private void gridControl1_Click(object sender, EventArgs e)
        {
            satiryakala1();
        }

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            satiryakala1();
        }
        void temizle()
        {

            txtad.Text = "";
            txtsoyad.Text = "";
            txttcno.Text = "";
            txtogrno.Text = "";
            txtil.EditValue = null;
            txtilce.EditValue = null;
            pictureBox1.ImageLocation = null;
            txtad.Focus();




        }
        private void simpleButton5_Click(object sender, EventArgs e)
        {
            temizle();  
        }

        private void xtraTabPage2_Click_1(object sender, EventArgs e)
        {
            listele();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            satiryakala2();
        }

        private void gridControl3_Click(object sender, EventArgs e)
        {
            satiryakala3();
        }

        private void gridControl4_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridControl4_Click(object sender, EventArgs e)
        {
            satiryakala4();
        }
    }
}
