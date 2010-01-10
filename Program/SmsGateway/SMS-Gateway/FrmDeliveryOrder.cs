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
    public partial class FrmDeliveryOrder : Form
    {
        public FrmDeliveryOrder()
        {
            InitializeComponent();
        }

        private void FrmDeliveryOrder_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'deliveryReport.customer_profile' table. You can move, or remove it, as needed.
            this.customer_profileTableAdapter.Fill(this.deliveryReport.customer_profile);
            //// TODO: This line of code loads data into the 'deliveryReport.customer_profile' table. You can move, or remove it, as needed.
            //this.customer_profileTableAdapter.Fill(this.deliveryReport.customer_profile);
            //// TODO: This line of code loads data into the 'deliveryReport.customer_profile' table. You can move, or remove it, as needed.
            //this.customer_profileTableAdapter.Fill(this.deliveryReport.customer_profile);

            
            //this.reportViewer1.RefreshReport();
            this.reportViewer1.LocalReport.SubreportProcessing += new Microsoft.Reporting.WinForms.SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            //object Dr
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }

        void LocalReport_SubreportProcessing(object sender, Microsoft.Reporting.WinForms.SubreportProcessingEventArgs e)
        {
            this.viewdeliveryorderdetilTableAdapter.Fill(this.deliveryReportDetil.viewdeliveryorderdetil);
            e.DataSources.Add(new ReportDataSource("DeliveryReportDetil_viewdeliveryorderdetil",this.deliveryReportDetil.viewdeliveryorderdetil));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.PEMBAYARANTableAdapter.Fill(this.dsPenerimaan.PEMBAYARAN);
            //this.customer_profileTableAdapter.Fill(this.deliveryReport.customer_profile);
            this.reportViewer1.PrintDialog();
            //this.reportViewer1.RefreshReport();
           
        }

       
    }
}