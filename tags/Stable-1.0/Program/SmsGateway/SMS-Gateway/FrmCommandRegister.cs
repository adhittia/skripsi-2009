using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Com.Martin.SMS.DB;

namespace SMS_Gateway.FormCommandRegister
{
    public partial class FrmCommandRegister : Form
    {
        public FrmCommandRegister()
        {
            InitializeComponent();
        }

        private void FrmCommandRegister_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void showData(String regType, String regName)
        {
            DBProvider dbprovider = new DBProvider();

            MySqlCommand command = new MySqlCommand();


            String sqlCmd = "select * from daftar_register where reg_type=?reg_type and reg_name=?reg_name";

            command.CommandText = sqlCmd;
            command.Parameters.Add(new MySqlParameter("reg_type", regType));
            command.Parameters.Add(new MySqlParameter("reg_name", regName));

            DataTable dtCommand = dbprovider.getData(command);

            if (dtCommand.Rows.Count > 0)
            {
                DataRow row = dtCommand.Rows[0];

                this.Txt_CmdType.Text = row["reg_type"].ToString();
                this.Txt_CmdName.Text = row["reg_name"].ToString();
                this.Txt_ClassName.Text = row["nama_class"].ToString();
                this.Txt_AssemblyName.Text = row["nama_assembly"].ToString();

            }

        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand();
            DBProvider dbProvider = new DBProvider();

            String sqlCmd = "select * from daftar_register where reg_type=?reg_type and reg_name=?reg_name";

            command.Parameters.Clear();
            command.Parameters.Add(new MySqlParameter("reg_type", Txt_CmdType.Text));
            command.Parameters.Add(new MySqlParameter("reg_name", Txt_CmdName.Text));
            command.Parameters.Add(new MySqlParameter("nama_class", Txt_ClassName.Text));
            command.Parameters.Add(new MySqlParameter("nama_assembly", Txt_AssemblyName.Text));

            command.CommandText = sqlCmd;
            DataTable dtCommand = dbProvider.getData(command);

            if (dtCommand.Rows.Count > 0)
            {
                command.CommandText = " update `daftar_register`  set ";
                command.CommandText += " `reg_type`=?reg_type, ";
                command.CommandText += " `reg_name`=?reg_name , ";
                command.CommandText += " `nama_class`=?nama_class , ";
                command.CommandText += " `nama_assembly`=?nama_assembly ";
                command.CommandText += " where `reg_type`=?reg_type and `reg_name`=?reg_name";
            }
            else
            {
                command.CommandText = "INSERT INTO `daftar_register` ";
                command.CommandText += "( `reg_type` , `reg_name` , `nama_class` , `nama_assembly` ) ";
                command.CommandText += "VALUES ( ?reg_type , ?reg_name , ?nama_class , ?nama_assembly )";
            }

            if (dbProvider.Exec(command))
            {
                MessageBox.Show("Data Saved", "Save");
            }
            else
            {
                MessageBox.Show("Cannot Saving Data", "Save");
            }
            dbProvider.dbClose();
            this.Close();


        }

        public void deleteData(String regType, String regName)
        {
            DBProvider dbprovider = new DBProvider();

            MySqlCommand command = new MySqlCommand();

            String sqlCmd = "DELETE FROM `daftar_register` ";
            sqlCmd += "WHERE CONVERT(`daftar_register`.`reg_type` USING utf8) = ?reg_type AND ";
            sqlCmd += "CONVERT(`daftar_register`.`reg_name` USING utf8) = ?reg_name ";

            command.CommandText = sqlCmd;
            command.Parameters.Clear();
            command.Parameters.Add(new MySqlParameter("reg_type", regType));
            command.Parameters.Add(new MySqlParameter("reg_name", regName));

            dbprovider.Exec(command);
        }
    }
}