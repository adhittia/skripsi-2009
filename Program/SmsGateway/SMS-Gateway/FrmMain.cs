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

using Com.Martin.SMS.DB;

using MySql.Data.MySqlClient;

using SMS_Gateway.FormDiagnostic;
using SMS_Gateway.FormBroadcastSchedule;
using SMS_Gateway.FormCommandRegister;
using Castle.ActiveRecord;

namespace SMS_Gateway
{
    public partial class FrmMain : Form
    {

        private DBProvider dbprovider = new DBProvider();
      
        private GSMModem oGsmModem = new GSMModem();

        private String dialogCaption = "SMS Gateway";
     

        public FrmMain()
        {
            InitializeComponent();
            this.oGsmModem.NewMessageReceived += new GSMModem.NewMessageReceivedEventHandler(this.oGsmModem_NewMessageReceived);
        }


        private void oGsmModem_NewMessageReceived(ATSMS.NewMessageReceivedEventArgs e)
        {
            MessageBox.Show("Message from " + e.MSISDN + ". Message - " + e.TextMessage, dialogCaption, MessageBoxButtons.OK);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (oGsmModem.IsConnected) {
                MessageBox.Show("Already Connected, please close connection", dialogCaption, MessageBoxButtons.OK);
                return;
            }

            if (cboComPort.Text == string.Empty)
            {
                MessageBox.Show("COM Port must be selected", dialogCaption, MessageBoxButtons.OK);
                return;
            }
            oGsmModem.Port = cboComPort.Text;

            if (cboBaudRate.Text != string.Empty)
            {
                oGsmModem.BaudRate = Convert.ToInt32(cboBaudRate.Text);
            }

            if (cboDataBit.Text != string.Empty)
            {
                oGsmModem.DataBits = Convert.ToInt32(cboDataBit.Text);
            }

            if (cboStopBit.Text != string.Empty)
            {
                switch (cboStopBit.Text)
                {
                    case "1":
                        oGsmModem.StopBits = ATSMS.Common.EnumStopBits.One;
                        break;
                    case "1.5":
                        oGsmModem.StopBits = ATSMS.Common.EnumStopBits.OnePointFive;
                        break;
                    case "2":
                        oGsmModem.StopBits = ATSMS.Common.EnumStopBits.Two;
                        break;
                }
            }

            if (cboFlowControl.Text != string.Empty)
            {
                switch (cboFlowControl.Text)
                {
                    case "None":
                        oGsmModem.FlowControl = ATSMS.Common.EnumFlowControl.None;
                        break;
                    case "Hardware":
                        oGsmModem.FlowControl = ATSMS.Common.EnumFlowControl.RTS_CTS;
                        break;
                    case "Xon/Xoff":
                        oGsmModem.FlowControl = ATSMS.Common.EnumFlowControl.Xon_Xoff;
                        break;
                }
            }
            
            try
            {
                oGsmModem.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, dialogCaption, MessageBoxButtons.OK);
                return;
            }
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            btnDiagnostic.Enabled = true;
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (oGsmModem.IsConnected)
            {
                oGsmModem.Disconnect();
            }
            btnConnect.Enabled = true; 
            btnDisconnect.Enabled = false;
            btnDiagnostic.Enabled = false;
        }

