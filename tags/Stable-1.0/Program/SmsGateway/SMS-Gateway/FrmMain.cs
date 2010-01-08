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

using Com.Martin.SMS.Command;
using Com.Martin.SMS.Common;
using Com.Martin.SMS.Data;
using Com.Martin.SMS.DB;

using MySql.Data.MySqlClient;

using SMS_Gateway.FormDiagnostic;
using SMS_Gateway.FormBroadcastSchedule;
using SMS_Gateway.FormCommandRegister;
using Castle.ActiveRecord;

namespace SMS_Gateway {
    public partial class FrmMain : Form {

        private DBProvider dbprovider = new DBProvider();

        private GSMModem oGsmModem = new GSMModem();

        private String dialogCaption = "SMS Gateway";

        delegate void SetTextCallback(string text);

        delegate void RefreshPage();

        public FrmMain() {
            InitializeComponent();
            this.oGsmModem.AutoDeleteReadMessage = true;
            this.oGsmModem.AutoDeleteSentMessage = true;
            CheckForIllegalCrossThreadCalls = true;

            this.oGsmModem.NewMessageReceived += new GSMModem.NewMessageReceivedEventHandler(oGsmModem_NewMessageReceived);
        }

        private void InputWorker_DoWork(object sender, DoWorkEventArgs e) {
            e.Result = e.Argument.ToString();
        }

