using DevExpress.XtraGrid;
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
    public partial class ayarlar : Form
    {
        public ayarlar()
        {
            InitializeComponent();
        }
        public void ogrtlistele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter("select * from TBL_OGRETMENLER", Db.con());
            adap.Fill(dt);
            gridControl1.DataSource = dt;

        }
        public void ogrlistele()
        {


            string sorgu1 = @"select *,ILADI,ILCEADI,VELIANNE,VELIBABA,COALESCE(VELIANNE+' | '+VELIBABA,'')AS VELILER,OGRSIFRE from TBL_OGRENCILER ogr  left join (select ID AS ILID,ILADI FROM ILLER) i on i.ILID=ogr.OGRIL left join (select ILCEID AS ILCID ,ILCEADI FROM ILCELER) iLC on iLC.ILCID=ogr.OGRILCE left join (select VELIID,VELIANNE,VELIBABA from  TBL_VELILER)AS VELILER ON VELILER.VELIID=ogr.VELIID";
          
            DataTable dt1 = new DataTable();
            SqlDataAdapter adap1 = new SqlDataAdapter(sorgu1, Db.con());
            adap1.Fill(dt1);
            gridControl2.DataSource = dt1;


        }


        void ogretmenlookup()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter("select * from TBL_OGRETMENLER", Db.con());
            adap.Fill(dt);
            txtogrtmenLookup.Properties.DisplayMember = "OGRTAD";
            txtogrtmenLookup.Properties.ValueMember = "OGRTID";
            txtogrtmenLookup.Properties.DataSource = dt;
        }
        void ogrlookup()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter("select * from TBL_OGRENCILER", Db.con());
            adap.Fill(dt);
            txtogrlookup.Properties.DisplayMember = "OGRAD";
            txtogrlookup.Properties.ValueMember = "OGRID";
            txtogrlookup.Properties.DataSource = dt;
        }


        private void ayarlar_Load(object sender, EventArgs e)
        {
            
           
            ogrtlistele();
            ogrlistele();
            ogretmenlookup();
            ogrlookup();
        }


        void ogretmenbilgileriata()
        {
            var dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtogrtid.Text = dr["OGRTID"].ToString();
                txtogrtmensifre.Text = dr["OGRTSIFRE"].ToString();
                txtogrtmenLookup.EditValue = dr["OGRTID"].ToString();
            }
          
       
        
        }

        void ogrbilgileriata()
        {
            var dr = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            if (dr != null)
            {
                txtogrid.Text = dr["OGRID"].ToString();
                txtogrsifre.Text = dr["OGRSIFRE"].ToString();
                txtogrsinifi.Text = dr["OGRSINIF"].ToString();
                txtogrlookup.EditValue = dr["OGRID"].ToString();
            }
       


        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            ogretmenbilgileriata();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            ogrbilgileriata();
        }

        private void gridControl2_KeyUp(object sender, KeyEventArgs e)
        {
            ogrbilgileriata();
        }

        private void gridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            ogrbilgileriata();
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {
            ogretmenbilgileriata();
        }

        private void gridControl1_KeyUp_1(object sender, KeyEventArgs e)
        {
            ogretmenbilgileriata();
        }

        private void gridControl2_Click_1(object sender, EventArgs e)
        {
            ogrbilgileriata();
        }

        private void gridControl2_KeyUp_1(object sender, KeyEventArgs e)
        {
            ogrbilgileriata();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (txtogrtmensifre.Text=="")
            {
                MessageBox.Show("şifre giriniz");
                return;
            }

            SqlCommand cmd = new SqlCommand("update TBL_OGRETMENLER SET OGRTSIFRE=@OGRTSIFRE WHERE OGRTID=@OGRTID", Db.con());
            cmd.Parameters.AddWithValue("OGRTSIFRE", txtogrtmensifre.Text);
            cmd.Parameters.AddWithValue("OGRTID", txtogrtid.Text);
            ogrtlistele();
          
            cmd.ExecuteNonQuery();
            MessageBox.Show("Öğretmen Şifre Güncellendi");
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            if (txtogrsifre.Text == "")
            {
                MessageBox.Show("şifre giriniz");
                return;
            }

            SqlCommand cmd = new SqlCommand("update TBL_OGRENCILER SET OGRSIFRE=@OGRSIFRE WHERE OGRID=@OGRID", Db.con());
            cmd.Parameters.AddWithValue("OGRSIFRE", txtogrsifre.Text);
            cmd.Parameters.AddWithValue("OGRID", txtogrid.Text);
            cmd.ExecuteNonQuery();

            ogrlistele();
            MessageBox.Show("Öğrenci Şifre Güncellendi");
      
        }

        private void gridControl2_Click_2(object sender, EventArgs e)
        {
            ogrbilgileriata();
        }

        private void gridControl2_KeyUp_2(object sender, KeyEventArgs e)
        {
            ogrbilgileriata();
        }

        private void gridControl2_Click_3(object sender, EventArgs e)
        {
            ogrbilgileriata();
        }
    }
}
