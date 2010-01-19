using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Com.Martin.SMS.DB;

namespace SMS_Gateway
{
    public partial class FrmAddSchedule : Form
    {
        private readonly AppData.MenuSchedule ms;

        public FrmAddSchedule()
        {
            InitializeComponent();
            ms = new SMS_Gateway.AppData.MenuSchedule();
        }

        public FrmAddSchedule(AppData.MenuSchedule msx) : this()
        {
            ms = msx;
            
            dateTimePicker1.Value = Convert.ToDateTime(ms.MsDate);
            txtMenuA.Text = AppData.Menu.Find(ms.MsMenuA).MName;
            txtMenuB.Text = AppData.Menu.Find(ms.MsMenuB).MName;
            txtMenuC.Text = AppData.Menu.Find(ms.MsMenuC).MName;
            txtScheduleID.Text = ms.MenuScheduleId.ToString();
            
            
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmLookupMenu frmLookMenu = new FrmLookupMenu();
          
            if (frmLookMenu.ShowDialog() == DialogResult.OK)
            {
                if (frmLookMenu.ReturnValue != null)
                {
                    AppData.Menu mn = frmLookMenu.ReturnValue;
                    txtMenuA.Text = mn.MName;
                    ms.MsMenuA = mn.MenuId;
                }
                
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ms.MsDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");

            ms.Save();
            MySqlCommand command = new MySqlCommand();
            DBProvider dbProvider = new DBProvider();
            command.CommandText  = "insert into customer_order(menu_schedule_id, com_order_date, com_selected, com_order_status, customer_id, id_input)";
            command.CommandText += "select a.Menu_Schedule_Id, a.MS_DATE, a.MS_MENU_A, 'ACTIVE', b.Customer_ID, 0  from menu_schedule a join customer_profile b where a.menu_schedule_id not in (select c.menu_schedule_id from customer_order c)";

            dbProvider.Exec(command);
            dbProvider.dbClose();

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            FrmLookupMenu frmLookMenu = new FrmLookupMenu();

            if (frmLookMenu.ShowDialog() == DialogResult.OK)
            {
                AppData.Menu mn = frmLookMenu.ReturnValue;
                txtMenuB.Text = mn.MName;
                ms.MsMenuB = mn.MenuId;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            FrmLookupMenu frmLookMenu = new FrmLookupMenu();

            if (frmLookMenu.ShowDialog() == DialogResult.OK)
            {
                AppData.Menu mn = frmLookMenu.ReturnValue;
                txtMenuC.Text = mn.MName;
                ms.MsMenuC = mn.MenuId;

            }
        }
    }
}