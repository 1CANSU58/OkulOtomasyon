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
    }
}