        private void InputWorker_Complete(object sender, RunWorkerCompletedEventArgs e) {
            try {
                this.txtInboxLog.Text += e.Result.ToString();
                cmbInboxFilter_SelectedIndexChanged(cmbInboxFilter, new EventArgs());
                cmbOutBoxFilter_SelectedIndexChanged(cmbOutBoxFilter, new EventArgs());
            }catch(InvalidOperationException ex){
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void SetInboxText(string text) {
            this.txtInboxLog.Text += text;
        }

        public void RefreshInbox() {
            cmbInboxFilter_SelectedIndexChanged(cmbInboxFilter, new EventArgs());
            cmbOutBoxFilter_SelectedIndexChanged(cmbOutBoxFilter, new EventArgs());
        }

        private void oGsmModem_NewMessageReceived(ATSMS.NewMessageReceivedEventArgs e) {
            //SMSIncoming smsInput = SMSHelper.SaveIncomingMessage("02191848465", "02191848465", "Test;Request");
            SMSIncoming smsInput = SMSHelper.SaveIncomingMessage(e.MSISDN, "02191848465", e.TextMessage);
            SMSOutgoing smsOut = CommandProcessor.ProcessRequest(smsInput);

            Com.Martin.Function.InputLog log = new Com.Martin.Function.InputLog();
            string text = log.composeReportDetail(smsOut.SMSRequest, smsOut);

            if (this.txtInboxLog.InvokeRequired) {
                FrmMain main = (this);
                SetTextCallback d = new SetTextCallback(main.SetInboxText);
                RefreshPage p = new RefreshPage(main.RefreshInbox);
                this.txtInboxLog.Invoke(d, new object[] { text });
                this.Invoke(p);
            } else {
                this.txtInboxLog.Text = text;
            }
            //System.ComponentModel.BackgroundWorker InputWorker;
            //InputWorker = new System.ComponentModel.BackgroundWorker();
            //InputWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.InputWorker_DoWork);
            //InputWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.InputWorker_Complete);
            //InputWorker.RunWorkerAsync(text);
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            if (oGsmModem.IsConnected) {
                MessageBox.Show("Already Connected, please close connection", dialogCaption, MessageBoxButtons.OK);
                return;
            }

            if (cboComPort.Text == string.Empty) {
                MessageBox.Show("COM Port must be selected", dialogCaption, MessageBoxButtons.OK);
                return;
            }
            oGsmModem.Port = cboComPort.Text;

            if (cboBaudRate.Text != string.Empty) {
                oGsmModem.BaudRate = Convert.ToInt32(cboBaudRate.Text);
            }

            if (cboDataBit.Text != string.Empty) {
                oGsmModem.DataBits = Convert.ToInt32(cboDataBit.Text);
            }

            if (cboStopBit.Text != string.Empty) {
                switch (cboStopBit.Text) {
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

            if (cboFlowControl.Text != string.Empty) {
                switch (cboFlowControl.Text) {
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

            try {
                oGsmModem.Connect();
                BroadcastTimer.Enabled = true;
                SendingTimer.Enabled = true;
                this.oGsmModem.SendDelay = 10;
                this.oGsmModem.NewMessageIndication = true;
                labelStatus.Text = "Modem is connected. Model: " + oGsmModem.PhoneModel;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, dialogCaption, MessageBoxButtons.OK);
                return;
            }
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = true;
            btnDiagnostic.Enabled = true;
        }

        private void btnDisconnect_Click(object sender, EventArgs e) {
            if (oGsmModem.IsConnected) {
                oGsmModem.Disconnect();
                BroadcastTimer.Enabled = false;
                SendingTimer.Enabled = false;
                labelStatus.Text = "Modem is disconnected.";

            }
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnDiagnostic.Enabled = false;
        }

        private void btnDiagnostic_Click(object sender, EventArgs e) {
            if (oGsmModem.IsConnected) {
                FrmDiagnostic frmDiag = new FrmDiagnostic();
                this.Cursor = Cursors.WaitCursor;
                frmDiag.getModemDetails(oGsmModem);
                frmDiag.ShowDialog();
                this.Cursor = Cursors.Default;
            } else {
                MessageBox.Show("Modem is not connected", dialogCaption, MessageBoxButtons.OK);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e) {
            if (oGsmModem.IsConnected) {
                oGsmModem.Disconnect();
            }
            this.Close();
        }

        private void FrmMain_Load(object sender, EventArgs e) {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnDiagnostic.Enabled = false;

            this.cboBaudRate.SelectedIndex = 7;
            this.cboComPort.SelectedIndex = 3;
            this.cboDataBit.SelectedIndex = 3;
            this.cboFlowControl.SelectedIndex = 0;
            this.cboStopBit.SelectedIndex = 0;

            cmbInboxTimeInterval.SelectedIndex = 0;
            cmbOutboxTimeInterval.SelectedIndex = 0;
            cmbInboxFilter.SelectedIndex = 0;
            cmbOutBoxFilter.SelectedIndex = 0;

            cmbReportFilter.Items.Add("Show All");
            cmbReportFilter.Items.Add("Broadcast");
            cmbReportFilter.Items.Add("Request");
            cmbReportFilter.SelectedIndex = 0;

            ConnectToDB();
            showCommandRegister(false);
            showBroadcastSchedule(false);
            showMenu();
            showCustomer();
            showSchedule();
        }

        private void chkTab() {
            if (oGsmModem.IsConnected) {

            }
        }

        private void btnInboxClearLog_Click(object sender, EventArgs e) {
            txtInboxLog.Clear();
        }

        private void chkInboxTimeInterval_CheckedChanged(object sender, EventArgs e) {
            LogTimer(this.chkInboxTimeInterval, this.InboxTimer, this.cmbInboxTimeInterval);
        }

        private void LogTimer(CheckBox oCheckBox, Timer oTimer, ComboBox oCombo) {
            if (oCheckBox.Checked) {
                oTimer.Stop();
                oTimer.Interval = getInterval(oCombo.SelectedIndex);
                oTimer.Start();
            } else {
                oTimer.Stop();
            }
        }

        private int getInterval(int oIndex) {
            int iRet = 1000;
            switch (oIndex) {
                case 0:
                    iRet = iRet * 60; //1 minutes
                    break;

                case 1:
                    iRet = iRet * 60 * 5; //5 minutes
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

        private void InboxTimer_Tick(object sender, EventArgs e) {
            this.txtInboxLog.Clear();
        }

        private void cmbInboxTimeInterval_SelectedIndexChanged(object sender, EventArgs e) {
            LogTimer(this.chkInboxTimeInterval, this.InboxTimer, this.cmbInboxTimeInterval);
        }

        private void chkOutboxTimeInterval_CheckedChanged(object sender, EventArgs e) {
            LogTimer(this.chkOutboxTimeInterval, this.OutboxTimer, this.cmbOutboxTimeInterval);
        }

        private void cmbOutboxTimeInterval_SelectedIndexChanged(object sender, EventArgs e) {
            LogTimer(this.chkOutboxTimeInterval, this.OutboxTimer, this.cmbOutboxTimeInterval);
        }

        private void OutboxTimer_Tick(object sender, EventArgs e) {
            this.txtOutBoxLog.Text += DateTime.Now + " ";
        }

        private void btnOutboxClearLog_Click(object sender, EventArgs e) {
            this.txtOutBoxLog.Text = String.Empty;
        }

        private void FrmMainClose(object sender, FormClosingEventArgs e) {
            try {
                if (oGsmModem.IsConnected) {
                    MessageBox.Show("Disconnecting modem.");
                    oGsmModem.Disconnect();
                }
            } catch (Exception ex) {

            }
        }

        private void ConnectToDB() {

            if (!dbprovider.dbConnect()) {
                MessageBox.Show("Cannot Connect to Database!!");
                this.Close();
                return;
            }
        }

        private void Btn_Add_Broadcast_Click(object sender, EventArgs e) {
            FrmBroadcastSchedule frmBroadcast = new FrmBroadcastSchedule();
            DialogResult hasil = frmBroadcast.ShowDialog(this);
            if (hasil == DialogResult.OK) {
                showBroadcastSchedule(true);
            }
        }

        private void Btn_Add_Cmd_Click(object sender, EventArgs e) {
            FrmCommandRegister frmCmdReg = new FrmCommandRegister();
            DialogResult hasil = frmCmdReg.ShowDialog(this);
            if (hasil == DialogResult.OK) {
                showCommandRegister(true);
            }
        }

        private void cmbOutBoxFilter_SelectedIndexChanged(object sender, EventArgs e) {
            String sqlCmd = String.Empty;
            switch (((ComboBox)sender).SelectedIndex) {
                case 0:
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

        private void cmbInboxFilter_SelectedIndexChanged(object sender, EventArgs e) {
            String sqlCmd = String.Empty;
            switch (((ComboBox)sender).SelectedIndex) {
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

        private void showCommandRegister(bool justRefresh) {
            String sqlCmd = String.Empty;

            sqlCmd = "select * from daftar_register";

            DataTable dtCommand = dbprovider.getData(sqlCmd);

            this.gridComands.DataSource = dtCommand;

            if (!justRefresh) {
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

            }
            this.gridComands.AllowUserToAddRows = false;
            this.gridComands.AllowUserToDeleteRows = false;
            this.gridComands.AllowUserToResizeColumns = true;
            this.gridComands.AllowUserToResizeRows = false;
            this.gridComands.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void showSchedule() {
            lvSchedule.Items.Clear();

            foreach (AppData.MenuSchedule ms in AppData.MenuSchedule.FindAll()) {
                ListViewItem item = lvSchedule.Items.Add(ms.MenuScheduleId.ToString());
                item.Tag = ms;
                item.SubItems.Add(ms.MsDate);
                item.SubItems.Add(AppData.Menu.Find(ms.MsMenuA).MName);
                item.SubItems.Add(AppData.Menu.Find(ms.MsMenuB).MName);
                item.SubItems.Add(AppData.Menu.Find(ms.MsMenuC).MName);
            }
        }
        private void showCustomer() {
            lvCustomer.Items.Clear();

            foreach (AppData.CustomerProfile cp in AppData.CustomerProfile.FindAll()) {
                ListViewItem item = lvCustomer.Items.Add(cp.CustomerId.ToString());
                item.Tag = cp;
                item.SubItems.Add(cp.CpName);
                item.SubItems.Add(cp.CpDeliveryAddress);
                item.SubItems.Add(cp.CpBillAddress);
                item.SubItems.Add(cp.CpMobileNumber);
                item.SubItems.Add(cp.CpEmail);
            }
        }
        private void showMenu() {

            lvMenu.Items.Clear();

            foreach (AppData.Menu mn in AppData.Menu.FindAll()) {
                ListViewItem item = lvMenu.Items.Add(mn.MenuId.ToString());
                item.Tag = mn;
                item.SubItems.Add(mn.MName);
                item.SubItems.Add(mn.MDescription);
                item.SubItems.Add(mn.MType);
                item.SubItems.Add(mn.MCategory);
                item.SubItems.Add(String.Format("{0:#.##0}", mn.MPrice.ToString()));

            }
        }
        private void showBroadcastSchedule(bool justRefresh) {
            String sqlCmd = String.Empty;

            sqlCmd = "select * from jadwal_broadcast";

            DataTable dtCommand = dbprovider.getData(sqlCmd);

            this.gridBroadcastSchedule.DataSource = dtCommand;

            if (!justRefresh) {
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

                this.gridBroadcastSchedule.Columns.Add(editLink);
                this.gridBroadcastSchedule.Columns.Add(deleteLink);
            }

            this.gridBroadcastSchedule.AllowUserToAddRows = false;
            this.gridBroadcastSchedule.AllowUserToDeleteRows = false;
            this.gridBroadcastSchedule.AllowUserToResizeColumns = true;
            this.gridBroadcastSchedule.AllowUserToResizeRows = false;
            this.gridBroadcastSchedule.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void gridComands_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.RowIndex >= this.gridComands.Rows.Count || e.ColumnIndex < 0 || e.ColumnIndex >= this.gridComands.Columns.Count)
                return;

            DataGridViewLinkCell linkCell = this.gridComands.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell;

            if (linkCell != null) {
                String regType = this.gridComands.Rows[e.RowIndex].Cells[2].Value.ToString();
                String regName = this.gridComands.Rows[e.RowIndex].Cells[3].Value.ToString();

                if (linkCell.Value.Equals("edit")) {

                    FrmCommandRegister frmCmdReg = new FrmCommandRegister();
                    frmCmdReg.showData(regType, regName);
                    DialogResult hasil = frmCmdReg.ShowDialog(this);
                    if (hasil == DialogResult.OK) {
                        showCommandRegister(true);
                    }
                } else if (linkCell.Value.Equals("delete")) {
                    if (MessageBox.Show("Are you sure?", "Delete Data", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        FrmCommandRegister frmCmdReg = new FrmCommandRegister();

                        frmCmdReg.deleteData(regType, regName);
                        showCommandRegister(true);
                    }
                }
                //MessageBox.Show("ada : " + regType + "-" + regName); 
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            oGsmModem_NewMessageReceived(new NewMessageReceivedEventArgs());
        }
        private void btnNewMenu_Click(object sender, EventArgs e) {
            FrmAddMenu frmAdd = new FrmAddMenu();

            if (frmAdd.ShowDialog() == DialogResult.OK) {
                showMenu();
            }
        }

        private void BroadcastTimer_Tick(object sender, EventArgs e) {
            CommandProcessor.ProcessBroadcast();
            cmbOutBoxFilter_SelectedIndexChanged(this.cmbOutBoxFilter, new EventArgs());
        }

        private void SendingTimer_Tick(object sender, EventArgs e) {
            List<SMSOutgoing> lstOutGoing = SMSHelper.GetOutgoingSMSList();
            for (int i = 0; i < lstOutGoing.Count; i++) {
                SMSOutgoing outSms = lstOutGoing[i];
                try {
                    //sleep
                    oGsmModem.SendSMS(outSms.DestinationNo, outSms.MessageText);

                    outSms.DateSent = DateTime.Now;
                    SMSHelper.SaveOutgoingMessage(ref outSms);
                    Com.Martin.Function.InputLog log = new Com.Martin.Function.InputLog();
                    this.txtOutBoxLog.Text += log.composeOutBoxDetail(outSms);
                } catch (Exception ex) {
                    // tulis ke outbox tab

                    //MessageBox.Show(ex.Message, dialogCaption, MessageBoxButtons.OK);
                }
            }
            cmbOutBoxFilter_SelectedIndexChanged(this.cmbOutBoxFilter, new EventArgs());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        private void btnAddCustomer_Click(object sender, EventArgs e) {
            FrmAddCustomer frmAddCust = new FrmAddCustomer();
            if (frmAddCust.ShowDialog() == DialogResult.OK) {
                showCustomer();
            }
        }

        private void btnAddSchedule_Click(object sender, EventArgs e) {
            FrmAddSchedule frmAddSche = new FrmAddSchedule();
            if (frmAddSche.ShowDialog() == DialogResult.OK) {
                showSchedule();
            }
        }

        private void btnRemoveMenu_Click(object sender, EventArgs e) {
            for (int i = 0; i < lvMenu.CheckedItems.Count; i++) {
                AppData.Menu mn = (AppData.Menu)lvMenu.CheckedItems[i].Tag;
                mn.Delete();
            }
            showMenu();
        }

        private void lvMenu_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (lvMenu.SelectedItems.Count == 1) {
                AppData.Menu mn = (AppData.Menu)lvMenu.SelectedItems[0].Tag;

                using (FrmAddMenu frmAddMenu = new FrmAddMenu(mn)) {
                    if (frmAddMenu.ShowDialog(this) == DialogResult.OK) {
                        showMenu();
                    }
                }
            }
        }

        private void lvCustomer_MouseDoubleClick(object sender, MouseEventArgs e) {

            if (lvCustomer.SelectedItems.Count == 1) {
                AppData.CustomerProfile cp = (AppData.CustomerProfile)lvCustomer.SelectedItems[0].Tag;

                using (FrmAddCustomer frmAddCust = new FrmAddCustomer(cp)) {
                    if (frmAddCust.ShowDialog(this) == DialogResult.OK) {
                        showCustomer();
                    }
                }
            }
        }

        private void btnRemoveCustomer_Click(object sender, EventArgs e) {
            for (int i = 0; i < lvCustomer.CheckedItems.Count; i++) {
                AppData.CustomerProfile cp = (AppData.CustomerProfile)lvCustomer.CheckedItems[i].Tag;
                cp.Delete();
            }
            showCustomer();
        }

        private void btnScheduleRemove_Click(object sender, EventArgs e) {
            for (int i = 0; i < lvSchedule.CheckedItems.Count; i++) {
                AppData.MenuSchedule ms = (AppData.MenuSchedule)lvSchedule.CheckedItems[i].Tag;
                ms.Delete();
            }
            showSchedule();
        }

        private void lvSchedule_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (lvSchedule.SelectedItems.Count == 1) {
                AppData.MenuSchedule ms = (AppData.MenuSchedule)lvSchedule.SelectedItems[0].Tag;

                using (FrmAddSchedule frmAddSche = new FrmAddSchedule(ms)) {
                    if (frmAddSche.ShowDialog(this) == DialogResult.OK) {
                        showSchedule();
                    }
                }
            }
        }

        private void cmbReportFilter_SelectedIndexChanged(object sender, EventArgs e) {
            initGridReport();
        }

        private void dtReportFilter_ValueChanged(object sender, EventArgs e) {
            initGridReport();
        }

        private void initGridReport() {

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = " SELECT Id_Output, Waktu_Kirim AS 'Sent Date', No_Tujuan as 'Receiver No', Pesan_Teks as 'Text Message', Status FROM catering.sms_output  ";

            cmd.CommandText += " where date_format(Waktu_Diproses, '%Y-%m-%d')=?Waktu_Diproses ";

            switch (cmbReportFilter.SelectedIndex) {
                case 0:
                    break;
                case 1:
                    cmd.CommandText += " and id_input is null or id_input ='' ";
                    break;
                case 2:
                    cmd.CommandText += " and id_input is not null and id_input <> ''";
                    break;
            }


            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("Waktu_Diproses", dtReportFilter.Value.ToString("yyyy-MM-dd"));

            DataTable dtReport = dbprovider.getData(cmd);

            this.gridReport.DataSource = dtReport;

            this.gridReport.AllowUserToAddRows = false;
            this.gridReport.AllowUserToDeleteRows = false;
            this.gridReport.AllowUserToResizeColumns = true;
            this.gridReport.AllowUserToResizeRows = false;
            this.gridReport.EditMode = DataGridViewEditMode.EditProgrammatically;
            if (dtReport.Rows.Count > 0) {
                this.gridReport.Columns[0].Visible = false;
            }
            gridReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void gridBroadcastSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.RowIndex >= this.gridComands.Rows.Count || e.ColumnIndex < 0 || e.ColumnIndex >= this.gridComands.Columns.Count)
                return;

            DataGridViewLinkCell linkCell = this.gridBroadcastSchedule.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewLinkCell;

            if (linkCell != null) {
                string jadwalID = this.gridBroadcastSchedule.Rows[e.RowIndex].Cells[2].Value.ToString();

                if (linkCell.Value.Equals("edit")) {

                    FrmBroadcastSchedule frmBroadcast = new FrmBroadcastSchedule();
                    frmBroadcast.showData(int.Parse(jadwalID));

                    DialogResult hasil = frmBroadcast.ShowDialog(this);
                    if (hasil == DialogResult.OK) {
                        showBroadcastSchedule(true);
                    }
                } else if (linkCell.Value.Equals("delete")) {
                    if (MessageBox.Show("Are you sure?", "Delete Data", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        FrmBroadcastSchedule frmBroadcast = new FrmBroadcastSchedule();

                        frmBroadcast.showData(int.Parse(jadwalID));
                        frmBroadcast.deleteData(int.Parse(jadwalID));
                        showBroadcastSchedule(true);
                    }
                }
                //MessageBox.Show("ada : " + regType + "-" + regName); 
            }
        }

        private void gridReport_Click(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0 || e.RowIndex >= this.gridComands.Rows.Count)
                return;

            composeReportDetail(this.gridReport.Rows[e.RowIndex].Cells[0].Value.ToString());

        }

        private void composeReportDetail(String id) {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from sms_output where id_output=?id_output";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("id_output", id);
            DataTable dt = dbprovider.getData(cmd);
            String s = String.Empty;
            if (dt.Rows.Count > 0) {
                DataRow r = dt.Rows[0];
                if (r["ID_INPUT"].ToString() == String.Empty) {
                    s = "SMS TYPE : BROADCAST";
                    s += "\r\n";
                    for (int i = 0; i < 200; i++)
                        s += "-";

                    s += "\r\n";
                    s += "Res ID : " + r["ID_OUTPUT"].ToString() + " , Process At : " + r["Waktu_Diproses"].ToString() + " , Sent at : " + r["Waktu_Kirim"].ToString();
                    s += "\r\n";
                    s += "Text : " + r["Pesan_Teks"].ToString();
                    s += "\r\n";
                    for (int i = 0; i < 100; i++)
                        s += "=";
                } else {
                    s = "SMS TYPE : REQUEST-RESPONE";
                    s += "\r\n";
                    for (int i = 0; i < 200; i++)
                        s += "-";

                    cmd.CommandText = "select * from sms_input where ID_INPUT=?ID_INPUT";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("ID_INPUT", r["ID_INPUT"].ToString());

                    DataTable dtInput = dbprovider.getData(cmd);

                    String reqID = String.Empty;
                    String recieveDate = String.Empty;
                    String sender = String.Empty;
                    String text = String.Empty;

                    if (dtInput.Rows.Count > 0) {
                        DataRow rr = dtInput.Rows[0];
                        reqID = rr["Id_Input"].ToString();
                        recieveDate = rr["Tanggal_Terima"].ToString();
                        sender = rr["No_Pengirim"].ToString();
                        text = rr["Pesan_Teks"].ToString();

                    }

                    s += "\r\n";
                    s += "Req ID : " + reqID + " , Received : " + recieveDate + " , Sender : " + sender;
                    s += "\r\n";
                    s += "Command Type : " + r["Reg_Type"].ToString() + " , Command Name : " + r["Reg_Name"].ToString();
                    s += "\r\n";
                    s += "Text : " + text;

                    s += "\r\n";
                    s += "\r\n";


                    s += "Res ID : " + r["ID_OUTPUT"].ToString() + " , Process At : " + r["Waktu_Diproses"].ToString() + " , Sent at : " + r["Waktu_Kirim"].ToString();
                    s += "\r\n";
                    s += "Text : " + r["Pesan_Teks"].ToString();
                    s += "\r\n";
                    for (int i = 0; i < 100; i++)
                        s += "=";
                }
            }
            this.txtReportDetail.Text = s;

        }

        private void button3_Click(object sender, EventArgs e) {
            BroadcastTimer_Tick(BroadcastTimer, new EventArgs());
        }

        private void button4_Click(object sender, EventArgs e) {
            SendingTimer_Tick(SendingTimer, new EventArgs());
        }

    }
}