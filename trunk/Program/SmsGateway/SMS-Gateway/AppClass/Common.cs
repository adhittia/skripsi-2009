using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

using Com.Martin.SMS.Data;

namespace Com.Martin.SMS.Common {

    public static class CommandProcessor {

        public static Com.Martin.SMS.Data.SMSIncoming ProcessRequest(Com.Martin.SMS.Data.SMSIncoming Request) {

            return Request;
        }

        public static void ProcessBroadcast() {
        }

        public static void SendOutgoingSMS() {
        }
    }

    public class SMSHelper {
        private static MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=;database=smsgolf;Allow Zero Datetime=true");

        public static Com.Martin.SMS.Data.SMSIncoming SaveIncomingMessage(String Sender, String Receiver, String Message) {
            SMSIncoming incoming = new SMSIncoming();
            incoming.ID = CreateIdNumber(TypeSMS.Input);
            incoming.DateReceive = DateTime.Now;
            incoming.Sender = Sender;
            incoming.MessageText = Message;
            try {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "select nama_class, nama_assembly from daftar_register where reg_type = ?type and reg_name = ?name";
                command.Parameters.Clear();
                //command.Parameters.AddWithValue("type", CommandType);
                //command.Parameters.AddWithValue("name", CommandName);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {

                }

            } finally {
                conn.Close();
            }


            return new SMS.Data.SMSIncoming();
        }

        public static Com.Martin.SMS.Data.SMSIncoming GetIncomingMessage(String ID) {
            return new SMS.Data.SMSIncoming();
        }

        public static void SaveOutgoingMessage(Com.Martin.SMS.Data.SMSOutgoing Outgoing) {
        }

        public static Com.Martin.SMS.Data.SMSOutgoing GetOutgoingMessage(String ID) {
            return new SMS.Data.SMSOutgoing();
        }

        public static void SaveBroadcastMessage(List<Com.Martin.SMS.Data.SMSOutgoing> OutgoingList) {
        }

        public static List<Com.Martin.SMS.Data.SMSOutgoing> GetOutgoingSMSList() {
            return new List<Com.Martin.SMS.Data.SMSOutgoing>();
        }

        public static List<Com.Martin.SMS.Data.BroadcastScheduler> GetBroadcastScheduler(DateTime NextExecute) {
            return new List<Com.Martin.SMS.Data.BroadcastScheduler>();
        }

        private static String CreateIdNumber(TypeSMS type) {
            switch (type) {
                case TypeSMS.Broadcast:
                    return "b" + DateTime.Now.ToString("yyyyMMddHHmmssff");
                case TypeSMS.Input:
                    return "i" + DateTime.Now.ToString("yyyyMMddHHmmssff");
                case TypeSMS.Output:
                    return "o" + DateTime.Now.ToString("yyyyMMddHHmmssff");
                default:
                    return "u" + DateTime.Now.ToString("yyyyMMddHHmmssff");
            }
        }

        private enum TypeSMS {
            Broadcast,
            Input,
            Output
        }
    }

    class ConfigLoader {
        private MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=;database=smsgolf;Allow Zero Datetime=true");

        public Com.Martin.SMS.Command.AbstractRequest CreateRequestCommand(String CommandType, String CommandName) {
            String nama_class = "";
            String nama_assembly = "";

            try {
                this.conn.Open();
                MySqlCommand command = this.conn.CreateCommand();
                command.CommandText = "select nama_class, nama_assembly from daftar_register where reg_type = ?type and reg_name = ?name";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("type", CommandType);
                command.Parameters.AddWithValue("name", CommandName);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {

                    if (!reader.IsDBNull(0))
                        nama_class = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        nama_class = reader.GetString(1);
                }

            } finally {
                this.conn.Close();
            }

            if ((nama_class.Length == 0) || (nama_assembly.Length == 0)) {
                throw new Com.Martin.SMS.Exception.SMSException("Register tidak ditemukan.");
            }

            Com.Martin.SMS.Command.AbstractRequest request = Activator.CreateInstance(nama_assembly, nama_class).Unwrap() as Com.Martin.SMS.Command.AbstractRequest;
            return request;
        }

        public Com.Martin.SMS.Command.AbstractBroadcast CreateBroadcastCommand(String CommandType, String CommandName) {
            String nama_class = "";
            String nama_assembly = "";

            try {
                this.conn.Open();
                MySqlCommand command = this.conn.CreateCommand();
                command.CommandText = "select nama_class, nama_assembly from daftar_register where reg_type = ?type and reg_name = ?name";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("type", CommandType);
                command.Parameters.AddWithValue("name", CommandName);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {

                    if (!reader.IsDBNull(0))
                        nama_class = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        nama_class = reader.GetString(1);
                }

            } finally {
                this.conn.Close();
            }

            if ((nama_class.Length == 0) || (nama_assembly.Length == 0)) {
                throw new Com.Martin.SMS.Exception.SMSException("Register tidak ditemukan.");
            }

            Com.Martin.SMS.Command.AbstractBroadcast request = Activator.CreateInstance(nama_assembly, nama_class).Unwrap() as Com.Martin.SMS.Command.AbstractBroadcast;
            return request;
        }
    }
}
