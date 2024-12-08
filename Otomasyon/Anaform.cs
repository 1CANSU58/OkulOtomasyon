using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Otomasyon
{
    public partial class Anaform : Form
    {
        public Anaform()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmogretmenler frm = Application.OpenForms["frmogretmenler"]as frmogretmenler;
            if (frm!=null)
            {
               
                frm.Activate();
            }
            else
            {
                frm =new frmogretmenler();
                 frm.MdiParent = this;
                frm.Show();
            }
           
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ogrenciler frm = Application.OpenForms["ogrenciler"] as ogrenciler;
            if (frm != null)
            {

                frm.Activate();
            }
            else
            {
                frm = new ogrenciler();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            veliler frm = Application.OpenForms["veliler"] as veliler;
            if (frm != null)
            {

                frm.Activate();
            }
            else
            {
                frm = new veliler();
                frm.MdiParent = this;
                frm.Show();
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ayarlar frm = Application.OpenForms["ayarlar"] as ayarlar;
            if (frm != null)
            {

                frm.Activate();
            }
            else
            {
                frm = new ayarlar();
                frm.MdiParent = this;
                frm.Show();
            }
        }
    }
}