        private void btnDiagnostic_Click(object sender, EventArgs e)
        {
            if (oGsmModem.IsConnected)
            {
                FrmDiagnostic frmDiag = new FrmDiagnostic();
                this.Cursor = Cursors.WaitCursor;
                frmDiag.getModemDetails(oGsmModem); 
                frmDiag.ShowDialog();
                this.Cursor = Cursors.Default;
            }
            else 
            {
                MessageBox.Show("Modem is not connected", dialogCaption, MessageBoxButtons.OK);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {   
            if (oGsmModem.IsConnected) 
            { 
                oGsmModem.Disconnect(); 
            }
            this.Close();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnDiagnostic.Enabled = false;
            
            cmbInboxTimeInterval.SelectedIndex = 0;
            cmbOutboxTimeInterval.SelectedIndex = 0;
            cmbInboxFilter.SelectedIndex = 0;
            cmbOutBoxFilter.SelectedIndex = 0;

            ConnectToDB();
            showCommandRegister();
            showBroadcastSchedule();
            showMenu();
            showCustomer();
            showSchedule();
        }

        private void chkTab() 
        {
            if (oGsmModem.IsConnected)
            { 
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Receiver = "085668495684";
            String Msg = "Test doank";
            try
            {
                oGsmModem.SendSMS(Receiver, Msg);
            }
            catch (Exception ex) 
            {
                MessageBox.Show( ex.Message , dialogCaption , MessageBoxButtons.OK);           
            }

        }

        private void btnInboxClearLog_Click(object sender, EventArgs e)
        {
            txtInboxLog.Text = String.Empty;
        }

        private void chkInboxTimeInterval_CheckedChanged(object sender, EventArgs e)
        {
            LogTimer(this.chkInboxTimeInterval, this.InboxTimer, this.cmbInboxTimeInterval);
        }

        private void LogTimer(CheckBox oCheckBox,Timer oTimer, ComboBox oCombo) 
        {
            if (oCheckBox.Checked)
            {
                oTimer.Stop();
                oTimer.Interval = getInterval(oCombo.SelectedIndex);
                oTimer.Start();
            }
            else
            {
                 oTimer.Stop();
            }
        }

        private int getInterval(int oIndex) 
        {
            int iRet = 1000;
            switch (oIndex) 
            {
                case 0:
                    iRet = iRet*60; //1 minutes
                    break;

                case 1:
                    iRet = iRet * 60 *5; //5 minutes
                    break;
                case 2:
                    iRet = iRet * 60 * 15; //15 minutes
                    break;
                case 3:
                    iRet = iRet * 60 * 30; //15 minutes
                    break;
                case 4:
                    iRet = iRet * 60 * 60; //60 minutes
                    break;
            }
            return iRet;
        }

        private void InboxTimer_Tick(object sender, EventArgs e)
        {
            this.txtInboxLog.Text += DateTime.Now + " ";
        }

        private void cmbInboxTimeInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogTimer(this.chkInboxTimeInterval, this.InboxTimer, this.cmbInboxTimeInterval);
        }

        private void chkOutboxTimeInterval_CheckedChanged(object sender, EventArgs e)
        {
            LogTimer(this.chkOutboxTimeInterval, this.OutboxTimer, this.cmbOutboxTimeInterval);
        }

        private void cmbOutboxTimeInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogTimer(this.chkOutboxTimeInterval, this.OutboxTimer, this.cmbOutboxTimeInterval);
        }

        private void OutboxTimer_Tick(object sender, EventArgs e)
        {
            this.txtOutBoxLog.Text  += DateTime.Now + " ";
        }

        private void btnOutboxClearLog_Click(object sender, EventArgs e)
        {
            this.txtOutBoxLog.Text = String.Empty;
        }

        private void FrmMainClose(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (oGsmModem.IsConnected)
                {
                    MessageBox.Show("disconnecting");
                    oGsmModem.Disconnect();
                }
            }
            catch (Exception ex) 
            { 
            
            }
        }

        private void ConnectToDB() 
        {
            
            if (!dbprovider.dbConnect())
            {
                MessageBox.Show("Cannot Connect to Database!!");
                this.Close();
                return;
            }
        }

        private void Btn_Add_Broadcast_Click(object sender, EventArgs e)
        {
            FrmBroadcastSchedule frmBroadcast = new FrmBroadcastSchedule();
            frmBroadcast.ShowDialog(this);
        }

        private void Btn_Add_Cmd_Click(object sender, EventArgs e)
        {
            FrmCommandRegister frmCmdReg = new FrmCommandRegister();
            frmCmdReg.ShowDialog();
        }

        private void cmbOutBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            String sqlCmd = String.Empty;
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0 :
                    sqlCmd = "select * from sms_output";
                    break;
                case 1:
                    sqlCmd = "select * from sms_output where status='OK'";
                    break;

