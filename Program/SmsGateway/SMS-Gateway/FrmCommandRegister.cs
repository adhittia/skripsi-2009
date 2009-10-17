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

        public void showData(String regType , String regName) 
        {
            MySqlCommand command = new MySqlCommand();

            sqlCmd = "select * from daftar_register where reg_type=?reg_type and reg_name=?reg_name";

            command.commandText = "select * from daftar_register where reg_type=?reg_type and reg_name=?reg_name";
            command.params.add(new MySqlParameter("reg_type",regType));
            command.params.add(new MySqlParameter("reg_name",regName));


            DataTable dtCommand = dbprovider.getData(command);


            
        
        
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand();
            command.Parameters.Clear();

            command.CommandText = "INSERT INTO `daftar_register` " ;
            command.CommandText += "( `reg_type` , `reg_name` , `nama_class` , `nama_assembly` ) ";
            command.CommandText += "VALUES ( ?reg_type , ?reg_name , ?nama_class , ?nama_assembly )";

            command.Parameters.Add( new MySqlParameter("reg_type", Txt_CmdType.Text));
            command.Parameters.Add( new MySqlParameter("reg_name", Txt_CmdName.Text));
            command.Parameters.Add( new MySqlParameter("nama_class", Txt_ClassName.Text));
            command.Parameters.Add( new MySqlParameter("nama_assembly", Txt_AssemblyName.Text));

            DBProvider dbProvider = new DBProvider();
            if (dbProvider.dbConnect())
            {
                if (dbProvider.Exec(command))
                {
                    MessageBox.Show("Data Saved", "Save");
                }
                else 
                {
                    MessageBox.Show("Cannot Saving Data", "Save");
                }
                dbProvider.dbClose();
            }
            else
            {
                MessageBox.Show("Cannot Connect to database", "Save");
            }
        }

    }
}