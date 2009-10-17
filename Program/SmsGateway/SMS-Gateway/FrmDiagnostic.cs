using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic;

using ATSMS;
using ATSMS.Common;
using ATSMS.SMS.Decoder;
using ATSMS.SMS.Encoder;

namespace SMS_Gateway.FormDiagnostic
{
    public partial class FrmDiagnostic : Form
    {
        private String dialogCaption = "SMS Gateway";
        public FrmDiagnostic()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDiagnostic_Load(object sender, EventArgs e)
        {

        }

        public void getModemDetails(GSMModem oGsmModem) 
        {
            if (oGsmModem == null) 
            {
                MessageBox.Show("Modem object is Null", dialogCaption, MessageBoxButtons.OK);
                this.Close();
            }

            if (!oGsmModem.IsConnected)
            {
                MessageBox.Show("Modem is not connected", dialogCaption, MessageBoxButtons.OK);
                this.Close();
            }

             try {
                 txtRevision.Text = oGsmModem.Revision;
             }
             catch (Exception ex) {
                 txtRevision.Text = "Not supported";
             }
            
             try {
                 txtIMSI.Text = oGsmModem.IMSI;
             }
             catch (Exception ex) {
                 txtIMSI.Text = "Not supported";
             }
            
             try {
                 txtIMEI.Text = oGsmModem.IMEI;
             }
             catch (Exception ex) {
                 txtIMEI.Text = "Not supported";
             }
            
             try {
                 txtModel.Text = oGsmModem.PhoneModel;
             }
             catch (Exception ex) {
                 txtModel.Text = "Not supported";
             }
            
             try {
                 txtManufacturer.Text = oGsmModem.Manufacturer;
             }
             catch (Exception ex) {
                 txtManufacturer.Text = "Not supported";
             }
            
             try {
                 txtSMSC.Text = oGsmModem.SMSC;
             }
             catch (Exception ex) {
                 txtSMSC.Text = "Not supported";
             }
            
             try {
                 Rssi rssi = oGsmModem.GetRssi();
                 txtSignal.Text = rssi.Current + " of " + rssi.Maximum;
             }
             catch (Exception ex) {
                 txtSignal.Text = "Not supported";
             }
            
             try {
                 Storage[] storages = oGsmModem.GetStorageSetting();
                 int i = 0;
                 txtSupportedStorage.Text = string.Empty;
                 for (i = 0; i <= storages.Length - 1; i++) {
                     Storage storage = storages[i];
                     txtSupportedStorage.Text += storage.Name + "(" + storage.Used + "/" + storage.Total + "), ";
                 }
             }
             catch (Exception ex) {
                 txtSupportedStorage.Text = "Not supported";
             }
             
        }

        private void txtSignal_TextChanged(object sender, EventArgs e)
        {

        }
      
    }
}