using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMS_Gateway
{
    public partial class FrmAddCustomer : Form
    {
        AppData.CustomerProfile cp;
        public FrmAddCustomer()
        {
            InitializeComponent();
            cp = new SMS_Gateway.AppData.CustomerProfile();
        }
        public FrmAddCustomer(AppData.CustomerProfile cpx) : this()
        {
            cp = cpx;
            txtName.Text = cp.CpName;
            txtDileveryAddress.Text = cp.CpDeliveryAddress;
            txtBillAddress.Text = cp.CpBillAddress;
            txtMobile.Text = cp.CpMobileNumber;
            txtEmail.Text = cp.CpEmail;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void FrmAddCustomer_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cp.CpName = txtName.Text;
            cp.CpDeliveryAddress = txtDileveryAddress.Text;
            cp.CpBillAddress = txtBillAddress.Text;
            cp.CpMobileNumber = txtMobile.Text;
            cp.CpEmail = txtEmail.Text;

            cp.Save();
            DialogResult = DialogResult.OK;
        }
    }
}