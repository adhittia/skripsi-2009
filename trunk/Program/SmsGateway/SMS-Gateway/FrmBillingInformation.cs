using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace SMS_Gateway
{
    public partial class FrmBillingInformation : Form
    {
        public FrmBillingInformation()
        {
            InitializeComponent();
        }

        private void FrmBillingInformation_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (AppData.CustomerProfile cp in AppData.CustomerProfile.FindAll())
            {

                comboBox1.Items.Add(cp.CustomerId.ToString() + "." + cp.CpName);
            }
            comboBox1.SelectedIndex = 0;
            drawReport();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            drawReport();

        }

        void drawReport()
        {
            string sel = comboBox1.Text.Split(new String[] { "." },StringSplitOptions.RemoveEmptyEntries)[0].ToString();
            AppData.CustomerProfile cp = AppData.CustomerProfile.Find(int.Parse(sel));

            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("nama", cp.CpName);
            parameters[1] = new ReportParameter("deliveryaddress", cp.CpDeliveryAddress);
            parameters[2] = new ReportParameter("billaddress", cp.CpBillAddress);
            parameters[3] = new ReportParameter("mobilenumber", cp.CpMobileNumber);
            parameters[4] = new ReportParameter("email", cp.CpEmail);
            this.reportViewer1.LocalReport.SetParameters(parameters);

            this.viewbillingTableAdapter.Fill(billingInfo.viewbilling, int.Parse(sel), comboBox3.Text.Trim() + "-" + comboBox2.Text.Trim());

            this.reportViewer1.RefreshReport();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawReport();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            drawReport();
        }
    }
}