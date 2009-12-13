using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMS_Gateway
{
    public partial class FrmLookupMenu : Form
    {
        private AppData.Menu returnValue;

        public AppData.Menu ReturnValue
        {
            get { return this.returnValue; }
            set { this.returnValue = value; }
        }
        public FrmLookupMenu()
        {
            InitializeComponent();
        }

        private void FrmLookupMenu_Activated(object sender, EventArgs e)
        {
            showMenu();
            
        }

        private void showMenu()
        {

            lvMenu.Items.Clear();

            foreach (AppData.Menu mn in AppData.Menu.FindAll())
            {
                ListViewItem item = lvMenu.Items.Add(mn.MenuId.ToString());
                item.Tag = mn;
                item.SubItems.Add(mn.MName);
            }
        }
        private void showMenu(String text)
        {

            lvMenu.Items.Clear();

            foreach (AppData.Menu mn in AppData.Menu.FindAll())
            {
                ListViewItem item = lvMenu.Items.Add(mn.MenuId.ToString());
                item.Tag = mn;
                item.SubItems.Add(mn.MName);
            }
        }

        private void txtSearchMenu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                showMenu(txtSearchMenu.Text);
            }
        }

        private void lvMenu_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvMenu.SelectedItems.Count == 1)
            {
                AppData.Menu mn = (AppData.Menu)lvMenu.SelectedItems[0].Tag;
                this.returnValue = mn;
                DialogResult = DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}