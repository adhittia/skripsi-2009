using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Com.Martin.SMS.DB {
    class DBProvider {
        private MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=;database=smsgolf;Allow Zero Datetime=true");

        public void DBConn() {

        }

        public void dbClose() {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }

        public bool dbConnect() {
            bool isOK = false;
            try {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();
                isOK = true;
            } catch (MySqlException ex) {

            }
            return isOK;
        }

        public DataTable getData(String sqlCmd) {
            DataTable dtResult = new DataTable();

            try {
                if (conn.State == ConnectionState.Closed)
                    dbConnect();

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = conn.CreateCommand();

                command.Parameters.Clear();
                command.CommandText = sqlCmd;
                adapter.SelectCommand = command;
                adapter.Fill(dtResult);
            } catch (MySqlException ex) {

            }

            return dtResult;
        }

        public DataTable getData(MySqlCommand Command) {
            DataTable dtResult = new DataTable();

            try {
                if (conn.State == ConnectionState.Closed)
                    dbConnect();

                Command.Connection = conn;

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                
                adapter.SelectCommand = Command;
                adapter.Fill(dtResult);
            } catch (MySqlException ex) {

            }

            return dtResult;
        }

        public bool Exec(MySqlCommand Command) {
            bool bResult = false;
            try {
                if (conn.State == ConnectionState.Closed)
                    dbConnect();

                Command.Connection = conn;
                Command.ExecuteNonQuery();
                bResult = true;
            } catch (MySqlException ex) {

            }

            return bResult;
        }

    }
}
