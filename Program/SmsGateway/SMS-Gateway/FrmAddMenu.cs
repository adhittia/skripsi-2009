using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMS_Gateway
{
    public partial class FrmAddMenu : Form
    {
        private readonly AppData.Menu mn;
        public FrmAddMenu()
        {
            InitializeComponent();
            mn = new SMS_Gateway.AppData.Menu();
            
        }
        public FrmAddMenu(AppData.Menu mnx) : this()
        {
            //InitializeComponent();
            btnSave.Text = "Update";
            mn = mnx;
            txtMenuID.Text = mn.MenuId.ToString();
            txtMenuName.Text = mn.MName;
            txtDescription.Text = mn.MDescription;
            txtPrice.Text = mn.MPrice.ToString();
            cmbType.Text = mn.MType;
            cmbType.Text = mn.MCategory;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //AppData.Models.Menu mn = new SMS_Gateway.AppData.Models.Menu();
           
            mn.MName = txtMenuName.Text;
            mn.MDescription = txtDescription.Text;
            mn.MPrice = decimal.Parse(txtPrice.Text);
            mn.MType = cmbType.Text;
            mn.MCategory = cmbCategory.Text;
            
           
            mn.Save();
            DialogResult = DialogResult.OK;
        }
    }
}