                case 2:
                    sqlCmd = "select * from sms_output where status='NOK'";
                    break;
                default:
                    sqlCmd = "select * from sms_output";
                    break;
            }
                        
            DataTable dtOutbox = dbprovider.getData(sqlCmd);
            this.gridOutbox.DataSource = dtOutbox;
            this.gridOutbox.AllowUserToAddRows = false;
            this.gridOutbox.AllowUserToDeleteRows = false;
            this.gridOutbox.AllowUserToResizeColumns = true;
            this.gridOutbox.AllowUserToResizeRows = false;
            this.gridOutbox.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void cmbInboxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            String sqlCmd = String.Empty;
            switch (((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    sqlCmd = "select * from sms_input";
                    break;
                case 1:
                    sqlCmd = "select * from sms_input where status='OK'";
                    break;

                case 2:
                    sqlCmd = "select * from sms_input where status='NOK'";
                    break;
                default:
                    sqlCmd = "select * from sms_input";
                    break;
            }
               
            DataTable dtInbox = dbprovider.getData(sqlCmd);
            this.gridInbox.DataSource = dtInbox;
            this.gridInbox.AllowUserToAddRows = false;
            this.gridInbox.AllowUserToDeleteRows = false;
            this.gridInbox.AllowUserToResizeColumns = true;
            this.gridInbox.AllowUserToResizeRows = false;
            this.gridInbox.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void showCommandRegister() 
        { 
            String sqlCmd = String.Empty;

            sqlCmd = "select * from daftar_register";

            DataTable dtCommand = dbprovider.getData(sqlCmd);
                
            this.gridComands.DataSource = dtCommand;
            
            DataGridViewLinkColumn editLink = new DataGridViewLinkColumn();
            editLink.Text = "edit";
            editLink.UseColumnTextForLinkValue = true;
            editLink.ToolTipText = "Edit Data";
            editLink.Width = 40;
            editLink.LinkColor = Color.Blue;
            

            DataGridViewLinkColumn deleteLink = new DataGridViewLinkColumn();
            deleteLink.Text = "delete";
            deleteLink.UseColumnTextForLinkValue = true;
            deleteLink.ToolTipText = "Delete Data";
            deleteLink.Width = 40;
            deleteLink.LinkColor = Color.Red;

            this.gridComands.Columns.Add(editLink);
            this.gridComands.Columns.Add(deleteLink);


            this.gridComands.AllowUserToAddRows = false;
            this.gridComands.AllowUserToDeleteRows = false;
            this.gridComands.AllowUserToResizeColumns = true;
            this.gridComands.AllowUserToResizeRows = false;
            this.gridComands.EditMode = DataGridViewEditMode.EditProgrammatically;
        
        }
        private void showSchedule()
        {

            lvSchedule.Items.Clear();

            foreach (AppData.MenuSchedule ms in AppData.MenuSchedule.FindAll())
            {
                ListViewItem item = lvSchedule.Items.Add(ms.MenuScheduleId.ToString());
                item.Tag = ms;
                item.SubItems.Add(ms.MsDate);
                item.SubItems.Add(AppData.Menu.Find(ms.MsMenuA).MName);
                item.SubItems.Add(AppData.Menu.Find(ms.MsMenuB).MName);
                item.SubItems.Add(AppData.Menu.Find(ms.MsMenuC).MName);
                

            }
        }
        private void showCustomer()
        {

            lvCustomer.Items.Clear();

            foreach (AppData.CustomerProfile cp in AppData.CustomerProfile.FindAll())
            {
                ListViewItem item = lvCustomer.Items.Add(cp.CustomerId.ToString());
                item.Tag = cp;
                item.SubItems.Add(cp.CpName);
                item.SubItems.Add(cp.CpDeliveryAddress);
                item.SubItems.Add(cp.CpBillAddress);
                item.SubItems.Add(cp.CpMobileNumber);
                item.SubItems.Add(cp.CpEmail);

            }
        }
        private void showMenu()
        {

            lvMenu.Items.Clear();
            
            foreach(AppData.Menu mn in AppData.Menu.FindAll())
            {
                ListViewItem item = lvMenu.Items.Add(mn.MenuId.ToString());
                item.Tag = mn;
                item.SubItems.Add(mn.MName);
                item.SubItems.Add(mn.MDescription);
                item.SubItems.Add(mn.MType);
                item.SubItems.Add(mn.MCategory);
                item.SubItems.Add(String.Format("{0:#.##0}",mn.MPrice.ToString()));
                
            }
        }
        private void showBroadcastSchedule()
        {
            String sqlCmd = String.Empty;

            sqlCmd = "select * from jadwal_broadcast";

            DataTable dtCommand = dbprovider.getData(sqlCmd);

            this.gridBroadcastSchedule.DataSource = dtCommand;
            
            this.gridBroadcastSchedule.AllowUserToAddRows = false;
            this.gridBroadcastSchedule.AllowUserToDeleteRows = false;
            this.gridBroadcastSchedule.AllowUserToResizeColumns = true;
            this.gridBroadcastSchedule.AllowUserToResizeRows = false;
            this.gridBroadcastSchedule.EditMode = DataGridViewEditMode.EditProgrammatically;


        }

        private void gridComands_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= this.gridComands.Rows.Count || e.ColumnIndex < 0 || e.ColumnIndex >= this.gridComands.Columns.Count)
                return;

            DataGridViewLinkCell linkCell = this.gridComands.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell;

            if (linkCell != null) 
            {
                String s = this.gridComands.Rows[e.RowIndex].Cells[0].Value.ToString();
                String s2 = this.gridComands.Rows[e.RowIndex].Cells[1].Value.ToString(); 

                MessageBox.Show("ada : " + s + "-" + s2); 
            
            }

        }

        private void btnNewMenu_Click(object sender, EventArgs e)
        {
            FrmAddMenu frmAdd = new FrmAddMenu();
            
            if (frmAdd.ShowDialog() == DialogResult.OK)
            {
                showMenu();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            FrmAddCustomer frmAddCust = new FrmAddCustomer();
            if (frmAddCust.ShowDialog() == DialogResult.OK)
            {
                showCustomer();
            }
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            FrmAddSchedule frmAddSche = new FrmAddSchedule();
            if (frmAddSche.ShowDialog() == DialogResult.OK)
            {
                showSchedule();
            }
        }

        private void btnRemoveMenu_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvMenu.CheckedItems.Count; i++)
            {
                AppData.Menu mn = (AppData.Menu)lvMenu.CheckedItems[i].Tag;
                mn.Delete();
            }
            showMenu();
        }

       

        private void lvMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvMenu.SelectedItems.Count == 1)
            {
                AppData.Menu mn = (AppData.Menu)lvMenu.SelectedItems[0].Tag;

                using (FrmAddMenu frmAddMenu = new FrmAddMenu(mn))
                {
                    if (frmAddMenu.ShowDialog(this) == DialogResult.OK)
                    {
                        showMenu();
                    }
                }
            }
        }

        private void lvCustomer_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (lvCustomer.SelectedItems.Count == 1)
            {
                AppData.CustomerProfile cp = (AppData.CustomerProfile)lvCustomer.SelectedItems[0].Tag;

                using (FrmAddCustomer frmAddCust = new FrmAddCustomer(cp))
                {
                    if (frmAddCust.ShowDialog(this) == DialogResult.OK)
                    {
                        showCustomer();
                    }
                }
            }
        }

        private void btnRemoveCustomer_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvCustomer.CheckedItems.Count; i++)
            {
                AppData.CustomerProfile cp = (AppData.CustomerProfile)lvCustomer.CheckedItems[i].Tag;
                cp.Delete();
            }
            showCustomer();
        }

        private void btnScheduleRemove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvSchedule.CheckedItems.Count; i++)
            {
                AppData.MenuSchedule ms = (AppData.MenuSchedule)lvSchedule.CheckedItems[i].Tag;
                ms.Delete();
            }
            showSchedule();
        }

        private void lvSchedule_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvSchedule.SelectedItems.Count == 1)
            {
                AppData.MenuSchedule ms = (AppData.MenuSchedule)lvSchedule.SelectedItems[0].Tag;

                using (FrmAddSchedule frmAddSche = new FrmAddSchedule(ms))
                {
                    if (frmAddSche.ShowDialog(this) == DialogResult.OK)
                    {
                        showSchedule();
                    }
                }
            }
        }


        
    }
}