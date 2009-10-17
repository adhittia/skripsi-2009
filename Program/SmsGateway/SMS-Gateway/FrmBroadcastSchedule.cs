using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMS_Gateway.FormBroadcastSchedule 
{
    public partial class FrmBroadcastSchedule : Form
    {
        public FrmBroadcastSchedule()
        {
            InitializeComponent();
        }

        private void FrmBroadcastSchedule_Load(object sender, EventArgs e)
        {
            this.Cmb_Status.SelectedIndex = 0; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}